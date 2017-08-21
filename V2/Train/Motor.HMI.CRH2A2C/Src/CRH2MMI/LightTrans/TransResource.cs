using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using CommonUtil.Controls;
using CRH2MMI.Common.Util;

namespace CRH2MMI.LightTrans
{
    class TransResource
    {
        private Trans m_Trans;

        // ReSharper disable once InconsistentNaming
        private const int m_PenWidth = 3;
        // ReSharper disable once InconsistentNaming
        private static readonly Pen m_DotPen = new Pen(Color.White, m_PenWidth) { DashStyle = DashStyle.Dash, DashPattern = new[] { 1.0f, 1.0f } };
        // ReSharper disable once InconsistentNaming
        private static readonly Pen m_RedPen = new Pen(Color.Red, m_PenWidth);
        // ReSharper disable once InconsistentNaming
        private static readonly Pen m_GreenPen = new Pen(CRH2Resource.GreenPen.Color, m_PenWidth);

        public static Pen DotPen { get { return m_DotPen; }}

        public static Pen RedPen
        {
            get { return m_RedPen; }
        }

        public static Pen GreenPen
        {
            get { return m_GreenPen; }
        }

        public TransResource(Trans trans)
        {
            m_Trans = trans;
        }


        public void GetLinePen(Line line, List<string> inBoolNames)
        {
            Debug.Assert(line != null, "line != null");
            var pen = m_GreenPen;
            if (m_Trans.GetInBoolValue(inBoolNames[1]))
            {
                pen = m_DotPen;
            }
            else if (m_Trans.GetInBoolValue(inBoolNames[0]))
            {
                pen = m_RedPen;
            }
            line.LinePen = pen;
        }
    }
}
