using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;
using CommonUtil.Controls;
using CommonUtil.Model;
using CRH2MMI.Common.Util;
using CRH2MMI.PowerClassify.PowerStates;

namespace CRH2MMI.PowerClassify
{
    /// <summary>
    /// 电路开关的基类, 大小固定,只有位置可变
    /// </summary>
    class CircuitChangerUnit : UnitBase
    {
        private readonly Pen m_LinePen = new Pen(Color.White, 2);
        // ReSharper disable once InconsistentNaming
        protected SolidBrush m_RectBrush;

        /// <summary>
        /// 实际的轮廓
        /// </summary>
        public override Rectangle ActureOutLine { get; protected set; }

        //public EventHandler IsPowerOnChanged;

        // ReSharper disable once InconsistentNaming
        protected GDIRectText m_Text;

        // ReSharper disable once InconsistentNaming
        protected List<StraightWire> m_StraightWires;
        public ReadOnlyCollection<StraightWire> StraightWires
        {
            get { return m_StraightWires.AsReadOnly(); }
        }
        // ReSharper disable once InconsistentNaming
        protected List<List<Point>> m_Lines;

        /// <summary>
        /// 中间矩形
        /// </summary>
        // ReSharper disable once InconsistentNaming
        protected Rectangle m_MidRectangle;

        /// <summary>
        /// 电流方向
        /// </summary>
        public Direction CircuitInputDirection
        {
            set;
            get;
        }



        public override bool IsPowerOn { get; set; }


        public override void RefreshByState(PowerFrom powerFrom)
        {
            PowerFrom = powerFrom;
            var color = PowerFromColorAdaptor.GetColor(powerFrom);
            m_LinePen.Color = PowerFromColorAdaptor.GetColor(PowerFrom.Null);
            m_RectBrush.Color = Color.Black;
            if (IsPowerOn)
            {
                m_RectBrush.Color = color;
                if (powerFrom != PowerFrom.Null)
                {
                    m_LinePen.Color = color;
                }
            }

            m_StraightWires.ForEach(e => e.RefreshByState(powerFrom));
        }

        /// <summary>
        /// 输入 
        /// </summary>
        public virtual StraightWire InputWire
        {
            get { return m_StraightWires[0]; }
        }

        /// <summary>
        /// 输出
        /// </summary>
        public virtual StraightWire OutputWire
        {
            get { return m_StraightWires[1]; }
        }

        /// <summary>
        ///  控件方向
        /// </summary>
        public Orientation UnitOrientation { protected set; get; }

        protected CircuitChangerUnit()
        {
            m_StraightWires = new List<StraightWire>();
            CircuitInputDirection = Direction.Up;
            m_RectBrush = new SolidBrush(Color.Black);
            OutLineChanged = OnOutLineChanged;
        }

        protected virtual void OnOutLineChanged(object sender, EventArgs eventArgs)
        {

        }

        public override void OnDraw(Graphics g)
        {
            // 边上的导线
            if (m_StraightWires != null)
            {
                m_StraightWires.ForEach(e => e.OnDraw(g));
            }

            // 中间的矩形 
            if (m_RectBrush != null) g.FillRectangle(m_RectBrush, m_MidRectangle);

            //中间的线
            foreach (var line in m_Lines)
            {
                g.DrawLine(m_LinePen, line[0], line[1]);
            }

            if (TextVisible)
            {
                m_Text.OnDraw(g);
            }
        }
    }
}
