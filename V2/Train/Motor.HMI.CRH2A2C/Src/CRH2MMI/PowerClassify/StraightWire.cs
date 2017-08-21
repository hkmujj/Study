using System;
using System.Drawing;
using CommonUtil.Controls;
using CRH2MMI.Common.View.Train;
using CRH2MMI.PowerClassify.PowerStates;

namespace CRH2MMI.PowerClassify
{
    /// <summary>
    /// 直的导线
    /// </summary>
    class StraightWire : Line, IPowerUnit
    {

        /// <summary>
        /// 默认导线宽度
        /// </summary>
        public const float DefaultWireWidth = 2f;

        /// <summary>
        /// 导线的宽度
        /// </summary>
        public float WireWidth
        {
            set
            {
                Width = value;
            }
            get { return Width; }
        }

        /// <summary>
        /// 在结束点是否画小圆
        /// </summary>
        public bool IsDrawEndPoint { set; get; }

        /// <summary>
        /// 结束点小圆点的直径, 默认为4
        /// </summary>
        public float Pie { set; get; }

        public TrainUnit Unit { get; set; }

        /// <summary>
        /// 是否通电
        /// </summary>
        public bool IsPowerOn { get { return true; } set{ throw new InvalidOperationException();} }

        public PowerFrom PowerFrom { get; set; }

        public bool TextVisible { get; set; }

        public void RefreshByState(PowerFrom powerFrom)
        {
            PowerFrom = powerFrom;
            var color = PowerFromColorAdaptor.GetColor(powerFrom);
            Color = color;
        }


        public StraightWire()
        {
            Pie = 4;
            IsDrawEndPoint = false;
            //IsPowerOn = false;
            //PowerOffColor = Color.White;
            WireWidth = DefaultWireWidth;
            //RefreshAction = OnRefresh;
        }

        public override void OnDraw(Graphics g)
        {
            base.OnDraw(g);
            if (IsDrawEndPoint)
            {
                var r = (int)Pie / 2;
                g.DrawPie(m_Pen, EndPoint.X - r, EndPoint.Y - r, Pie, Pie, 0, 360);
            }
        }

    }
}
