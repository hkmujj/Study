using System;
using System.Drawing;

namespace Motor.ATP._300D.Common
{
    /// <summary>
    /// ¾ØÐÎ¿ò±³¾°ÑÕÉ«ºÍ×Ö
    /// </summary>
    public class RectTextInfo
    {
        //int FC_X = FrameButton2D.FrameChange_X;
        //int FC_Y = FrameButton2D.FrameChange_Y;

        SolidBrush m_TextBrush;
        Font m_TmpDrawFont;
        String m_Str;

        readonly StringFormat m_DrawFormat = new StringFormat();
        SolidBrush m_BkBrush;
        RectangleF m_RectPosition;

        public void SetTextColor(int r, int g, int b, string font, float size, int format, int LineAlignment)
        {
            m_TextBrush = new SolidBrush(Color.FromArgb(r, g, b));
            m_TmpDrawFont = new Font(font, size);
            m_DrawFormat.Alignment = (StringAlignment)format;
            m_DrawFormat.LineAlignment = (StringAlignment)LineAlignment;
        }

        public void SetTextColor(int r, int g, int b, string font, float size, int format)
        {
            m_TextBrush = new SolidBrush(Color.FromArgb(r, g, b));
            m_TmpDrawFont = new Font(font, size);
            m_DrawFormat.Alignment = (StringAlignment)format;
        }


        public void SetBKColor(int r, int g, int b)
        {
            m_BkBrush = new SolidBrush(Color.FromArgb(r, g, b));
        }

        public void SetRect(float x, float y, float width, float height)
        {
            m_RectPosition.X = x;
            m_RectPosition.Y = y;
            m_RectPosition.Height = height;
            m_RectPosition.Width = width;
        }

        public void Setstr(string str)
        {
            m_Str = str;
        }

        public void OnDraw(Graphics g)
        {
            g.FillRectangle(m_BkBrush, m_RectPosition.X, m_RectPosition.Y,
                m_RectPosition.Width, m_RectPosition.Height);

            if (m_TextBrush != null)
            {
                g.DrawString(m_Str, m_TmpDrawFont, m_TextBrush, m_RectPosition, m_DrawFormat);
            }
        }
    }
}