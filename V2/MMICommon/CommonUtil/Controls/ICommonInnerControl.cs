using System.Drawing;

namespace CommonUtil.Controls
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


        /// <summary>
        /// 平移
        /// </summary>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        void Translate(float offsetX, float offsetY);

        /// <summary>
        /// 平移
        /// </summary>
        /// <param name="offset"></param>
        void Translate(PointF offset);

        /// <summary>
        /// 将此  放大指定量。
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        void Inflate(float width, float height);

        /// <summary>
        /// 将此 放大指定量。
        /// </summary>
        /// <param name="size"></param>
        void Inflate(Size size);

    }
}
