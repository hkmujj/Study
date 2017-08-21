using System;
using System.Drawing;

namespace Urban.NanJing.AirportLine.ATC.Casco
{
    public class Button1
    {
        private Rectangle m_Rect;
        public Image m_ImageDown;
        public Image m_ImageUp;

        private bool m_IsSelected;

        public event Action<int> MouseUpEvent;

        private int m_Message;

        public Button1(Rectangle rect, Image imageDown, Image imageUp, int message = 0)
        {
            m_Rect = rect;
            m_ImageDown = imageDown;
            m_ImageUp = imageUp;
            m_Message = message;
        }

        public void Paint(Graphics g)
        {
            if (m_IsSelected)
            {
                if (m_ImageDown != null)
                    g.DrawImage(m_ImageDown, m_Rect);
            }
            else
            {
                if (m_ImageUp != null)
                    g.DrawImage(m_ImageUp, m_Rect);
            }
        }

        public void MouseDown(Point p)
        {
            if (m_Rect.Contains(p))
            {
                m_IsSelected = true;
            }
        }

        public void MouseUp(Point p)
        {
            if (m_Rect.Contains(p))
            {
                m_IsSelected = false;


                if (MouseUpEvent != null)
                {
                    MouseUpEvent(m_Message);
                }

            }
        }
    }
}
