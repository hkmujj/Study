using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using CommonUtil.Model;
using CommonUtil.Util;
using CommonUtil.Util.Extension;
using CommonUtil.Controls;


namespace CRH2MMI.Common.View.Train
{
    /// <summary>
    /// 车头 车尾
    /// </summary>
    class HeadCar : Car
    {
        public Direction Direction
        {
            set
            {
                m_Direction = value;
                m_Arrows.Direction = value;
                m_CarView.Direction = value;
            }
            get { return m_Direction; }
        }

        /// <summary>
        ///  是否为司机室
        /// </summary>
        public bool IsDriverRoom { get; set; }

        public bool IsDriverRoomVisible { set; get; }

        private readonly Arrows m_Arrows;
        private Direction m_Direction;

        /// <summary>
        /// 车头的区域
        /// </summary>
        private readonly HeadCarBackRegion m_CarView;

        public HeadCar()
        {
            m_Arrows=new Arrows();
            m_CarView = new HeadCarBackRegion() { Brush = m_Brush };
        }

        protected override void OnOutLineChanged(object sender, EventArgs eventArgs)
        {
            base.OnOutLineChanged(sender, eventArgs);

            const int interval = 5;
            switch (m_Direction)
            {
                case Direction.Left:
                    m_Arrows.OutLineRectangle =
                        new Rectangle(
                            new Point(Location.X - Arrows.DefaultBoundSize.Width - interval,
                                m_OutLineRectangle.GetMidPoint(Direction.Left).Y - Arrows.DefaultBoundSize.Height/2),
                            Arrows.DefaultBoundSize);
                    
                    break;
                case Direction.Right:
                    m_Arrows.OutLineRectangle = new Rectangle(
                        new Point(OutLineRectangle.Right + interval,
                            m_OutLineRectangle.GetMidPoint(Direction.Right).Y - Arrows.DefaultBoundSize.Height/2),
                        new Size(Arrows.DefaultBoundSize.Width, Arrows.DefaultBoundSize.Height));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            m_CarView.OutLineRectangle = new Rectangle(new Point(Location.X, Location.Y + 1), new Size(Size.Width - 1, Size.Height - Wheel.DefaultSize.Height));
        }

        public override void OnDraw(Graphics g)
        {
            m_CarView.OnDraw(g);

            DrawWithoutRectangle(g);

            if (IsDriverRoomVisible)
            {
                RefreshDirvierRoomState();
                if (IsDriverRoom )
                {
                    m_Arrows.OnDraw(g);
                }
            }

        }

        public void RefreshDirvierRoomState()
        {
            IsDriverRoom = TrainResource.Instance.IsDriverRoom(CarConfig);
        }

        public override void Reverse()
        {
            base.Reverse();

            Direction = ~Direction;
        }

        private class HeadCarBackRegion : CommonInnerControlBase
        {
            private static readonly Size HeadCarViewDefaultSize = new Size(42, 26);

            private GraphicsPath m_CurrentCarRegion;

            public SolidBrush Brush { get; set; }

            public Direction Direction
            {
                set
                {
                    m_CurrentCarRegion = m_CarRegionDic[value];
                }
            }

            private readonly Dictionary<Direction, GraphicsPath> m_CarRegionDic;

            public HeadCarBackRegion()
            {
                m_CurrentCarRegion = new GraphicsPath();
                m_CurrentCarRegion.AddArc(new Rectangle(new Point(0, 0), new Size(HeadCarViewDefaultSize.Width, HeadCarViewDefaultSize.Height * 2)), 180, 90);

                //校正 , 把右边的矩阵扩大.
                const int correction = 50;
                m_CurrentCarRegion.AddPolygon(new Point[]
                {
                    new Point(21, 0), new Point(HeadCarViewDefaultSize.Width + correction, 0),
                    new Point(HeadCarViewDefaultSize.Width + correction, HeadCarViewDefaultSize.Height), new Point(0, HeadCarViewDefaultSize.Height),
                });

                m_CarRegionDic = new Dictionary<Direction, GraphicsPath>(2) { { Direction.Left, m_CurrentCarRegion } };
                m_CurrentCarRegion = (GraphicsPath)m_CurrentCarRegion.Clone();
                ReverseCurrent();
                m_CarRegionDic.Add(Direction.Right, m_CurrentCarRegion);

                m_CurrentCarRegion = m_CarRegionDic[Direction.Left];

                OutLineChanged += OnOutLineChanged;

            }

            private void OnOutLineChanged(object sender, EventArgs eventArgs)
            {
                foreach (var kvp in m_CarRegionDic)
                {
                    var graphicsPath = kvp.Value;

                    var bound = graphicsPath.GetBounds();

                    var mat = new Matrix();
                    mat.Translate(-bound.Left , -bound.Top );
                    mat.Scale(Size.Width / bound.Width, Size.Height / bound.Height, MatrixOrder.Append);
                    mat.Translate(Location.X , Location.Y, MatrixOrder.Append);
                    
                    graphicsPath.Transform(mat);
                }
            }

            public override void OnDraw(Graphics g)
            {
                g.FillPath(Brush, m_CurrentCarRegion);
            }

            private void ReverseCurrent()
            {
                var bound = m_CurrentCarRegion.GetBounds();

                var mat = MatrixHelper.CreateTurnMatrix(bound.GetMidPoint(Direction.Left).X, TurnOrientation.Horizontal);

                m_CurrentCarRegion.Transform(mat);
            }
        }
    }
}
