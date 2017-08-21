using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Security.Permissions;
//#pragma warning disable 1009
using ES.Facility.PublicModule.Win32;

[assembly: CLSCompliant(true)]

namespace ES.Facility.PublicModule
{
    [CLSCompliant(false)]
    unsafe public class GlobalHooks
    {
        public delegate void HookReplacedEventHandler();
        public delegate void WindowEventHandler(IntPtr Handle);
        public delegate void SysCommandEventHandler(int SysCommand, int lParam);
        public delegate void ActivateShellWindowEventHandler();
        public delegate void TaskmanEventHandler();
        public delegate void BasicHookEventHandler(IntPtr Handle1, IntPtr Handle2);
        public delegate void WndProcEventHandler(IntPtr Handle, IntPtr Message, IntPtr wParam, IntPtr lParam);

        // Functions imported from our unmanaged DLL
        [DllImport("GlobalCbtHook.dll")]
        private static extern bool InitializeCbtHook(int threadID, IntPtr DestWindow);
        [DllImport("GlobalCbtHook.dll")]
        private static extern void UninitializeCbtHook();
        [DllImport("GlobalCbtHook.dll")]
        private static extern bool InitializeShellHook(int threadID, IntPtr DestWindow);
        [DllImport("GlobalCbtHook.dll")]
        private static extern void UninitializeShellHook();
        [DllImport("GlobalCbtHook.dll")]
        private static extern void InitializeKeyboardHook(int threadID, IntPtr DestWindow);
        [DllImport("GlobalCbtHook.dll")]
        private static extern void UninitializeKeyboardHook();
        [DllImport("GlobalCbtHook.dll")]
        private static extern void InitializeMouseHook(int threadID, IntPtr DestWindow);
        [DllImport("GlobalCbtHook.dll")]
        private static extern void UninitializeMouseHook();
        [DllImport("GlobalCbtHook.dll")]
        private static extern void InitializeKeyboardLLHook(int threadID, IntPtr DestWindow);
        [DllImport("GlobalCbtHook.dll")]
        private static extern void UninitializeKeyboardLLHook();
        [DllImport("GlobalCbtHook.dll")]
        private static extern void InitializeMouseLLHook(int threadID, IntPtr DestWindow);
        [DllImport("GlobalCbtHook.dll")]
        private static extern void UninitializeMouseLLHook();
        [DllImport("GlobalCbtHook.dll")]
        private static extern void InitializeCallWndProcHook(int threadID, IntPtr DestWindow);
        [DllImport("GlobalCbtHook.dll")]
        private static extern void UninitializeCallWndProcHook();
        [DllImport("GlobalCbtHook.dll")]
        private static extern void InitializeGetMsgHook(int threadID, IntPtr DestWindow);
        [DllImport("GlobalCbtHook.dll")]
        private static extern void UninitializeGetMsgHook();

        // Handle of the window intercepting messages
        private readonly IntPtr _Handle;

        private readonly CBTHook _Cbt;
        private readonly ShellHook _Shell;
        private readonly KeyboardHook _Keyboard;
        private readonly MouseHook _Mouse;
        private readonly KeyboardLLHook _KeyboardLL;
        private readonly MouseLLHook _MouseLL;
        private readonly CallWndProcHook _CallWndProc;
        private readonly GetMsgHook _GetMsg;

        public GlobalHooks(IntPtr handle)
        {
            _Handle = handle;

            _Cbt = new CBTHook(_Handle);
            _Shell = new ShellHook(_Handle);
            _Keyboard = new KeyboardHook(_Handle);
            _Mouse = new MouseHook(_Handle);
            _KeyboardLL = new KeyboardLLHook(_Handle);
            _MouseLL = new MouseLLHook(_Handle);
            _CallWndProc = new CallWndProcHook(_Handle);
            _GetMsg = new GetMsgHook(_Handle);
        }

        ~GlobalHooks()
        {
            _Cbt.Stop();
            _Shell.Stop();
            _Keyboard.Stop();
            _Mouse.Stop();
            _KeyboardLL.Stop();
            _MouseLL.Stop();
            _CallWndProc.Stop();
            _GetMsg.Stop();
        }

        [EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted = true)]
        public void ProcessWindowMessage(ref Message rMsg)
        {
            _Cbt.ProcessWindowMessage(ref rMsg);
            _Shell.ProcessWindowMessage(ref rMsg);
            _Keyboard.ProcessWindowMessage(ref rMsg);
            _Mouse.ProcessWindowMessage(ref rMsg);
            _KeyboardLL.ProcessWindowMessage(ref rMsg);
            _MouseLL.ProcessWindowMessage(ref rMsg);
            _CallWndProc.ProcessWindowMessage(ref rMsg);
            _GetMsg.ProcessWindowMessage(ref rMsg);
        }

        #region Accessors
        
        public CBTHook CBT
        {
            get { return _Cbt; }
        }

        public ShellHook Shell
        {
            get { return _Shell; }
        }

        public KeyboardHook Keyboard
        {
            get { return _Keyboard; }
        }

        public MouseHook Mouse
        {
            get { return _Mouse; }
        }

        public KeyboardLLHook KeyboardLL
        {
            get { return _KeyboardLL; }
        }

        public MouseLLHook MouseLL
        {
            get { return _MouseLL; }
        }

        public CallWndProcHook CallWndProc
        {
            get { return _CallWndProc; }
        }

        public GetMsgHook GetMsg
        {
            get { return _GetMsg; }
        }

        #endregion
        [CLSCompliant(false)]
        public abstract class Hook
        {
            protected bool isActive = false;
            protected IntPtr handle;

            public Hook(IntPtr Handle)
            {
                handle = Handle;
            }

            public void Start()
            {
                if (!isActive)
                {
                    isActive = true;
                    OnStart();
                }
            }

            public void Stop()
            {
                if (isActive)
                {
                    OnStop();
                    isActive = false;
                }
            }

            ~Hook()
            {
                Stop();
            }

            public bool IsActive
            {
                get { return isActive; }
            }

            protected abstract void OnStart();
            protected abstract void OnStop();

            [EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted = true)]
            public abstract void ProcessWindowMessage(ref Message rMsg);
        }

        [CLSCompliant(false)]
        public class CBTHook : Hook
        {
            // Values retreived with RegisterWindowMessage
            private int _MsgID_CBT_HookReplaced;
            private int _MsgID_CBT_Activate;
            private int _MsgID_CBT_CreateWnd;
            private int _MsgID_CBT_DestroyWnd;
            private int _MsgID_CBT_MinMax;
            private int _MsgID_CBT_MoveSize;
            private int _MsgID_CBT_SetFocus;
            private int _MsgID_CBT_SysCommand;

            public event HookReplacedEventHandler HookReplaced;
            public event WindowEventHandler Activate;
            public event WindowEventHandler CreateWindow;
            public event WindowEventHandler DestroyWindow;
            public event WindowEventHandler MinMax;
            public event WindowEventHandler MoveSize;
            public event WindowEventHandler SetFocus;
            public event SysCommandEventHandler SysCommand;

            public CBTHook(IntPtr Handle)
                : base(Handle)
            {
            }

            protected override void OnStart()
            {
                // Retreive the message IDs that we'll look for in WndProc
                _MsgID_CBT_HookReplaced = User.RegisterWindowMessage("WILSON_HOOK_CBT_REPLACED");
                _MsgID_CBT_Activate = User.RegisterWindowMessage("WILSON_HOOK_HCBT_ACTIVATE");
                _MsgID_CBT_CreateWnd = User.RegisterWindowMessage("WILSON_HOOK_HCBT_CREATEWND");
                _MsgID_CBT_DestroyWnd = User.RegisterWindowMessage("WILSON_HOOK_HCBT_DESTROYWND");
                _MsgID_CBT_MinMax = User.RegisterWindowMessage("WILSON_HOOK_HCBT_MINMAX");
                _MsgID_CBT_MoveSize = User.RegisterWindowMessage("WILSON_HOOK_HCBT_MOVESIZE");
                _MsgID_CBT_SetFocus = User.RegisterWindowMessage("WILSON_HOOK_HCBT_SETFOCUS");
                _MsgID_CBT_SysCommand = User.RegisterWindowMessage("WILSON_HOOK_HCBT_SYSCOMMAND");

                // Start the hook
                InitializeCbtHook(0, handle);
            }

            protected override void OnStop()
            {
                UninitializeCbtHook();
            }

            [EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted = true)]
            public override void ProcessWindowMessage(ref Message rMsg)
            {
                if (rMsg.Msg == _MsgID_CBT_HookReplaced)
                {
                    if (HookReplaced != null)
                        HookReplaced();
                }
                else if (rMsg.Msg == _MsgID_CBT_Activate)
                {
                    if (Activate != null)
                        Activate(rMsg.WParam);
                }
                else if (rMsg.Msg == _MsgID_CBT_CreateWnd)
                {
                    if (CreateWindow != null)
                        CreateWindow(rMsg.WParam);
                }
                else if (rMsg.Msg == _MsgID_CBT_DestroyWnd)
                {
                    if (DestroyWindow != null)
                        DestroyWindow(rMsg.WParam);
                }
                else if (rMsg.Msg == _MsgID_CBT_MinMax)
                {
                    if (MinMax != null)
                        MinMax(rMsg.WParam);
                }
                else if (rMsg.Msg == _MsgID_CBT_MoveSize)
                {
                    if (MoveSize != null)
                        MoveSize(rMsg.WParam);
                }
                else if (rMsg.Msg == _MsgID_CBT_SetFocus)
                {
                    if (SetFocus != null)
                        SetFocus(rMsg.WParam);
                }
                else if (rMsg.Msg == _MsgID_CBT_SysCommand)
                {
                    if (SysCommand != null)
                        SysCommand(rMsg.WParam.ToInt32(), rMsg.LParam.ToInt32());
                }
            }
        }

        [CLSCompliant(false)]
        public class ShellHook : Hook
        {
            // Values retreived with RegisterWindowMessage
            private int _MsgID_Shell_ActivateShellWindow;
            private int _MsgID_Shell_GetMinRect;
            private int _MsgID_Shell_Language;
            private int _MsgID_Shell_Redraw;
            private int _MsgID_Shell_Taskman;
            private int _MsgID_Shell_HookReplaced;
            private int _MsgID_Shell_WindowActivated;
            private int _MsgID_Shell_WindowCreated;
            private int _MsgID_Shell_WindowDestroyed;

            public event HookReplacedEventHandler HookReplaced;
            public event ActivateShellWindowEventHandler ActivateShellWindow;
            public event WindowEventHandler GetMinRect;
            public event WindowEventHandler Language;
            public event WindowEventHandler Redraw;
            public event TaskmanEventHandler Taskman;
            public event WindowEventHandler WindowActivated;
            public event WindowEventHandler WindowCreated;
            public event WindowEventHandler WindowDestroyed;

            public ShellHook(IntPtr Handle)
                : base(Handle)
            {
            }

            protected override void OnStart()
            {
                // Retreive the message IDs that we'll look for in WndProc
                _MsgID_Shell_HookReplaced = User.RegisterWindowMessage("WILSON_HOOK_SHELL_REPLACED");
                _MsgID_Shell_ActivateShellWindow = User.RegisterWindowMessage("WILSON_HOOK_HSHELL_ACTIVATESHELLWINDOW");
                _MsgID_Shell_GetMinRect = User.RegisterWindowMessage("WILSON_HOOK_HSHELL_GETMINRECT");
                _MsgID_Shell_Language = User.RegisterWindowMessage("WILSON_HOOK_HSHELL_LANGUAGE");
                _MsgID_Shell_Redraw = User.RegisterWindowMessage("WILSON_HOOK_HSHELL_REDRAW");
                _MsgID_Shell_Taskman = User.RegisterWindowMessage("WILSON_HOOK_HSHELL_TASKMAN");
                _MsgID_Shell_WindowActivated = User.RegisterWindowMessage("WILSON_HOOK_HSHELL_WINDOWACTIVATED");
                _MsgID_Shell_WindowCreated = User.RegisterWindowMessage("WILSON_HOOK_HSHELL_WINDOWCREATED");
                _MsgID_Shell_WindowDestroyed = User.RegisterWindowMessage("WILSON_HOOK_HSHELL_WINDOWDESTROYED");

                // Start the hook
                InitializeShellHook(0, handle);
            }

            protected override void OnStop()
            {
                UninitializeShellHook();
            }

            [EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted = true)]
            public override void ProcessWindowMessage(ref Message rMsg)
            {
                if (rMsg.Msg == _MsgID_Shell_HookReplaced)
                {
                    if (HookReplaced != null)
                        HookReplaced();
                }
                else if (rMsg.Msg == _MsgID_Shell_ActivateShellWindow)
                {
                    if (ActivateShellWindow != null)
                        ActivateShellWindow();
                }
                else if (rMsg.Msg == _MsgID_Shell_GetMinRect)
                {
                    if (GetMinRect != null)
                        GetMinRect(rMsg.WParam);
                }
                else if (rMsg.Msg == _MsgID_Shell_Language)
                {
                    if (Language != null)
                        Language(rMsg.WParam);
                }
                else if (rMsg.Msg == _MsgID_Shell_Redraw)
                {
                    if (Redraw != null)
                        Redraw(rMsg.WParam);
                }
                else if (rMsg.Msg == _MsgID_Shell_Taskman)
                {
                    if (Taskman != null)
                        Taskman();
                }
                else if (rMsg.Msg == _MsgID_Shell_WindowActivated)
                {
                    if (WindowActivated != null)
                        WindowActivated(rMsg.WParam);
                }
                else if (rMsg.Msg == _MsgID_Shell_WindowCreated)
                {
                    if (WindowCreated != null)
                        WindowCreated(rMsg.WParam);
                }
                else if (rMsg.Msg == _MsgID_Shell_WindowDestroyed)
                {
                    if (WindowDestroyed != null)
                        WindowDestroyed(rMsg.WParam);
                }
            }
        }

        [CLSCompliant(false)]
        public class KeyboardHook : Hook
        {
            // Values retreived with RegisterWindowMessage
            private int _MsgID_Keyboard;
            private int _MsgID_Keyboard_HookReplaced;

            public event HookReplacedEventHandler HookReplaced;
            public event BasicHookEventHandler KeyboardEvent;

            public KeyboardHook(IntPtr Handle)
                : base(Handle)
            {
            }

            protected override void OnStart()
            {
                // Retreive the message IDs that we'll look for in WndProc
                _MsgID_Keyboard = User.RegisterWindowMessage("WILSON_HOOK_KEYBOARD");
                _MsgID_Keyboard_HookReplaced = User.RegisterWindowMessage("WILSON_HOOK_KEYBOARD_REPLACED");

                // Start the hook
                InitializeKeyboardHook(0, handle);
            }
            protected override void OnStop()
            {
                UninitializeKeyboardHook();
            }

            [EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted = true)]
            public override void ProcessWindowMessage(ref Message rMsg)
            {
                if (rMsg.Msg == _MsgID_Keyboard)
                {
                    if (KeyboardEvent != null)
                        KeyboardEvent(rMsg.WParam, rMsg.LParam);
                }
                else if (rMsg.Msg == _MsgID_Keyboard_HookReplaced)
                {
                    if (HookReplaced != null)
                        HookReplaced();
                }
            }
        }

        [CLSCompliant(false)]
        public class MouseHook : Hook
        {
            // Values retreived with RegisterWindowMessage
            private int _MsgID_Mouse;
            private int _MsgID_Mouse_HookReplaced;

            public event HookReplacedEventHandler HookReplaced;
            public event BasicHookEventHandler MouseEvent;

            public MouseHook(IntPtr Handle)
                : base(Handle)
            {
            }

            protected override void OnStart()
            {
                // Retreive the message IDs that we'll look for in WndProc
                _MsgID_Mouse = User.RegisterWindowMessage("WILSON_HOOK_MOUSE");
                _MsgID_Mouse_HookReplaced = User.RegisterWindowMessage("WILSON_HOOK_MOUSE_REPLACED");

                // Start the hook
                InitializeMouseHook(0, handle);
            }
            protected override void OnStop()
            {
                UninitializeMouseHook();
            }

            [EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted = true)]
            public override void ProcessWindowMessage(ref Message rMsg)
            {
                if (rMsg.Msg == _MsgID_Mouse)
                {
                    if (MouseEvent != null)
                        MouseEvent(rMsg.WParam, rMsg.LParam);
                }
                else if (rMsg.Msg == _MsgID_Mouse_HookReplaced)
                {
                    if (HookReplaced != null)
                        HookReplaced();
                }
            }
        }

        [CLSCompliant(false)]
        public class KeyboardLLHook : Hook
        {
            // Values retreived with RegisterWindowMessage
            private int _MsgID_KeyboardLL;
            private int _MsgID_KeyboardLL_HookReplaced;

            public event HookReplacedEventHandler HookReplaced;
            public event BasicHookEventHandler KeyboardLLEvent;

            public KeyboardLLHook(IntPtr Handle)
                : base(Handle)
            {
            }

            protected override void OnStart()
            {
                // Retreive the message IDs that we'll look for in WndProc
                _MsgID_KeyboardLL = User.RegisterWindowMessage("WILSON_HOOK_KEYBOARDLL");
                _MsgID_KeyboardLL_HookReplaced = User.RegisterWindowMessage("WILSON_HOOK_KEYBOARDLL_REPLACED");

                // Start the hook
                InitializeKeyboardLLHook(0, handle);
            }

            protected override void OnStop()
            {
                UninitializeKeyboardLLHook();
            }

            [EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted = true)]
            public override void ProcessWindowMessage(ref Message rMsg)
            {
                if (rMsg.Msg == _MsgID_KeyboardLL)
                {
                    if (KeyboardLLEvent != null)
                        KeyboardLLEvent(rMsg.WParam, rMsg.LParam);
                }
                else if (rMsg.Msg == _MsgID_KeyboardLL_HookReplaced)
                {
                    if (HookReplaced != null)
                        HookReplaced();
                }
            }
        }

        [CLSCompliant(false)]
        public class MouseLLHook : Hook
        {
            // Values retreived with RegisterWindowMessage
            private int _MsgID_MouseLL;
            private int _MsgID_MouseLL_HookReplaced;

            public event HookReplacedEventHandler HookReplaced;
            public event BasicHookEventHandler MouseLLEvent;
            public event MouseEventHandler MouseDown;
            public event MouseEventHandler MouseMove;
            public event MouseEventHandler MouseUp;

            private const int WM_MOUSEMOVE = 0x0200;
            private const int WM_LBUTTONDOWN = 0x0201;
            private const int WM_LBUTTONUP = 0x0202;
            private const int WM_LBUTTONDBLCLK = 0x0203;
            private const int WM_RBUTTONDOWN = 0x0204;
            private const int WM_RBUTTONUP = 0x0205;
            private const int WM_RBUTTONDBLCLK = 0x0206;
            private const int WM_MBUTTONDOWN = 0x0207;
            private const int WM_MBUTTONUP = 0x0208;
            private const int WM_MBUTTONDBLCLK = 0x0209;
            private const int WM_MOUSEWHEEL = 0x020A;

            struct MSLLHOOKSTRUCT
            {
                public Point pt;
                public int mouseData;
                public int flags;
                public int time;
                public IntPtr dwExtraInfo;
            };

            public MouseLLHook(IntPtr Handle)
                : base(Handle)
            {
            }

            protected override void OnStart()
            {
                // Retreive the message IDs that we'll look for in WndProc
                _MsgID_MouseLL = User.RegisterWindowMessage("WILSON_HOOK_MOUSELL");
                _MsgID_MouseLL_HookReplaced = User.RegisterWindowMessage("WILSON_HOOK_MOUSELL_REPLACED");

                // Start the hook
                InitializeMouseLLHook(0, handle);
            }

            protected override void OnStop()
            {
                UninitializeMouseLLHook();
            }

            [EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted = true)]
            public override void ProcessWindowMessage(ref Message rMsg)
            {
                if (rMsg.Msg == _MsgID_MouseLL)
                {
                    if (MouseLLEvent != null)
                        MouseLLEvent(rMsg.WParam, rMsg.LParam);

                    var M = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(rMsg.LParam, typeof(MSLLHOOKSTRUCT));

                    if (rMsg.WParam.ToInt32() == WM_MOUSEMOVE)
                    {
                        if (MouseMove != null)
                            MouseMove(this, new MouseEventArgs(MouseButtons.None, 0, M.pt.X, M.pt.Y, 0));
                    }
                    else if (rMsg.WParam.ToInt32() == WM_LBUTTONDOWN)
                    {
                        if (MouseDown != null)
                            MouseDown(this, new MouseEventArgs(MouseButtons.Left, 0, M.pt.X, M.pt.Y, 0));
                    }
                    else if (rMsg.WParam.ToInt32() == WM_RBUTTONDOWN)
                    {
                        if (MouseDown != null)
                            MouseDown(this, new MouseEventArgs(MouseButtons.Right, 0, M.pt.X, M.pt.Y, 0));
                    }
                    else if (rMsg.WParam.ToInt32() == WM_LBUTTONUP)
                    {
                        if (MouseUp != null)
                            MouseUp(this, new MouseEventArgs(MouseButtons.Left, 0, M.pt.X, M.pt.Y, 0));
                    }
                    else if (rMsg.WParam.ToInt32() == WM_RBUTTONUP)
                    {
                        if (MouseUp != null)
                            MouseUp(this, new MouseEventArgs(MouseButtons.Right, 0, M.pt.X, M.pt.Y, 0));
                    }
                }
                else if (rMsg.Msg == _MsgID_MouseLL_HookReplaced)
                {
                    if (HookReplaced != null)
                        HookReplaced();
                }
            }
        }

        [CLSCompliant(false)]
        public class CallWndProcHook : Hook
        {
            // 注册消息
            private int _MsgID_CallWndProc;
            private int _MsgID_CallWndProc_Params;
            private int _MsgID_CallWndProc_HookReplaced;

            public event HookReplacedEventHandler HookReplaced;
            public event WndProcEventHandler CallWndProc;

            private IntPtr _CacheHandle;
            private IntPtr _CacheMessage;

            public CallWndProcHook(IntPtr handle)
                : base(handle)
            {
            }

            protected override void OnStart()
            {
                // 在消息回调函数中，接收到注册的信息ID
                _MsgID_CallWndProc_HookReplaced = User.RegisterWindowMessage("WILSON_HOOK_CALLWNDPROC_REPLACED");
                _MsgID_CallWndProc = User.RegisterWindowMessage("WILSON_HOOK_CALLWNDPROC");
                _MsgID_CallWndProc_Params = User.RegisterWindowMessage("WILSON_HOOK_CALLWNDPROC_PARAMS");

                //  开始钩子
                InitializeCallWndProcHook(0, handle);
            }

            protected override void OnStop()
            {
                UninitializeCallWndProcHook();
            }

            [EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted = true)]
            public override void ProcessWindowMessage(ref Message rMsg)
            {
                if (rMsg.Msg == _MsgID_CallWndProc)
                {
                    _CacheHandle = rMsg.WParam;
                    _CacheMessage = rMsg.LParam;
                }
                else if (rMsg.Msg == _MsgID_CallWndProc_Params)
                {
                    if (CallWndProc != null && _CacheHandle != IntPtr.Zero && _CacheMessage != IntPtr.Zero)
                        CallWndProc(_CacheHandle, _CacheMessage, rMsg.WParam, rMsg.LParam);
                    _CacheHandle = IntPtr.Zero;
                    _CacheMessage = IntPtr.Zero;
                }
                else if (rMsg.Msg == _MsgID_CallWndProc_HookReplaced)
                {
                    if (HookReplaced != null)
                        HookReplaced();
                }
            }
        }

        [CLSCompliant(false)]
        public class GetMsgHook : Hook
        {
            // 注册消息
            private int _MsgID_GetMsg;
            private int _MsgID_GetMsg_Params;
            private int _MsgID_GetMsg_HookReplaced;

            public event HookReplacedEventHandler HookReplaced;
            public event WndProcEventHandler GetMsg;

            private IntPtr _CacheHandle;
            private IntPtr _CacheMessage;

            public GetMsgHook(IntPtr Handle)
                : base(Handle)
            {
            }

            protected override void OnStart()
            {
                // 在消息回调函数中，接收到注册的信息ID
                _MsgID_GetMsg_HookReplaced = User.RegisterWindowMessage("WILSON_HOOK_GETMSG_REPLACED");
                _MsgID_GetMsg = User.RegisterWindowMessage("WILSON_HOOK_GETMSG");
                _MsgID_GetMsg_Params = User.RegisterWindowMessage("WILSON_HOOK_GETMSG_PARAMS");

                //  开始钩子
                InitializeGetMsgHook(0, handle);
            }

            protected override void OnStop()
            {
                //  卸载钩子
                UninitializeGetMsgHook();
            }

            [EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted = true)]
            public override void ProcessWindowMessage(ref Message rMsg)
            {
                if (rMsg.Msg == _MsgID_GetMsg)
                {
                    _CacheHandle = rMsg.WParam;
                    _CacheMessage = rMsg.LParam;
                }
                else if (rMsg.Msg == _MsgID_GetMsg_Params)
                {
                    if (GetMsg != null && _CacheHandle != IntPtr.Zero && _CacheMessage != IntPtr.Zero)
                        GetMsg(_CacheHandle, _CacheMessage, rMsg.WParam, rMsg.LParam);
                    _CacheHandle = IntPtr.Zero;
                    _CacheMessage = IntPtr.Zero;
                }
                else if (rMsg.Msg == _MsgID_GetMsg_HookReplaced)
                {
                    if (HookReplaced != null)
                        HookReplaced();
                }
            }
        }
    }
}
