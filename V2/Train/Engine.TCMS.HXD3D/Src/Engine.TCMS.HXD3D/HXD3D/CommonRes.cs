using System.ComponentModel;
using System.Drawing;

namespace Engine.TCMS.HXD3D.HXD3D
{
    internal class CommonRes
    {
        // Properties
        [DefaultValue(1f)]
        public static float Scale    {
            get
            {
                if (m_Scale==0)
                {
                    return 1f;
                }
                return m_Scale;
            } private set { m_Scale = value; } }

        [DefaultValue(0)]
        public static int ScreenMoveX { get; private set; }
        [DefaultValue(0)]
        public static int ScreenMoveY { get; private set; }

        // Fields

        public static readonly SolidBrush BgrBursh = new SolidBrush(Color.FromArgb(170, 170, 170));
        public static readonly SolidBrush BlackBrush = new SolidBrush(Color.FromArgb(0, 0, 0));
        public static readonly Pen BlackPen = new Pen(Color.FromArgb(0, 0, 0), 1f * Scale);
        public static readonly Pen BlackPen1 = new Pen(Color.FromArgb(0, 0, 0), 2f * Scale);
        public static readonly Pen BlackPen2 = new Pen(Color.FromArgb(0, 0, 0), 3f * Scale);
        public static readonly SolidBrush BlueBrush = new SolidBrush(Color.FromArgb(0, 0, 0xff));
        public static readonly Pen BluePen = new Pen(Color.FromArgb(0, 0, 0xff));
        public static readonly SolidBrush DarkGreyBrush = new SolidBrush(Color.FromArgb(0x61, 0x70, 0x83));
        public static readonly Pen DarkGreyPen = new Pen(Color.FromArgb(0x55, 0x55, 0x55));
        public static readonly StringFormat DrawFormat = new StringFormat();
        public static readonly Font Font10 = new Font("宋体", 10f * Scale);
        public static readonly Font Font10B = new Font("宋体", 10f * Scale, Scale >= 1f ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font12 = new Font("宋体", 12f * Scale);
        public static readonly Font Font12B = new Font("宋体", 12f * Scale, Scale >= 1f ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font14 = new Font("宋体", 14f * Scale);
        public static readonly Font Font14B = new Font("宋体", 14f * Scale, Scale >= 1f ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font16 = new Font("宋体", 16f * Scale);
        public static readonly Font Font16B = new Font("宋体", 16f * Scale, Scale >= 1f ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font18 = new Font("宋体", 18f * Scale);
        public static readonly Font Font18B = new Font("宋体", 18f * Scale, Scale >= 1f ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font20 = new Font("宋体", 20f * Scale);
        public static readonly Font Font20B = new Font("宋体", 20f * Scale, Scale >= 1f ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font22 = new Font("宋体", 22f * Scale);
        public static readonly Font Font22B = new Font("宋体", 22f * Scale, Scale >= 1f ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font24 = new Font("宋体", 24f * Scale);
        public static readonly Font Font24B = new Font("宋体", 24f * Scale, Scale >= 1f ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font26 = new Font("宋体", 26f * Scale);
        public static readonly Font Font26B = new Font("宋体", 26f * Scale, Scale >= 1f ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font32 = new Font("宋体", 32f * Scale);
        public static readonly Font Font32B = new Font("宋体", 32f * Scale, Scale >= 1f ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font34 = new Font("宋体", 34f * Scale);
        public static readonly Font Font34B = new Font("宋体", 34f * Scale, Scale >= 1f ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font38 = new Font("宋体", 38f * Scale);
        public static readonly Font Font38B = new Font("宋体", 38f * Scale, Scale >= 1f ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font64 = new Font("宋体", 64f * Scale);
        public static readonly Font Font64B = new Font("宋体", 64f * Scale, Scale >= 1f ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font8 = new Font("宋体", 8f * Scale);
        public static readonly Font Font8B = new Font("宋体", 8f * Scale, Scale >= 1f ? FontStyle.Bold : FontStyle.Regular);
        public static readonly SolidBrush GreenBrush = new SolidBrush(Color.FromArgb(0, 0xff, 0));
        public static readonly SolidBrush GreyBrus = new SolidBrush(Color.FromArgb(0x8f, 0x94, 0x90));
        public static readonly SolidBrush GreyBrush = new SolidBrush(Color.FromArgb(0x55, 0x55, 0x55));
        public static readonly SolidBrush GreyBrushsBrush = new SolidBrush(Color.FromArgb(210, 210, 210));
        public static readonly SolidBrush GreyWhite = new SolidBrush(Color.FromArgb(0xe1, 0xe3, 0xf8));
        public static readonly SolidBrush HalfPGreySolidBrush = new SolidBrush(Color.FromArgb(0x80, 0x80, 0x80));
        public static readonly StringFormat LeftFormat;
        public static readonly SolidBrush LightGreenBrush = new SolidBrush(Color.FromArgb(0x2d, 200, 0x33));
        public static readonly Pen MediumGreyPen = new Pen(Color.FromArgb(150, 150, 150), 3f * Scale);
        public static readonly SolidBrush MediumGreySolidBrush = new SolidBrush(Color.FromArgb(150, 150, 150));
        public static readonly StringFormat MFormat;
        public static readonly SolidBrush OrangeBrush = new SolidBrush(Color.Orange);
        public static readonly SolidBrush RedBrush = new SolidBrush(Color.FromArgb(0xbf, 0, 2));
        public static readonly Pen RedPen = new Pen(Color.FromArgb(0xbf, 0, 2));
        public static readonly Pen RedPen3 = new Pen(Color.FromArgb(0xbf, 0, 2), 3f * Scale);
        public static readonly StringFormat RightFormat;
        public static readonly string[] Str1 = { "Cl", "选择", "轴速度" };
        public static readonly string[] Str2 = new string[0];
        public const string StrFont = "宋体";
        public static readonly SolidBrush ThinBlue = new SolidBrush(Color.FromArgb(0, 0x6d, 0xbd));
        public static readonly SolidBrush ThinGreys = new SolidBrush(Color.FromArgb(0xc0, 0xc0, 0xc0));
        public static readonly SolidBrush ThinReds = new SolidBrush(Color.FromArgb(0xff, 0, 0xff));
        public static readonly SolidBrush WhiteBrush = new SolidBrush(Color.FromArgb(0xff, 0xff, 0xff));
        public static readonly Pen WhitePen = new Pen(Color.FromArgb(0xff, 0xff, 0xff), 1f * Scale);
        public static readonly Pen WhitePen1 = new Pen(Color.FromArgb(0xff, 0xff, 0xff), 2f * Scale);
        public static readonly Pen WhitePen2 = new Pen(Color.FromArgb(0xff, 0xff, 0xff), 3f * Scale);
        public static readonly Pen YelloPen = new Pen(Color.FromArgb(0xff, 0xff, 0));
        public static readonly SolidBrush YellowBrush = new SolidBrush(Color.FromArgb(0xff, 0xff, 0));
        private static float m_Scale;

        // Methods
        static CommonRes()
        {
            
            var format = new StringFormat
            {
                Alignment = StringAlignment.Far,
                LineAlignment = StringAlignment.Far
            };
            RightFormat = format;
            var format2 = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Near
            };
            LeftFormat = format2;
            var format3 = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            MFormat = format3;
    
        }


    }
}