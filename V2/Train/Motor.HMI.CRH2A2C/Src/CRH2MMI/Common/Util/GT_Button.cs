using System;
using System.Drawing;

namespace CRH2MMI
{
    /// <summary>
    /// 自定义按钮
    /// 
    /// </summary>
    // ReSharper disable once InconsistentNaming
    class GT_Button
    {
        public RectangleF RectPosition;
        public GT_RectText ButtonText = new GT_RectText();
        public SolidBrush WhiteBrush = new SolidBrush(Color.FromArgb(149, 149, 149));
        public SolidBrush BlackBrush = new SolidBrush(Color.FromArgb(0, 0, 0));
        public SolidBrush ShapeBrush;
        public const int gap = 3;
        public bool style = true;//按钮样式，true为正常，false为按下
        public Point[] curpoint = new Point[4];
        public Pen darkpen = new Pen(Color.FromArgb(104, 112, 125), 3);
        public Pen linepen = new Pen(Color.FromArgb(221, 228, 234), 3);
        public Image m_Bgkimage;
        public GT_Button()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }


        public void SetButtonRect(int x, int y, int weight, int height)
        {
            RectPosition.X = x;
            RectPosition.Y = y;
            RectPosition.Height = height;
            RectPosition.Width = weight;
            ButtonText.SetTextRect(x + gap, y + gap, weight - gap * 2, height - gap * 2);
            curpoint[0].X = x;
            curpoint[0].Y = y + 1;
            curpoint[1].X = x;
            curpoint[1].Y = y + height - 1;
            curpoint[2].X = x + weight;
            curpoint[2].Y = y + 1;
            curpoint[3].X = x + weight;
            curpoint[3].Y = y + height - 1;


        }

        public void SetButtonColor(int r, int g, int b)
        {
            ButtonText.SetBkColor(r, g, b);
            ButtonText.SetTextColor(0, 0, 0);
            ShapeBrush = new SolidBrush(Color.FromArgb(255, 255, 0));
        }
        public void SetButtonText(String str)
        {
            ButtonText.SetTextStyle(12, FormatStyle.Center, true, "Arial");
            ButtonText.SetText(str);
        }
        public void OnButtonDown()
        {
            style = false;

        }
        public void OnButtonUp()
        {
            style = true;

        }
        public void SetButtonPic(String picpath)
        {
            m_Bgkimage = Image.FromFile(picpath);
        }
        public void SetButtonPic(Image image)
        {
            m_Bgkimage = image;
        }
        public void OnDraw(Graphics e)
        {

            ButtonText.OnDraw(e);
            if (style)
            {
                e.DrawLine(linepen, curpoint[0], curpoint[1]);
                e.DrawLine(linepen, curpoint[0], curpoint[2]);
                e.DrawLine(darkpen, curpoint[1], curpoint[3]);
                e.DrawLine(darkpen, curpoint[2], curpoint[3]);
            }

            else
            {
                e.DrawLine(darkpen, curpoint[0], curpoint[1]);
                e.DrawLine(darkpen, curpoint[0], curpoint[2]);
                e.DrawLine(linepen, curpoint[1], curpoint[3]);
                e.DrawLine(linepen, curpoint[2], curpoint[3]);
            }
            if (m_Bgkimage != null)
            {
                e.DrawImage(m_Bgkimage, ButtonText.RectPosition);
            }
        }
    }
}
