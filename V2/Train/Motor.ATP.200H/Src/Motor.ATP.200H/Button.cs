﻿using System;
using System.Drawing;

namespace Motor.ATP._200H
{
    // ReSharper disable once InconsistentNaming
    public class Button
    {
        public RectangleF RectPosition;
        private readonly RectText m_ButtonText = new RectText();
        private const int Gap = 3;
        private bool m_Style = true;
        private readonly Point[] m_Curpoint = new Point[4];
        private readonly Pen m_Darkpen = new Pen(Color.FromArgb(104, 112, 125), 3);
        private readonly Pen m_Linepen = new Pen(Color.FromArgb(221, 228, 234), 3);
        private Image m_Bgkimage;


        public void SetButtonRect(int x, int y, int weight, int height)
        {
            RectPosition.X = x;
            RectPosition.Y = y;
            RectPosition.Height = height;
            RectPosition.Width = weight;
            m_ButtonText.SetTextRect(x + Gap, y + Gap, weight - Gap*2, height - Gap*2);
            m_Curpoint[0].X = x;
            m_Curpoint[0].Y = y + 1;
            m_Curpoint[1].X = x;
            m_Curpoint[1].Y = y + height - 1;
            m_Curpoint[2].X = x + weight;
            m_Curpoint[2].Y = y + 1;
            m_Curpoint[3].X = x + weight;
            m_Curpoint[3].Y = y + height - 1;


        }

        public void SetButtonColor(int r, int g, int b)
        {
            m_ButtonText.SetBkColor(r, g, b);
            m_ButtonText.SetTextColor(0, 0, 0);
        }

        public void SetButtonText(String str)
        {
            m_ButtonText.SetTextStyle(12, FormatStyle.Center, true, "Arial");
            m_ButtonText.SetText(str);
        }

        public void OnButtonDown()
        {
            m_Style = false;

        }

        public void OnButtonUp()
        {
            m_Style = true;

        }

        public void SetButtonPic(String picpath)
        {
            m_Bgkimage = Image.FromFile(picpath);
        }

        public void SetButtonPic(Image image)
        {
            m_Bgkimage = image;
        }

        public void OnDraw(Graphics g)
        {
            m_ButtonText.OnDraw(g);
            if (m_Style)
            {
                g.DrawLine(m_Linepen, m_Curpoint[0], m_Curpoint[1]);
                g.DrawLine(m_Linepen, m_Curpoint[0], m_Curpoint[2]);
                g.DrawLine(m_Darkpen, m_Curpoint[1], m_Curpoint[3]);
                g.DrawLine(m_Darkpen, m_Curpoint[2], m_Curpoint[3]);
            }

            else
            {
                g.DrawLine(m_Darkpen, m_Curpoint[0], m_Curpoint[1]);
                g.DrawLine(m_Darkpen, m_Curpoint[0], m_Curpoint[2]);
                g.DrawLine(m_Linepen, m_Curpoint[1], m_Curpoint[3]);
                g.DrawLine(m_Linepen, m_Curpoint[2], m_Curpoint[3]);
            }
            if (m_Bgkimage != null)
            {
                g.DrawImage(m_Bgkimage, m_ButtonText.m_RectPosition);
            }

        }
    }
}