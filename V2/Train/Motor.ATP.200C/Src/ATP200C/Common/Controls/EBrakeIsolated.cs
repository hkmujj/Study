using System;
using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Controls;

namespace ATP200C.Common.Controls
{
    /// <summary>
    /// 紧急制动隔离
    /// </summary>
    public class EBrakeIsolated : GDIRectText
    {
        private List<Line> m_Lines;

        private readonly Color m_Forgroud = Color.DarkRed;

        public EBrakeIsolated()
        {
            m_Lines = new List<Line>();

            Text = "紧急制动";

            SetTextColor(m_Forgroud.R, m_Forgroud.G, m_Forgroud.B);
            
            OutLineChanged += OnOutLineChanged;
        }

        private void OnOutLineChanged(object sender, EventArgs eventArgs)
        {
            m_Lines = new List<Line>()
            {
                new Line(Location, new Point(OutLineRectangle.Right, OutLineRectangle.Bottom)) {Color = m_Forgroud},
                new Line(new Point(OutLineRectangle.X, OutLineRectangle.Bottom),
                    new Point(OutLineRectangle.Right, OutLineRectangle.Top)) {Color = m_Forgroud}
            };
        }


        public override void OnDraw(Graphics g)
        {
            base.OnDraw(g);

            foreach (var line in m_Lines)
            {
                line.OnDraw(g);
            }
        }
    }
}