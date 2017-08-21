using System.Drawing;
using System.Drawing.Drawing2D;
using CommonUtil.Util.Extension;

namespace CommonUtil.Controls
{
    /// <summary>
    /// 坐标系
    /// </summary>
    public class Coordinate : CommonInnerControlBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        public Coordinate(Point origin)
        {
            Origin = origin;

            var mat = new Matrix();
            mat.Translate(-20, 5);
            m_OriginTextPoint = mat.TransformPoint(origin);

            IsOriginTextVisible = true;

            XAxis = new Line(new Point(origin.X, origin.Y), new Point(origin.X, origin.Y)) {Color = Color.White};
            YAxis = new Line(new Point(origin.X, origin.Y), new Point(origin.X, origin.Y)) {Color = Color.White};
        }

        /// <summary>
        /// 坐标原点
        /// </summary>
        public Point Origin { private set; get; }

        /// <summary>
        /// 坐标轴的颜色, 默认白色
        /// </summary>
        public Color Color
        {
            set
            {
                XAxis.Color = value;
                YAxis.Color = value;
            }
            get { return XAxis.Color; }
        }

        /// <summary>
        /// X 轴
        /// </summary>
        public Line XAxis { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public Line YAxis { private set; get; }

        /// <summary>
        /// X偏移, 左和右
        /// </summary>
        public float[] XOffsets
        {
            set
            {
                m_XOffsets = value;
                XAxis.StartPoint = new Point((int) (XAxis.StartPoint.X + value[0]),XAxis.StartPoint.Y);
                XAxis.EndPoint = new Point((int)(XAxis.EndPoint.X + value[1]), XAxis.EndPoint.Y);
            }
            get { return m_XOffsets; }
        }

        /// <summary>
        /// Y偏移, 上和下
        /// </summary>
        public float[] YOffsets
        {
            set
            {
                m_YOffsets = value;

                YAxis.StartPoint = new Point(YAxis.StartPoint.X, (int)(YAxis.StartPoint.Y + value[0]));
                YAxis.EndPoint = new Point(YAxis.EndPoint.X, (int)(YAxis.EndPoint.Y + value[1]));
            }
            get { return m_YOffsets; }
        }

        /// <summary>
        /// 坐标原点的 0 是否可见, 默认可见
        /// </summary>
        public bool IsOriginTextVisible { set; get; }

        private readonly Point m_OriginTextPoint;
        private float[] m_XOffsets;
        private float[] m_YOffsets;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        public override void OnDraw(Graphics g)
        {
            XAxis.OnDraw(g);
            YAxis.OnDraw(g);
            if (IsOriginTextVisible)
            {
                g.DrawString("0", CommonResouce.DefalutFont, CommonResouce.WhiteBrush, m_OriginTextPoint);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        public override void OnPaint(Graphics g)
        {
            XAxis.Refresh();
            YAxis.Refresh();

            base.OnPaint(g);
        }
    }
}
