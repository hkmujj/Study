using System;
using System.ComponentModel;
using System.Drawing;

namespace CommonUtil.Controls
{
    /// <summary>
    /// 直线
    /// </summary>
    public class Line : CommonInnerControlBase
    {
        /// <summary>
        /// 起始点
        /// </summary>
        public Point StartPoint { set; get; }

        /// <summary>
        /// 结束点
        /// </summary>
        public Point EndPoint { set; get; }

        /// <summary>
        /// 线宽度, 默认为2
        /// </summary>
        public float Width
        {
            set { m_Pen.Width = value; }
            get { return m_Pen.Width; }
        }

        /// <summary>
        /// 线的颜色 默认白色
        /// </summary>
        public Color Color
        {
            set { m_Pen.Color = value; }
            get { return m_Pen.Color; }
        }

        /// <summary>
        /// m_Pen
        /// </summary>
        // ReSharper disable once InconsistentNaming
        protected Pen m_Pen;
        /// <summary>
        /// 线的画笔
        /// </summary>
        public Pen LinePen
        {
            get { return m_Pen; }
            set { m_Pen = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="width"></param>
        public Line(Point startPoint, Point endPoint, float width)
        {
            m_Pen = new Pen(Color.White);
            Width = width;
            EndPoint = endPoint;
            StartPoint = startPoint;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        public Line(Point startPoint, Point endPoint)
            : this(startPoint, endPoint, 2)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        public Line(float width)
        {
            m_Pen = new Pen(Color.White);
            Width = width;
        }

        /// <summary>
        /// 
        /// </summary>
        public Line()
        {
            m_Pen = new Pen(Color.White);
            Width = 2;
        }

        /// <summary>
        /// 鼠标按下 always return false
        /// </summary>
        /// <param name="point"></param>
        public override bool OnMouseDown(Point point)
        {
            return false;
        }

        /// <summary>
        /// 鼠标按下后弹起 always return false
        /// </summary>
        /// <param name="point"></param>
        public override bool OnMouseUp(Point point)
        {
            return false;
        }

        /// <summary>
        /// 绘图
        /// </summary>
        /// <param name="g"></param>
        public override void OnDraw(Graphics g)
        {
            //Refresh();
            if (Visible)
            {
                g.DrawLine(m_Pen, StartPoint, EndPoint);
            }
        }

        /// <summary>
        /// 是否包含一个点
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        [Description("未实现 的")]
        public override bool Contains(Point point)
        {
            return false;
        }

        /// <summary>
        /// 平移
        /// </summary>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        public void Move(int offsetX, int offsetY)
        {
            StartPoint = new Point(StartPoint.X + offsetX, StartPoint.Y + offsetY);
            EndPoint = new Point(EndPoint.X + offsetX, EndPoint.Y + offsetY);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        public override void Translate(float offsetX, float offsetY)
        {
            Move((int)Math.Ceiling(offsetX), (int)Math.Ceiling(offsetY));
        }
    }
}
