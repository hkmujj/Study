using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Urban.NanJing.AirportLine.DDU
{
    class StateButton
    {
        private Rectangle m_Rect;
        private string m_Label;

        private Point m_Point1;
        private Point m_Point2;
        private Point m_Point3;
        private Point m_Point4;

        private Point[] m_PointsArray;

        private System.Drawing.Drawing2D.GraphicsPath m_Gp;
        private StringFormat m_StringFormat;


        public event Action<int> MouseUpEvent;


        private int m_SendMseeage;

        public bool IsSelected
        {
            set
            {
                m_IsSelected = value;
            }
            get
            {
                return m_IsSelected;
            }
        }
        private bool m_IsSelected;

        public StateButton(Rectangle rect, string label,int message)
        {
            m_Rect = rect;
            m_Label = label;

            m_SendMseeage = message;

            m_Point1 = new Point(m_Rect.X, m_Rect.Top);
            m_Point2 = new Point(m_Rect.Right, m_Rect.Top);
            m_Point3 = new Point(m_Rect.Right - 20, m_Rect.Bottom);
            m_Point4 = new Point(m_Rect.Left + 20, m_Rect.Bottom);

            m_PointsArray = new Point[4] { m_Point1, m_Point2, m_Point3, m_Point4 };


            m_Gp = new System.Drawing.Drawing2D.GraphicsPath();
            m_Gp.AddPolygon(m_PointsArray);
            m_StringFormat = new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center };

        }

        public void Paint(Graphics g)
        {
            if (m_IsSelected)
            {
                g.FillPath(Common.m_BackgroundBrush, m_Gp);
                g.DrawLine(Common.m_BlackPen, m_Point1, m_Point4);
                g.DrawLine(Common.m_BlackPen, m_Point4, m_Point3);
                g.DrawLine(Common.m_BlackPen, m_Point3, m_Point2);
            }
            else
            {
                g.FillPath(Common.m_WhiteBrush, m_Gp);
                g.DrawLine(Common.m_BlackPen, m_Point1, m_Point4);
                g.DrawLine(Common.m_BlackPen, m_Point4, m_Point3);
                g.DrawLine(Common.m_BlackPen, m_Point3, m_Point2);
                g.DrawLine(Common.m_BlackPen, m_Point2, m_Point1);
            }

            g.DrawString(m_Label, Common.m_16Font, Common.m_BlackBrush, m_Rect, m_StringFormat);
        }

        public void MouseUp(Point point)
        {
            if (m_Rect.Contains(point))
            {
                if (MouseUpEvent != null)
                {
                    MouseUpEvent(m_SendMseeage);
                }
            }


        }

        public void MouseDown(Point point)
        {

        }
    }
}
