using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using CommonUtil.Controls;
using CommonUtil.Util.Extension;
using Motor.HMI.CRH1A.Common.Global;

namespace Motor.HMI.CRH1A.Common.Train
{
    class Car : CommonInnerControlBase
    {

        public static readonly Size DefaultSize = new Size(42, 42);

        // ReSharper disable once InconsistentNaming
        protected readonly SolidBrush m_WhiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255)); //白色画刷
        // ReSharper disable once InconsistentNaming
        protected readonly SolidBrush m_Blue1Brush = new SolidBrush(Color.FromArgb(48, 128, 168)); //较深蓝色画刷 用于绘制车身
        // ReSharper disable once InconsistentNaming
        protected readonly SolidBrush m_Blue2Brush = new SolidBrush(Color.FromArgb(56, 144, 192)); //较浅蓝色画刷 用于绘制各节车厢
        // ReSharper disable once InconsistentNaming
        protected readonly SolidBrush m_GrayBrush = new SolidBrush(Color.Gray); //灰色画刷

        private readonly Font m_Font = new Font("Arial", 11);

        /// <summary>
        /// 
        /// </summary>
        protected GraphicsPath GraphicsPath;


        /// <summary>
        /// 车身的
        /// </summary>
        protected SolidBrush BodyBrush;

        /// <summary>
        /// 车名
        /// </summary>
        protected SolidBrush NameBrush;

        protected Point NameLocation;

        protected List<Line> IntervalLines;

        private bool m_HasFault;
        private bool m_IsSelected;

        public int CarNo { private set; get; }

        public string CarName { private set; get; }


        /// <summary>
        ///  是否有故障
        /// </summary>
        public bool HasFault
        {
            set
            {
                m_HasFault = value;
                if (value)
                {
                    BodyBrush = m_GrayBrush;
                    NameBrush = m_WhiteBrush;
                }
                else
                {
                    BodyBrush = m_Blue2Brush;
                    NameBrush = m_WhiteBrush;
                    IsSelected = IsSelected;
                }
            }
            get { return m_HasFault; }
        }

        /// <summary>
        ///  是否被选中
        /// </summary>
        public bool IsSelected
        {
            set
            {
                m_IsSelected = value;
                if (!m_HasFault)
                {
                    if (!value)
                    {
                        BodyBrush = m_Blue2Brush;
                        NameBrush = m_WhiteBrush;
                    }
                    else
                    {
                        BodyBrush = m_WhiteBrush;
                        NameBrush = CommonResouce.BlackBrush;
                    }
                }
            }
            get { return m_IsSelected; }
        }


        public Car(int carNo)
        {
            CarNo = carNo;
            CarName = ((CarNo + 1) % GlobalParam.CarCount).ToString("00");
            HasFault = false;
            IsSelected = false;

            var location = Point.Empty + new Size(0, 2);
            var size = DefaultSize - new Size(0, 2);
            m_OutLineRectangle.Size = DefaultSize;
            GraphicsPath = new GraphicsPath();
            GraphicsPath.AddRectangle(new Rectangle(location, size));

            IntervalLines = new List<Line>
            {
                new Line(location, new Point(location.X, size.Width)),
                new Line(location, new Point(size.Height + 2, location.Y)),
                new Line(new Point(size.Width, location.Y), new Point(size.Width, size.Height))
            };

            CorretNameLocation(false);

            RefreshAction += o => HasFault = CommonTrainInBoolRes.Instance.IsFault(CarNo);

            OutLineChanged += OnOutLineChanged;
        }

        protected virtual void OnOutLineChanged(object sender, EventArgs eventArgs)
        {
            m_OutLineRectangle.Size = DefaultSize;

            GraphicsPath.Translate(LocationOffset);

            IntervalLines.ForEach(e => e.Translate(LocationOffset.X, LocationOffset.Y));

            NameLocation = NameLocation.Translate((int)LocationOffset.X, (int)LocationOffset.Y);
        }

        protected void CorretNameLocation(bool isReverse)
        {
            NameLocation = !isReverse
                ? new Point(Size.Width/3 + Location.X, Size.Height/3 + Location.Y)
                : new Point(-2*Size.Width/3 + Location.X, Size.Height/3 + Location.Y);
        }

        public override void OnDraw(Graphics g)
        {
            g.FillPath(BodyBrush, GraphicsPath);

            g.DrawString(CarName, m_Font, NameBrush, NameLocation);

            IntervalLines.ForEach(e => e.OnDraw(g));
        }

        public override void Translate(float offsetX, float offsetY)
        {
            GraphicsPath.Translate(offsetX, offsetY);

            IntervalLines.ForEach(e => e.Translate(offsetX, offsetY));

            NameLocation = NameLocation.Translate((int)offsetX, (int)offsetY);

            m_OutLineRectangle.Location = Point.Ceiling(GraphicsPath.GetBounds().Location);
        }

    }
}
