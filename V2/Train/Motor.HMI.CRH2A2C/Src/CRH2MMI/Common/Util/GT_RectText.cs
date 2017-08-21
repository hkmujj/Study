using System;
using System.Drawing;

namespace CRH2MMI
{

    /// <summary>
    /// 颜色定义
    /// 
    /// </summary>
    public class ButtonColor
    {
        public const int Yellow = 1;
        public const int Green = 2;
        public const int Blue = 3;
        public const int Red = 4;
    }
    /// <summary>
    /// 样式定义
    /// </summary>
    public class FormatStyle
    {
        public const int DirectionRightToLeft = 1;
        public const int Center = 2;
        public const int DirectionLeftToRight = 0;
        public const String strFont = "Arial";
    }

    /// <summary>
    /// 自定义文本框
    /// </summary>

    // ReSharper disable once InconsistentNaming
    public class GT_RectText
    {
        public Rectangle RectPosition;
        public String StrText;
        public System.Drawing.Font DrawFont;
        public int TextSize;
        public int labelsize;
        public int TextFormat;
        public bool Bold;
        public SolidBrush TextBrush;
        public SolidBrush BKBrush;
        public Pen Drawlinepen = new Pen(Color.FromArgb(255, 255, 255), 1);
        public bool isdrawrectfrm = false;
        public String StrLabel = "";
        public bool ShowLable = false;
        public Bitmap image;



        public void SetTextStyle(int size, int format, bool bold, String font)
        {

            TextSize = size;
            TextFormat = format;
            Bold = bold;
            DrawFont = new System.Drawing.Font(font, size);
            if (bold)
            {
                DrawFont = new Font(DrawFont, FontStyle.Bold);
            }
        }

        public void SetBkColor(int r, int g, int b)
        {

            BKBrush = new SolidBrush(Color.FromArgb(r, g, b));
        }
        public void setBkColor(Color color)
        {
            BKBrush = new SolidBrush(color);
        }

        public void SetDrawFrm(bool b)
        {
            isdrawrectfrm = b;
        }
        public void SetLabel(String text, int size)
        {
            StrLabel = text;
            labelsize = size;
            ShowLable = true;
        }
        public void SetLabelOn(bool b)
        {
            ShowLable = b;
        }

        public void SetTextColor(int r, int g, int b)
        {

            TextBrush = new SolidBrush(Color.FromArgb(r, g, b));

        }

        public void SetText(String text)
        {
            StrText = text;
        }

        public void SetTextRect(int x, int y, int width, int height)
        {
            RectPosition.X = x;
            RectPosition.Y = y;
            RectPosition.Height = height;
            RectPosition.Width = width;
        }
        public void SetLinePen(int r, int g, int b, int weight)
        {
            Drawlinepen = new Pen(Color.FromArgb(r, g, b), weight);
        }

        public void SetImage(Image img)
        {
            image = new Bitmap(img);

        }
        public void OnDraw(Graphics e)
        {

            if (BKBrush != null)
            {
                e.FillRectangle(BKBrush, RectPosition);
            }

            if (TextBrush != null)
            {
                StringFormat drawFormat = new StringFormat();

                drawFormat.LineAlignment = StringAlignment.Center;

                if (TextFormat != FormatStyle.DirectionLeftToRight)
                {

                    if (TextFormat == FormatStyle.DirectionRightToLeft)
                    {
                        drawFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;
                    }
                    else if (TextFormat == FormatStyle.Center)
                    {
                        drawFormat.Alignment = StringAlignment.Center;

                    }
                    if (ShowLable)
                    {
                        System.Drawing.Font tmpDrawFont = new System.Drawing.Font("Arial", labelsize);

                        RectangleF tmpRectPosition = RectPosition;
                        tmpRectPosition.Width = RectPosition.Width * 0.8f;
                        e.DrawString(StrText, DrawFont, TextBrush, tmpRectPosition, drawFormat);
                        tmpRectPosition.Width = RectPosition.Width * 0.4f;
                        tmpRectPosition.X = RectPosition.X + RectPosition.Width * 0.65f;
                        e.DrawString(StrLabel, tmpDrawFont, TextBrush, tmpRectPosition, drawFormat);
                    }
                    else
                    {
                        e.DrawString(StrText, DrawFont, TextBrush, RectPosition, drawFormat);
                    }
                }
                else
                {
                    if (ShowLable)
                    {
                        System.Drawing.Font tmpDrawFont = new System.Drawing.Font("Arial", labelsize);

                        RectangleF tmpRectPosition = RectPosition;
                        tmpRectPosition.Width = RectPosition.Width * 0.8f;
                        e.DrawString(StrText, DrawFont, TextBrush, tmpRectPosition, drawFormat);
                        tmpRectPosition.Width = RectPosition.Width * 0.4f;
                        tmpRectPosition.X = RectPosition.X + RectPosition.Width * 0.65f;
                        e.DrawString(StrLabel, tmpDrawFont, TextBrush, tmpRectPosition, drawFormat);
                    }
                    else
                    {
                        e.DrawString(StrText, DrawFont, TextBrush, RectPosition);
                    }
                }

            }
            if (isdrawrectfrm)
            {
                e.DrawRectangle(Drawlinepen, RectPosition);
            }

            if (image != null)
            {
                e.DrawImage(image, RectPosition);
            }
        }
    }
}

