using System;

namespace MMI.Facility.Interface.Hook
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHookProcess
    {
        /// <summary>
        /// 
        /// </summary>
        event Action<HotKeys> HotKeyEvent;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle1"></param>
        /// <param name="handle2"></param>
        void OnKeyboardEvent(IntPtr handle1, IntPtr handle2);
    }
}
