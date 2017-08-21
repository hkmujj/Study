using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using CommonUtil.Controls;
using CommonUtil.Model;
using CommonUtil.Util;
using CommonUtil.Util.Extension;
using Motor.HMI.CRH1A.Common.Config;
using Motor.HMI.CRH1A.Common.Global;

namespace Motor.HMI.CRH1A.Common.Train
{
    class HeadCar : Car
    {
        private HeadCarType m_HeadCarType;

        private readonly Point[] m_Point1 = new Point[9];
        private readonly Point[] m_Point2 = new Point[7];

        private Rectangle m_IsActiveRectangle;

        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActived { set; get; }

        public HeadCarType HeadCarType
        {
            private set
            {
                RectangleF bounds;
                if (GraphicsPath != null)
                {
                    if (value != m_HeadCarType)
                    {
                        m_HeadCarType = value;
                        bounds = GraphicsPath.GetBounds();
                        var matrix = MatrixHelper.CreateTurnMatrix(bounds.GetCenterPoint().X, TurnOrientation.Horizontal);
                        Transform(matrix);
                        return;
                    }
                }
                m_HeadCarType = value;
                GraphicsPath = new GraphicsPath();
                
                // 头向左
                if (value == HeadCarType.Head)
                {
                    GraphicsPath.AddArc(0, 0, 30, 60, 105, 130);
                    GraphicsPath.AddLine(m_Point1[0], m_Point1[1]);
                    GraphicsPath.AddLine(m_Point1[1], m_Point2[0]);
                    GraphicsPath.AddLine(m_Point2[0].X, m_Point2[0].Y, 0 + 5, 0 + 57);
                    IntervalLines = new List<Line>
                                      {
                                          new Line(m_Point1[0], new Point(m_Point1[1].X - 2, m_Point1[1].Y)),
                                          //new Line(new Point(m_Point1[1].X - 2, m_Point1[1].Y), new Point(m_Point2[0].X - 2, m_Point2[0].Y)),
                                      };
                    m_IsActiveRectangle = new Rectangle(1, 14, 10, 10);
                }
                else
                {
                    GraphicsPath.AddArc(0 + 305, 0, 30, 60, 308, 125);
                    GraphicsPath.AddLine(0 + 330, 0 + 57, m_Point2[6].X, m_Point2[6].Y);
                    GraphicsPath.AddLine(m_Point2[6], m_Point1[7]);
                    GraphicsPath.AddLine(m_Point1[7], m_Point1[8]);
                    IntervalLines = new List<Line>
                                      {
                                          new Line(new Point(m_Point2[6].X, m_Point2[6].Y + 1), new Point(m_Point1[7].X, m_Point1[7].Y + 1)),
                                          new Line(new Point(m_Point1[7].X, m_Point1[7].Y + 1), new Point(m_Point1[8].X, m_Point1[8].Y + 1)),
                                      };
                    m_IsActiveRectangle = new Rectangle( 30, 14, 10, 10);
                }
                var corrMat = new Matrix();
                bounds = GraphicsPath.GetBounds();
                corrMat.Translate(-bounds.X, -bounds.Y);
                corrMat.Scale(DefaultSize.Width / bounds.Width, DefaultSize.Height / bounds.Height, MatrixOrder.Append);
                GraphicsPath.Transform(corrMat);
                IntervalLines.ForEach(e => e.Translate(-bounds.X, -bounds.Y));
            }
            get { return m_HeadCarType; }
        }

        public HeadCar(int carNo,HeadCarType type)
            : base(carNo)
        {
            GraphicsPath = null;
            //个点坐标初始化
            m_Point1[0] = new Point(0 , 0 + 14);
            m_Point1[8] = new Point(0 + 331, 0 + 14);
            for (int i = 1; i < 8; i++)
            {
                m_Point1[i] = new Point(0 + 42 * i, 0 + 14);
            }
            m_Point2[0] = new Point(42, 57);
            for (int i = 1; i < 7; i++)
            {
                m_Point2[i] = new Point(0 + 42 * (i + 1), 0 + 55);
            }

            HeadCarType = type;

            RefreshAction += o =>
            {
                if ((HeadCarType == HeadCarType.Head && GlobalParam.Instance.TrainInfo.DriverLocation == DriverLocation.Left)||
                    HeadCarType == HeadCarType.Tail && GlobalParam.Instance.TrainInfo.DriverLocation == DriverLocation.Right)
                {
                    IsActived = true;
                }
                else
                {
                    IsActived = false;
                }
            };

        }

        public override void OnDraw(Graphics g)
        {
            base.OnDraw(g);

            if (IsActived)
            {
                g.FillEllipse(Brushes.Black, m_IsActiveRectangle);
            }
        }

        public override void Translate(float offsetX, float offsetY)
        {
            GraphicsPath.Translate(offsetX, offsetY);

            IntervalLines.ForEach(e => e.Translate(offsetX, offsetY));

            m_IsActiveRectangle.Offset((int)Math.Ceiling(offsetX), (int)Math.Ceiling(offsetY));

            NameLocation = NameLocation.Translate((int)offsetX, (int)offsetY);

            m_OutLineRectangle.Location = Point.Ceiling(GraphicsPath.GetBounds().Location);
        }

        private void Transform(Matrix matrix)
        {
            GraphicsPath.Transform(matrix);
            IntervalLines.ForEach(e =>
                                    {
                                        e.StartPoint = matrix.TransformPoint(e.StartPoint);
                                        e.EndPoint = matrix.TransformPoint(e.EndPoint);
                                    });
            m_IsActiveRectangle.Location = matrix.TransformPoint(m_IsActiveRectangle.Location);
        }

        protected override void OnOutLineChanged(object sender, EventArgs eventArgs)
        {
            var bounds = GraphicsPath.GetBounds();
            m_OutLineRectangle.Size = Size.Ceiling(bounds.Size);

            Translate(LocationOffset);
        }

        public override void Reverse()
        {
            HeadCarType = HeadCarType == HeadCarType.Head ? HeadCarType.Tail : HeadCarType.Head;
        }
    }
}
