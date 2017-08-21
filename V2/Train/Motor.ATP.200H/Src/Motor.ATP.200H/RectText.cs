using System;
using System.Drawing;

namespace Motor.ATP._200H
{
    public class RectText
    {
        public Rectangle m_RectPosition;
        public String m_StrText;
        public Font m_DrawFont;
        public int TextSize
        {
            get { return m_TextSize; }
            set
            {
                if (m_TextSize == value)
                {
                    return;
                }
                m_TextSize = value;
                m_DrawFont = new Font(m_Font, m_TextSize);
            }
        }

        public int m_Labelsize;
        public int m_TextFormat;
        public bool m_Bold;
        public SolidBrush m_TextBrush;
        public SolidBrush m_BKBrush;
        public Pen m_Drawlinepen = new Pen(Color.FromArgb(255, 255, 255), 1);
        public bool m_Isdrawrectfrm;
        public String m_StrLabel = "";
        public bool m_ShowLable;
        public Bitmap m_Image;
        private int m_TextSize;
        private string m_Font;

        public string Font
        {
            get { return m_Font; }
            set
            {
                if (m_Font == value)
                {
                    return;
                }
                m_Font = value;
            }
        }

        public void SetTextStyle(int size, int format, bool bold, String font)
        {

            TextSize = size;
            m_TextFormat = format;
            m_Bold = bold;
            Font = font;
            m_DrawFont = new Font(font, size);
            if (bold)
            {
                m_DrawFont = new Font(m_DrawFont, FontStyle.Bold);
            }
        }

        public void SetBkColor(int r, int g, int b)
        {

            m_BKBrush = new SolidBrush(Color.FromArgb(r, g, b));
        }

        public void SetDrawFrm(bool b)
        {
            m_Isdrawrectfrm = b;
        }
        public void SetLabel(String text, int size)
        {
            m_StrLabel = text;
            m_Labelsize = size;
            m_ShowLable = true;
        }
        public void SetLabelOn(bool b)
        {
            m_ShowLable = b;
        }

        public void SetTextColor(int r, int g, int b)
        {

            m_TextBrush = new SolidBrush(Color.FromArgb(r, g, b));

        }

        public void SetText(String text)
        {
            m_StrText = text;
        }

        public void SetTextRect(int x, int y, int width, int height)
        {
            m_RectPosition.X = x;
            m_RectPosition.Y = y;
            m_RectPosition.Height = height;
            m_RectPosition.Width = width;
        }
        public void SetLinePen(int r, int g, int b, int weight)
        {
            m_Drawlinepen = new Pen(Color.FromArgb(r, g, b), weight);
        }

        public void SetImage(Image img)
        {
            m_Image = new Bitmap(img);

        }
        public void OnDraw(Graphics e)
        {

            if (m_BKBrush != null)
            {
                e.FillRectangle(m_BKBrush, m_RectPosition);
            }

            if (m_TextBrush != null)
            {
                StringFormat drawFormat = new StringFormat();

                drawFormat.LineAlignment = StringAlignment.Center;

                if (m_TextFormat != FormatStyle.DirectionLeftToRight)
                {

                    if (m_TextFormat == FormatStyle.DirectionRightToLeft)
                    {
                        drawFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;
                    }
                    else if (m_TextFormat == FormatStyle.Center)
                    {
                        drawFormat.Alignment = StringAlignment.Center;

                    }
                    if (m_ShowLable)
                    {
                        Font tmpDrawFont = new Font("Arial", m_Labelsize);

                        RectangleF tmpRectPosition = m_RectPosition;
                        tmpRectPosition.Width = m_RectPosition.Width * 0.8f;
                        e.DrawString(m_StrText, m_DrawFont, m_TextBrush, tmpRectPosition, drawFormat);
                        tmpRectPosition.Width = m_RectPosition.Width * 0.4f;
                        tmpRectPosition.X = m_RectPosition.X + m_RectPosition.Width * 0.65f;
                        e.DrawString(m_StrLabel, tmpDrawFont, m_TextBrush, tmpRectPosition, drawFormat);
                    }
                    else
                    {
                        e.DrawString(m_StrText, m_DrawFont, m_TextBrush, m_RectPosition, drawFormat);
                    }
                }
                else
                {
                    if (m_ShowLable)
                    {
                        Font tmpDrawFont = new Font("Arial", m_Labelsize);

                        RectangleF tmpRectPosition = m_RectPosition;
                        tmpRectPosition.Width = m_RectPosition.Width * 0.8f;
                        e.DrawString(m_StrText, m_DrawFont, m_TextBrush, tmpRectPosition, drawFormat);
                        tmpRectPosition.Width = m_RectPosition.Width * 0.4f;
                        tmpRectPosition.X = m_RectPosition.X + m_RectPosition.Width * 0.65f;
                        e.DrawString(m_StrLabel, tmpDrawFont, m_TextBrush, tmpRectPosition, drawFormat);
                    }
                    else
                    {
                        e.DrawString(m_StrText, m_DrawFont, m_TextBrush, m_RectPosition);
                    }
                }

            }
            if (m_Isdrawrectfrm)
            {
                e.DrawRectangle(m_Drawlinepen, m_RectPosition);
            }

            if (m_Image != null)
            {
                e.DrawImage(m_Image, m_RectPosition);
            }
        }
    }
}
