using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace Urban.NanJing.AirportLine.DDU
{
    public static class Common
    {

        public static int m_InitialPosX = 0;
        public static int m_InitialPosY = 0;

        public static Rectangle m_FirstTrainRect = new Rectangle(220, 210, 63, 35);
        public static Color m_WhiteColor = Color.FromArgb(255, 255, 255);
        public static Color m_GrayColor = Color.Gray;
        public static Color m_YellowColor = Color.FromArgb(60, Color.Yellow);
        public static Color m_BackGroundColor = Color.FromArgb(105, 191, 171);

        public static Font m_12Font = new Font("Arial", 12);
        public static Font m_14Font = new Font("Arial", 14);
        public static Font m_16Font = new Font("Arial", 16);

        public static Pen m_PinkPen = new Pen(Color.Pink);
        public static Pen m_BlackPen = new Pen(Color.Black);
        public static Pen m_BluePen = new Pen(Color.Blue);
        public static Pen m_Black2Pen = new Pen(Color.Black, 2);

        public static SolidBrush m_WhiteBrush = new SolidBrush(Color.White);
        public static SolidBrush m_GreenBrush = new SolidBrush(Color.Green);
        public static SolidBrush m_BackgroundBrush = new SolidBrush(m_BackGroundColor);
        public static SolidBrush m_BlueBrush = new SolidBrush(Color.Blue);
        public static SolidBrush m_BlackBrush = new SolidBrush(Color.Black);
        public static SolidBrush m_RedBrush = new SolidBrush(Color.Red);

        public static StringFormat m_Farmat = new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center };

    }
}
