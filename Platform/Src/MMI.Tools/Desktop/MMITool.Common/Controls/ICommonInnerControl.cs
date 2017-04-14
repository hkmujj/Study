using System.Drawing;

namespace MMITool.Common.Controls
{
    /// <summary>
    /// 公共的控件
    /// </summary>
    public interface ICommonInnerControl : IInnerControl
    {
        /// <summary>
        /// 起始点, 绝对位置 
        /// </summary>
        Point Location { set; get; }

        /// <summary>
        /// 大小 
        /// </summary>
        Size Size { set; get; }

        /// <summary>
        /// 轮廓大小
        /// </summary>
        Rectangle OutLineRectangle { set; get; }

        /// <summary>
        /// 反转自身
        /// </summary>
        void Reverse();
    }
}
