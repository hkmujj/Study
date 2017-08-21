using System.Drawing;

namespace HXD1.DeepDomestic
{
    /// <summary>
    /// 公共的基础模块
    /// </summary>
    public static class Common
    {
        public static int InitialPosX = 0;
        public static int InitialPosY = 0;


        public static  Color WhiteColor = Color.FromArgb(255, 255, 255);
        public static Color BlackColor = Color.FromArgb(0, 0, 0);
        public static Color GrayColor = Color.Gray;
        public static Color YellowColor = Color.FromArgb(60, Color.Yellow);
        public static Color ControlColor = Color.FromArgb(42, 238, 142);

        public static Font _12Font = new Font("Arial", 12);
        public static Font _14Font = new Font("Arial", 14);
        public static Font _16Font = new Font("Arial", 16);
        public static Font _20Font = new Font("Arial", 20);

        public static Pen PinkPen = new Pen(Color.Pink);
        public static Pen GreenPen = new Pen(Color.Green, 2);
        public static Pen GreenBoldPen = new Pen(Color.Green, 4);

        public static SolidBrush WhiteBrush = new SolidBrush(Color.White);
        public static SolidBrush GreenBrush = new SolidBrush(Color.Green);
        public static SolidBrush RedBrush = new SolidBrush(Color.Red);
        public static SolidBrush GrayBrush = new SolidBrush(Color.Gray);
        public static SolidBrush YellowBrush = new SolidBrush(Color.Yellow);
        public static SolidBrush BlueBrush = new SolidBrush(Color.Blue);
    }
}
