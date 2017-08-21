using System;
using System.Drawing;

namespace LightRail.ATC.Casco
{
    public class DialPlateDegreeLineView
    {
        private readonly DialPlateDegree m_DialPlateDegree;

        private readonly PointF[] m_LinePoint;

        private readonly PointF m_TextLocaion;

        private static StringFormat m_Format = new StringFormat(){ LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center};

        public DialPlateDegreeLineView(DialPlateDegree dialPlateDegree, PointF o, float r)
        {
            m_DialPlateDegree = dialPlateDegree;
            var cos = (float)Math.Cos((dialPlateDegree.Angle + LineDialPlateModel.ZeroAngleInDrawArc) * Math.PI / 180.0);
            var sin = (float)Math.Sin((dialPlateDegree.Angle + LineDialPlateModel.ZeroAngleInDrawArc) * Math.PI / 180.0);
            var start = new PointF(o.X + r * cos, o.Y + r * sin);
            var end = new PointF(o.X + (r - dialPlateDegree.Legth) * cos, o.Y + (r - dialPlateDegree.Legth) * sin);
            m_TextLocaion = new PointF(o.X + (r - dialPlateDegree.Legth - 30) * cos, o.Y + (r - dialPlateDegree.Legth - 30) * sin);
            m_LinePoint = new PointF[] { start, end };
        }

        public void OnDraw(Graphics g)
        {
            g.DrawLine(m_DialPlateDegree.DegreePen, m_LinePoint[0], m_LinePoint[1]);

            if (!string.IsNullOrWhiteSpace(m_DialPlateDegree.Text))
            {
                g.DrawString(m_DialPlateDegree.Text, m_DialPlateDegree.TxtFont, m_DialPlateDegree.TextBrush, m_TextLocaion, m_Format);
            }
        }
    }
}