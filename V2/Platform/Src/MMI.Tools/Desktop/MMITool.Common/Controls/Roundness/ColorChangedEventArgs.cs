using System;
using System.Diagnostics;
using System.Drawing;

namespace MMITool.Common.Controls.Roundness
{
    /// <summary>
    ///  颜色变化的事件参数
    /// </summary>
    public class ColorChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldColor"></param>
        /// <param name="newColor"></param>
        [DebuggerStepThrough]
        public ColorChangedEventArgs(Color oldColor, Color newColor)
        {
            NewColor = newColor;
            OldColor = oldColor;
        }

        /// <summary>
        /// 老的
        /// </summary>
        public Color OldColor { private set; get; }

        /// <summary>
        /// 新的
        /// </summary>
        public Color NewColor { private set; get; }
    }
}
