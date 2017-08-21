using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace GT_MMI.Interface.Button
{
    /// <summary>
    /// 按钮事件对象
    /// </summary>
    class ButtonMouseEventArgs : EventArgs
    {
        [DebuggerStepThrough]
        public ButtonMouseEventArgs(ButtonType buttonType, ButtonMouseEventType eventType)
        {
            EventType = eventType;
            ButtonType = buttonType;
        }

        public ButtonType ButtonType { private set; get; }

        public ButtonMouseEventType EventType { private set; get; }

        
    }
}
