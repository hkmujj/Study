using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using CommonUtil.Util.Extension;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.Util;
using CRH2MMI.PowerClassify.PowerStates;
using CommonUtil.Controls;

namespace CRH2MMI.Common.View.Train
{
    /// <summary>
    /// 车厢
    /// </summary>
    class Car : UnitBase
    {
        public Wheel Wheel { protected set; get; }

        public CarConfig CarConfig
        {
            set
            {
                m_CarConfig = value;
                CarNo = value.CarNo;
                CarType = value.CarType;
            }
            get { return m_CarConfig; }
        }

        /// <summary>
        /// 是否可见
        /// </summary>
        public bool CarVisible { set; get; }

        /// <summary>
        /// 手动设置车厢状态
        /// </summary>
        public ICarStateProxy CarStateProxy { set; get; }

        /// <summary>
        ///  { "T1", "N1", "N2", "N3", "N4", "N5", "N6", "T2" };
        /// </summary>
        private  readonly List<string> m_CarNames;

        // ReSharper disable once InconsistentNaming
        protected string m_CarNoName;

        public override int CarNo
        {
            get { return m_CarNo; }
            set
            {
                m_CarNo = value;
                m_CarNoName = (m_CarNo + 1).ToString(CultureInfo.InvariantCulture);
                m_CarName = m_CarNames[value];
            }
        }

        public override void RefreshByState(PowerFrom powerFrom)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 需要绘制 车名
        /// </summary>
        public bool NeedDrawCarName { set; get; }

        private string m_CarName;
        private GDIRectText m_CarNameText;

        // ReSharper disable once InconsistentNaming
        protected SolidBrush m_Brush;

        /// <summary>
        /// 车身大小
        /// </summary>
        private Rectangle m_CarRectangle;

        // ReSharper disable once InconsistentNaming
        protected Point m_CarNoPoint;

        private CarState m_CarState;
        private int m_CarNo;
        private CarConfig m_CarConfig;

        public CarType CarType
        {
            set { Wheel.WheelType = value; }
            get { return Wheel.WheelType; }
        }

        public static readonly Size DefaultSize = new Size(42, 24 + Wheel.DefaultSize.Height);

        public static readonly Size DefaultCarNameSize = new Size(30, 12);

        //public override Size Size 
        //{
        //    get { return DefaultSize; }
        //    set { throw new InvalidOperationException("不能设置大小, 只能设置 Location"); }
        //}

        /// <summary>
        /// 受电弓
        /// </summary>
        public List<AcceptEleArc> AcceptEleArcs { set; get; }

        /// <summary>
        /// 车辆状态
        /// </summary>
        public CarState CarState
        {
            set
            {
                m_CarState = value;
                switch (value)
                {
                    case CarState.Normal:
                        m_Brush.Color = CarConfig.Colors[0];
                        break;
                    case CarState.Pull:
                        m_Brush.Color = CarConfig.Colors[1];
                        break;
                    case CarState.EleBreak:
                        m_Brush.Color = CarConfig.Colors[2];
                        break;
                    case CarState.Fault:
                        m_Brush.Color = CarConfig.Colors[3];
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("value");
                }
            }
            get { return m_CarState; }
        }

        public Car()
        {
            CarVisible = true;
            Wheel = new Wheel();
            AcceptEleArcs = new List<AcceptEleArc>();
            m_Brush = new SolidBrush(Color.White);
            m_CarNames =
                GlobalInfo.CurrentCRH2Config.TrainConfig.CarConfigs.Select(s => s.CarName)
                    .ToList();
            OutLineChanged = OnOutLineChanged;
            this.RefreshAction += o =>
            {
                RefreshState();
                if (CarStateProxy != null)
                {
                    CarState = CarStateProxy.UserSetState;
                }
            };
        }

        protected virtual void OnOutLineChanged(object sender, EventArgs eventArgs)
        {
            // 忽略高度设置
            m_OutLineRectangle = new Rectangle(Location, Size.IsEmpty ? DefaultSize : new Size(Size.Width, DefaultSize.Height));
            m_CarRectangle = new Rectangle(new Point(Location.X, Location.Y + 1), new Size(Size.Width - 1, DefaultSize.Height - Wheel.DefaultSize.Height));
            m_CarNoPoint = new Point(Location.X + m_CarRectangle.Width / 2 - 5, Location.Y + m_CarRectangle.Height / 2 - 10);
            // 车厢号超过2位
            if (m_CarNo >= 9)
            {
                m_CarNoPoint = m_CarNoPoint.Translate(-10, 0);
            }
            m_CarNameText = new GDIRectText()
            {
                TextColor = Color.White,
                OutLineRectangle =
                    new Rectangle(m_OutLineRectangle.Left + m_OutLineRectangle.Width / 2 - DefaultCarNameSize.Width / 3,
                        m_OutLineRectangle.Top - DefaultCarNameSize.Height,
                        DefaultCarNameSize.Width, DefaultCarNameSize.Height),
                Text = m_CarNames[m_CarNo],
                DrawFont = new Font(CommonResouce.DefalutFont.FontFamily, 9f, FontStyle.Bold),
            };
            if (AcceptEleArcs.Any())
            {
                AcceptEleArcs.ForEach(e => e.Location = new Point(Location.X, Location.Y - AcceptEleArc.DefaultSize.Height ));
            }

            Wheel.OutLineRectangle =
                new Rectangle(
                    new Point(m_OutLineRectangle.X + m_CarRectangle.Width / 2 - Wheel.DefaultSize.Width / 3, m_CarRectangle.Bottom + 1),
                    AcceptEleArc.DefaultSize);
        }

        public override void OnDraw(Graphics g)
        {
            if (CarVisible)
            {
                //RefreshState();
                g.FillRectangle(m_Brush, m_CarRectangle);
                DrawWithoutRectangle(g);
            }
        }

        protected void DrawWithoutRectangle(Graphics g)
        {
            Wheel.OnDraw(g);

            DrawCarNo(g);

            if (NeedDrawCarName)
            {
                m_CarNameText.OnDraw(g);
            }

            if (AcceptEleArcs.Any())
            {
                AcceptEleArcs.ForEach(e =>
                {
                    e.State = TrainResource.Instance.GetAcceptEleArcState(e);
                    e.OnDraw(g);
                });
            }
        }

        public override void RefreshState()
        {
            if (CarType == CarType.Move)
            {
                CarState = TrainResource.Instance.GetMoveCarState(CarConfig);
            }
        }

        protected void DrawCarNo(Graphics g)
        {
            g.DrawString(m_CarNoName, CRH2Resource.Font14, CRH2Resource.BlackBrush, m_CarNoPoint);
        }

        [Obsolete]
        public override void Reverse()
        {
            if (AcceptEleArcs.Any())
            {
                AcceptEleArcs.ForEach(e => e.Reverse());
            }
        }
    }
}
