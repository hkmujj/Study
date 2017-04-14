using System;
using System.Drawing;
using MMITool.Common.Util;

namespace MMITool.Common.Controls
{
    /// <summary>
    /// 可选择的矩形控件
    /// </summary>
    public class SelectableRectangleControl : CommonInnerControlBase
    {

        /// <summary>
        /// 被选中时产生的事件
        /// </summary>
        public EventHandler SelectedHandler;

        /// <summary>
        ///  是否被选中 默认不选中
        /// </summary>
        public bool IsSelected { set; get; }

        /// <summary>
        /// 矩形轮廓是否可见 默认可见
        /// </summary>
        public bool IsRectangleVisible { set; get; }

        /// <summary>
        /// 选中时的边框
        /// </summary>
        public Pen SelectedPen { set; get; }

        /// <summary>
        /// 默认的选中边框色  黑色
        /// </summary>
        public static readonly Pen DefaultSelectedPend = new Pen(Color.Black, 2);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="outLineRectangle"></param>
        public SelectableRectangleControl(Rectangle outLineRectangle)
            : this()
        {
            OutLineRectangle = outLineRectangle;
        }

        /// <summary>
        /// 
        /// </summary>
        protected SelectableRectangleControl()
        {
            SelectedPen = DefaultSelectedPend;
            IsSelected = false;
            IsRectangleVisible = true;
        }

        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="point"></param>
        public override bool OnMouseDown(Point point)
        {
            if (OutLineRectangle.Contains(point))
            {
                IsSelected = true;
                HandleUtil.OnHandle(SelectedHandler, this, null);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 绘图
        /// </summary>
        /// <param name="g"></param>
        public override void OnDraw(Graphics g)
        {
            if (IsSelected && IsRectangleVisible)
            {
                g.DrawRectangle(SelectedPen, OutLineRectangle); 
            }
        }

    }
}
