using System;
using System.Drawing;
using CommonUtil.Util;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;


namespace CRH2MMI.Common.Control
{

    partial class CRH2Button
    {
        public class CRH2BtnBehavierStrategy : IBtnBehavierStrategy
        {
            public CRH2BtnBehavierStrategy(GDIButton control)
            {
                Control = control;
                m_CRH2Button = control as CRH2Button;
            }

            private bool IsAvailable
            {
                get { return m_CRH2Button.IsEnable && m_CRH2Button.Visible; }
            }

            public bool OnMouseDown(Point point)
            {
                if (!IsAvailable)
                {
                    return false;
                }

                if (m_CRH2Button.Contains(point))
                {
                    HandleUtil.OnHandle(m_CRH2Button.ButtonDownEvent, m_CRH2Button, null);
                    return true;
                }
                return false;
            }

            /// <summary>
            /// 不需要
            /// </summary>
            /// <param name="point"></param>
            /// <returns></returns>
            public bool OnMouseUp(Point point)
            {
                if (!IsAvailable)
                {
                    return false;
                }

                if (m_CRH2Button.Contains(point))
                {
                    HandleUtil.OnHandle(m_CRH2Button.ButtonUpEvent, m_CRH2Button, null);
                    return true;
                }
                return false;
            }

            public virtual void OnDraw(Graphics g)
            {
                if (!IsAvailable)
                {
                    return;
                }

                switch (m_CRH2Button.CurrentMouseState)
                {
                    case MouseState.MouseDown:
                        g.DrawImage(m_CRH2Button.DownImage, m_CRH2Button.OutLineRectangle);
                        m_CRH2Button.TextColor = Color.Black;
                        m_CRH2Button.DrawOutLine(g);
                        break;
                    case MouseState.MouseUp:
                        g.DrawImage(m_CRH2Button.UpImage, m_CRH2Button.OutLineRectangle);
                        m_CRH2Button.TextColor = Color.White;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                m_CRH2Button.DrawText(g, false);
                
            }

            public GDIButton Control { get; private set; }

            private readonly CRH2Button m_CRH2Button;
        }
    }
}
