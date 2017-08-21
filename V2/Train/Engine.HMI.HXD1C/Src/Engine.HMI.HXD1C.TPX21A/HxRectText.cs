using System;
using System.Drawing;

namespace Engine.HMI.HXD1C.TPX21A
{
    public class HxRectText
    {
        public Rectangle m_RectPosition;
        public String m_StrText;
        public Font m_DrawFont;
        public int m_TextSize;
        public int m_Labelsize;
        public int m_TextFormat;
        public bool m_Bold;
        public SolidBrush m_TextBrush;
        public SolidBrush BKBrush { private set; get; }
        public Pen m_Drawlinepen = new Pen(Color.FromArgb(255, 255, 255), 1);
        public bool m_Isdrawrectfrm = false;
        public String m_StrLabel = "";
        public bool m_ShowLable = false;
        public Bitmap m_Image;

        public HxRectText()
        {
            BKBrush = new SolidBrush(Color.White);
            m_TextBrush = new SolidBrush(Color.White);
        }


        public void SetTextStyle(int size, int format, bool bold, String font)
        {

            m_TextSize = size;
            m_TextFormat = format;
            m_Bold = bold;
            m_DrawFont = new Font(font, size);
            if (bold)
            {
                m_DrawFont = new Font(m_DrawFont, FontStyle.Bold);
            }
        }

        public void SetBkColor(int r, int g, int b)
        {

            BKBrush.Color = Color.FromArgb(r, g, b);
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

            m_TextBrush.Color = Color.FromArgb(r, g, b);

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
            if (img!=null)
            {
                m_Image = new Bitmap(img);
            }
           
        }
        public void OnDraw(Graphics e)
        {

            if (BKBrush != null)
            {
                e.FillRectangle(BKBrush, m_RectPosition);
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

            if (m_Image!=null)
            {
                e.DrawImage(m_Image,m_RectPosition);
            }
        }
    }
}
