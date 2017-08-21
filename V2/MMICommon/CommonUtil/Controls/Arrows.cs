using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using CommonUtil.Model;
using CommonUtil.Util.Extension;

namespace CommonUtil.Controls
{
    /// <summary>
    /// 箭头
    /// </summary>
    public class Arrows : CommonInnerControlBase
    {
        /// <summary>
        /// 箭头方向
        /// </summary>
        public Direction Direction
        {
            set
            {
                if ((value == Direction.Right && m_Direction == Direction.Left)
                    ||(value == Direction.Left && m_Direction == Direction.Right))
                {
                    var mat = new Matrix();
                    var center = m_OutLineRectangle.GetCenterPoint();
                    mat.RotateAt(180, new PointF(center.X, center.Y));
                    m_Graph.Transform(mat);
                }
                m_Direction = value;
            }
            get { return m_Direction; }
        }

        /// <summary>
        /// 默认的边框大小
        /// </summary>
        public static Size DefaultBoundSize = new Size(15, 20);

        private readonly SolidBrush m_InnerBrush;
        /// <summary>
        /// 默认Color.White
        /// </summary>
        public Color ContentColor { set { m_InnerBrush.Color = value; } get { return m_InnerBrush.Color; } }

        // ReSharper disable once InconsistentNaming
        /// <summary>
        /// m_Graph
        /// </summary>
        protected GraphicsPath m_Graph;

        private Direction m_Direction;

        /// <summary>
        /// 
        /// </summary>
        public Arrows()
        {
            m_Direction = Direction.Left;
            m_OutLineRectangle = new Rectangle(new Point(0, 0), DefaultBoundSize);
            m_Graph = new GraphicsPath();
            InitArrow();
            m_InnerBrush = new SolidBrush(Color.White);
            OutLineChanged = OnOutLineChanged;
        }

        private void InitArrow()
        {
            m_Graph.AddLine(m_OutLineRectangle.GetMidPoint(Direction.Down), m_OutLineRectangle.GetMidPoint(Direction.Left));
            m_Graph.AddLine(m_OutLineRectangle.GetMidPoint(Direction.Left), m_OutLineRectangle.GetMidPoint(Direction.Up));
            m_Graph.AddLine(m_OutLineRectangle.GetMidPoint(Direction.Up), m_OutLineRectangle.GetMidPoint(Direction.Down));

            m_Graph.AddRectangle(
                new Rectangle(
                    new Point(m_OutLineRectangle.GetMidPoint(Direction.Up).X,
                        m_OutLineRectangle.GetPoint(Direction.Left, 1 / 4f).Y),
                    new Size(m_OutLineRectangle.Size.Width / 2, m_OutLineRectangle.Size.Height / 2)));

            m_Graph.CloseAllFigures();

        }

        private void OnOutLineChanged(object sender, EventArgs eventArgs)
        {
            var arg = eventArgs as OutLineChangedEventArgs<Rectangle>;

            var mat = new Matrix();
            Debug.Assert(arg != null, "arg != null");
            mat.Scale((float)arg.NewRectangle.Size.Width / (float)arg.OldRectangle.Size.Width, (float)arg.NewRectangle.Size.Height / (float)arg.OldRectangle.Size.Height);
            mat.Translate(arg.NewRectangle.X - arg.OldRectangle.X, arg.NewRectangle.Y - arg.OldRectangle.Y, MatrixOrder.Append);
            m_Graph.Transform(mat);

        }

        /// <summary>
        /// 绘图
        /// </summary>
        /// <param name="g"></param>
        public override void OnDraw(Graphics g)
        {
            g.FillPath(m_InnerBrush, m_Graph);
        }
    }
}
