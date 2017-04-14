using System;
using System.Diagnostics;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ES.Facility.PublicModule.Win32
{
    public class ServerRoot
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct TokPriv1Luid
        {
            public int Count;
            public long Luid;
            public int Attr;
        }
        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GetCurrentProcess();

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);

        [DllImport("advapi32.dll", SetLastError = true)]
        internal static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall,
            ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool ExitWindowsEx(int DoFlag, int rea);

        internal const int SE_PRIVILEGE_ENABLED = 0x00000002;
        internal const int TOKEN_QUERY = 0x00000008;
        internal const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;
        internal const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";
        internal const int EWX_LOGOFF = 0x00000000;
        internal const int EWX_SHUTDOWN = 0x00000001;
        internal const int EWX_REBOOT = 0x00000002;
        internal const int EWX_FORCE = 0x00000004;
        internal const int EWX_POWEROFF = 0x00000008;
        internal const int EWX_FORCEIFHUNG = 0x00000010;

        private static bool DoExitWin(int DoFlag)
        {
            bool ok;
            TokPriv1Luid tp;
            var hproc = GetCurrentProcess();
            var htok = IntPtr.Zero;
            ok = OpenProcessToken(hproc, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref htok);
            tp.Count = 1;
            tp.Luid = 0;
            tp.Attr = SE_PRIVILEGE_ENABLED;
            ok = LookupPrivilegeValue(null, SE_SHUTDOWN_NAME, ref tp.Luid);
            ok = AdjustTokenPrivileges(htok, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero);
            ok = ExitWindowsEx(DoFlag, 0);
            return ok;
        }

        /**/
        /// <summary>
        /// 重新启动
        /// </summary>
        public static bool Reboot()
        {
            return DoExitWin(EWX_FORCE | EWX_REBOOT);
        }

        /**/
        /// <summary>
        /// 关机
        /// </summary>
        public static bool PowerOff()
        {
            return DoExitWin(EWX_FORCE | EWX_POWEROFF);
        }

        /**/
        /// <summary>
        /// 注销
        /// </summary>
        public static bool LogOff()
        {
            return DoExitWin(EWX_FORCE | EWX_LOGOFF);
        }
        /// <summary>
        /// 重启当前程序
        /// </summary>
        public static void ReStart()
        {
            var frm = GetControl();
            frm?.Invoke(new Action(() =>
            {
                Application.Exit();
                System.Threading.Thread thre = new System.Threading.Thread(Run);
                var appname = Application.ExecutablePath;
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                thre.Start(appname);
            }
            ));
        }

        private static void Run(object obj)
        {
            Process pro = new Process();
            pro.StartInfo.FileName = obj.ToString();
            pro.Start();
        }
        /// <summary>
        /// 关闭程序
        /// </summary>
        public static void Shutdown()
        {
            var frm = GetControl();
            frm?.Invoke(new Action(Application.Exit));
        }

        private static Control GetControl()
        {
            var frm = Application.OpenForms;
            if (frm.Count > 0)
            {
                return frm[0];
            }
            return null;
        }
    }
}
