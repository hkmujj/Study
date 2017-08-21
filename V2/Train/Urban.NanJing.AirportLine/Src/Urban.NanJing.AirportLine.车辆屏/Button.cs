using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Urban.NanJing.AirportLine.DDU
{
    public class Button
    {
        private Rectangle m_Rect;
        public Image m_ImageDown;
        public Image m_ImageUp;

        private bool m_IsSelected;


        public event Action<int> MouseUpEvent;      

        private int m_Message;

        public int Message {
            get
            {
                return m_Message;
            }
            set
            {
                m_Message = value;
            }
        }

        public Button(Rectangle rect, Image imageDown, Image imageUp, int message = 0)
        {
            m_Rect = rect;
            m_ImageDown = imageDown;
            m_ImageUp = imageUp;
            m_Message = message;
        }

        public void OnDraw(Graphics g)
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
