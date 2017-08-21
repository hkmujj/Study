using System;
using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;

namespace CRH2MMI.PowerVoltage
{
    internal abstract class PowerVoltagePageBase
    {
        // ReSharper disable once InconsistentNaming
        protected Power m_PowerView;

        // ReSharper disable once InconsistentNaming
        protected List<PowerVoltageUnit> m_PowerVoltageUnits;

        // ReSharper disable once InconsistentNaming
        protected List<GDIRectText> m_ConstInfo;

        // ReSharper disable once InconsistentNaming
        protected GDIButton m_ChangePageBtn;

        public EventHandler ChangePage;

        /// <summary>
        /// 坐标原点
        /// </summary>
        // ReSharper disable once InconsistentNaming
        protected static readonly Point m_CoordinateOrigin = new Point(91, 450);

        protected PowerVoltagePageBase(Power power)
        {
            m_PowerView = power;
            m_PowerVoltageUnits = new List<PowerVoltageUnit>();
            m_ConstInfo = new List<GDIRectText>();
        }


        public virtual void OnPaint(Graphics g)
        {
            m_ChangePageBtn.OnDraw(g);

            m_PowerVoltageUnits.ForEach(e => e.OnPaint(g));

            m_ConstInfo.ForEach(e => e.OnDraw(g));
        }

        public bool OnMouseDown(Point point)
        {
            return m_ChangePageBtn.OnMouseDown(point);
        }
    }
}
