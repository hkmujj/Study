using System;
using System.Drawing;
using CRH2MMI.Common.Util;
using CRH2MMI.PowerClassify.PowerStates;

namespace CRH2MMI.Common.View.Train
{
    /// <summary>
    /// 车厢的轮子, 第节两个
    /// </summary>
    class Wheel : UnitBase
    {
        private readonly SolidBrush m_Brush;
        private readonly Pen m_Pen;

        public CarType WheelType
        {
            set
            {
                m_WheelType = value;
                if (value == CarType.Move)
                {
                    m_DrawDelegate = (g, f, f1, width, height, angle, sweepAngle) =>
                            g.FillPie(m_Brush, f, f1, width, height, angle, sweepAngle);
                }
                else
                {
                    m_DrawDelegate = (g, f, f1, width, height, angle, sweepAngle) => g.DrawArc(m_Pen, f, f1, width, height, angle, sweepAngle);
                }
            }
            get { return m_WheelType; }
        }

        /// <summary>
        /// 轮子半径
        /// </summary>
        public static readonly int DefaultRadius = 5;

        /// <summary>
        /// 轮子 间的距离 
        /// </summary>
        public static readonly int DefaultInterval = 20;

        public static readonly Size DefaultSize = new Size(DefaultRadius * 4 + DefaultInterval, DefaultRadius * 2);

        public WheelState State
        {
            set
            {
                m_State = value;
                if (value == WheelState.Normal)
                {
                    m_Pen.Color = Color.White;
                    m_Brush.Color = Color.White;
                }
                else
                {
                    m_Pen.Color = Color.Red;
                    m_Brush.Color = Color.Red;
                }
            }
            get { return m_State; }
        }

        /// <summary>
        /// Pen pen, float x, float y, float width, float height, float startAngle, float sweepAngle
        /// </summary>
        private DrawFunc m_DrawDelegate;

        private CarType m_WheelType;
        private WheelState m_State;

        ///// <summary>
        ///// 轮子半径
        ///// </summary>
        //public int Radius { set; get; }
        ///// <summary>
        ///// 轮子 间的距离 
        ///// </summary>
        //public int Interval { set; get; }

        public override Size Size
        {
            get { return DefaultSize; }
            set { throw new InvalidOperationException("不能设置大小, 只能设置 Location"); }
        }

        public Wheel()
        {
            m_Brush = new SolidBrush(Color.White);
            m_Pen = new Pen(m_Brush);
            State = WheelState.Normal;
        }

        public override void OnDraw(Graphics g)
        {
            m_DrawDelegate(g, Location.X , Location.Y , DefaultRadius*2, DefaultRadius*2,
                0, 360);
            m_DrawDelegate(g, Location.X + DefaultInterval , Location.Y , DefaultRadius*2,
                DefaultRadius*2,
                0, 360);
        }

        private delegate void DrawFunc(Graphics g,
            float x, float y, float width, float height, float startAngle, float sweepAngle);

        public override void RefreshByState(PowerFrom powerFrom)
        {
            throw new NotImplementedException();
        }
    }


}
