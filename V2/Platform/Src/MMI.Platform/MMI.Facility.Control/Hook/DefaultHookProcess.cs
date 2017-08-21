using System;
using ES.Facility.PublicModule;
using MMI.Facility.Interface.Hook;

namespace MMI.Facility.Control.Hook
{
    class DefaultHookProcess : IHookProcess
    {
        /// <summary>
        /// Keyboard_KeyboardEvent使用
        /// </summary>
        string m_TvKbInfo = string.Empty;

        private readonly GlobalHooks m_GlobalHooks;

        /// <summary>
        /// 记录上次钩子记录的按钮信息
        /// </summary>
        private string m_OldKbinfo = string.Empty;

        public event Action<HotKeys> HotKeyEvent;

        public DefaultHookProcess(IntPtr handle)
        {
            m_GlobalHooks = new GlobalHooks(handle);

            m_GlobalHooks.Keyboard.KeyboardEvent += OnKeyboardEvent;
        }


        public void OnKeyboardEvent(IntPtr handle1, IntPtr handle2)
        {
            //键盘钩子处理程序
            m_TvKbInfo = string.Format("{0}/{1}", handle1, handle2);

            //键盘在按下后，会重复出现多次，因此需将多余的忽略掉
            if (m_TvKbInfo != m_OldKbinfo)
            {
                if (handle2.ToInt32() == 558694401) { OnHotKeyEvent(HotKeys.AltRight); }

                else if (handle2.ToInt32() == 558366721) { OnHotKeyEvent(HotKeys.AltUp); }

                else if (handle2.ToInt32() == 558891009) { OnHotKeyEvent(HotKeys.AltDown); }

                else if (handle2.ToInt32() == 558563329) { OnHotKeyEvent(HotKeys.AltLeft); }

                else if (handle2.ToInt32() == 983041) { OnHotKeyEvent(HotKeys.WinTab); }

                else if (handle2.ToInt32() == 65537) { OnHotKeyEvent(HotKeys.Esc); }

                else if (handle2.ToInt32() == 541261825) { OnHotKeyEvent(HotKeys.AltF9); }

                else if (handle2.ToInt32() == 542572545) { OnHotKeyEvent(HotKeys.AltF11); }

                else if (handle2.ToInt32() == 542638081) { OnHotKeyEvent(HotKeys.AltF12); }

            }

            m_OldKbinfo = m_TvKbInfo;     //记录该次按键
        }

        private void OnHotKeyEvent(HotKeys hotKeys)
        {
            if (HotKeyEvent!=null)
            {
                HotKeyEvent(hotKeys);
            }
        }

    }
}
