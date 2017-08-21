using System;
using System.Diagnostics;

namespace MMI.Facility.DataType.View.EventArg
{
    /// <summary>
    /// 属性窗口的值被双击
    /// </summary>
    public class AttributeValueDoubleClickEventArg : EventArgs
    {
        [DebuggerStepThrough]
        public AttributeValueDoubleClickEventArg(string appName, int index)
        {
            Index = index;
            AppName = appName;
        }

        public string AppName { private set; get; }

        public int Index { private set; get; }
    }
}
