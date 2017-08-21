using System.Drawing;
using ATP200C.MainView;

namespace ATP200C.Common
{
    public class ATP200CCommon
    {
        public static Rectangle[] RectA { get; private set; }

        public static Rectangle[] RectB { get; private set; }

        public static Rectangle[] RectC { get; private set; }

        public static Rectangle[] RectD { get; private set; }

        public static Rectangle[] RectE { get; private set; }

        public static Rectangle[] RectAreaE { get; private set; }

        public static Rectangle[] RectF { get; private set; }

        public static GtRectText[] GText { get; private set; }

        public static GtRectText MeunTitle { get; private set; }

        public static Rectangle Rectposition { get; private set; }

        public static Pen LinePen { get; private set; }

        public static Pen GrayPen2 { get; private set; }

        public static Pen GrayPen3 { get; private set; }

        public static Pen GrayPen4 { get; private set; }

        public static Pen GrayPen12 { get; private set; }

        public static Pen GrayPen20 { get; private set; }

        public static Pen DarkGrayPen12 { get; private set; }

        public static Pen DarkGrayPen20 { get; private set; }

        public static Pen WhitePen2 { get; private set; }

        public static Pen WhitePen3 { get; private set; }

        public static Pen WhitePen4 { get; private set; }

        public static Pen YellowPen3 { get; private set; }

        public static Pen YellowPen12 { get; private set; }

        public static Pen YellowPen20 { get; private set; }

        public static Pen RedPen12 { get; private set; }

        public static Pen RedPen18 { get; private set; }

        public static Pen RedPen20 { get; private set; }

        public static Pen OrangePen20 { get; private set; }

        public static Pen YellowlightPen2 { get; private set; }

        public static SolidBrush GrayBrush { get; private set; }

        public static SolidBrush DarkGrayBrush { get; private set; }

        public static SolidBrush WhiteBrush { get; private set; }

        public static SolidBrush BlackBrush { get; private set; }

        public static SolidBrush DarkBlueBrush { get; private set; }

        public static SolidBrush LightBlueBrush { get; private set; }

        public static SolidBrush YellowlightBrush { get; private set; }

        public static Font Font10B { get; private set; }

        public static Font Font12B { get; private set; }

        public static Font Font14B { get; private set; }

        public static Font Font16B { get; private set; }

        public static Font Font18B { get; private set; }

        public static Font Font11 { get; private set; }

        public static Font Font12 { get; private set; }

        public static Font Font14 { get; private set; }

        public static Font Font16 { get; private set; }

        static ATP200CCommon()
        {
            Font16 = new Font("宋体", 14);
            Font14 = new Font("宋体", 14);
            Font12 = new Font("宋体", 12);
            Font11 = new Font("宋体", 11);
            Font18B = new Font("宋体", 18, FontStyle.Bold);
            Font16B = new Font("宋体", 16, FontStyle.Bold);
            Font14B = new Font("宋体", 14, FontStyle.Bold);
            Font12B = new Font("宋体", 12, FontStyle.Bold);
            Font10B = new Font("宋体", 10, FontStyle.Bold);
            YellowlightBrush = new SolidBrush(Color.FromArgb(239, 239, 141));
            LightBlueBrush = new SolidBrush(Color.FromArgb(69, 170, 255));
            DarkBlueBrush = new SolidBrush(Color.FromArgb(28, 81, 194));
            BlackBrush = new SolidBrush(Color.FromArgb(0, 0, 0));
            WhiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
            DarkGrayBrush = new SolidBrush(Color.FromArgb(95, 95, 95));
            GrayBrush = new SolidBrush(Color.FromArgb(192, 192, 192));
            YellowlightPen2 = new Pen(Color.FromArgb(255, 255, 0), 2);
            OrangePen20 = new Pen(Color.FromArgb(255, 128, 64), 20);
            RedPen20 = new Pen(Color.FromArgb(255, 0, 0), 20);
            RedPen18 = new Pen(Color.FromArgb(255, 0, 0), 18);
            RedPen12 = new Pen(Color.FromArgb(255, 0, 0), 12);
            YellowPen20 = new Pen(Color.FromArgb(255, 255, 0), 20);
            YellowPen12 = new Pen(Color.FromArgb(255, 255, 0), 12);
            YellowPen3 = new Pen(Color.FromArgb(255, 255, 0), 3);
            WhitePen4 = new Pen(Color.FromArgb(255, 255, 255), 4);
            WhitePen3 = new Pen(Color.FromArgb(255, 255, 255), 3);
            WhitePen2 = new Pen(Color.FromArgb(255, 255, 255), 2);
            DarkGrayPen20 = new Pen(Color.FromArgb(85, 85, 85), 20);
            DarkGrayPen12 = new Pen(Color.FromArgb(85, 85, 85), 12);
            GrayPen20 = new Pen(Color.FromArgb(192, 192, 192), 20);
            GrayPen12 = new Pen(Color.FromArgb(192, 192, 192), 12);
            GrayPen4 = new Pen(Color.FromArgb(192, 192, 192), 4);
            GrayPen3 = new Pen(Color.FromArgb(192, 192, 192), 3);
            GrayPen2 = new Pen(Color.FromArgb(192, 192, 192), 2);
            LinePen = new Pen(Color.White, 3);
            Rectposition = new Rectangle(2, 2, 800, 600);
            GText = new GtRectText[8];
            RectF = new Rectangle[8];
            RectAreaE = new Rectangle[24];
            RectE = new Rectangle[24];
            RectD = new Rectangle[5];
            RectC = new Rectangle[9];
            RectB = new Rectangle[6];
            RectA = new Rectangle[3];

            #region;;;;;;;;A  区;;;;;;;;;;;;;; 

            RectA[0] = new Rectangle(Rectposition.X, Rectposition.Y, 67, 67);
            RectA[1] = new Rectangle(Rectposition.X, Rectposition.Y + 67, 67, 278);
            RectA[2] = new Rectangle(Rectposition.X, Rectposition.Y + 345, 67, 30);

            #endregion

            #region ;;;;;;;;;B 区;;;;;;;;;;;;;;;;;;

            RectB[0] = new Rectangle(Rectposition.X + RectA[0].Width, Rectposition.Y, 350, 375);
            RectB[1] = new Rectangle(Rectposition.X + RectA[0].Width + 15, Rectposition.Y + 310, 45, 45);
            for (int i = 2; i < 5; i++)
            {
                RectB[i] = new Rectangle(Rectposition.X + RectA[0].Width + 45*(i - 2) + 75, Rectposition.Y + 310, 45, 45);
            }
            RectB[5] = new Rectangle(Rectposition.X + RectA[0].Width + 45*5, Rectposition.Y + 310, 65, 45);

            #endregion

            #region ;;;;;;;;;C  区:::::::::::::::::::

            RectC[0] = new Rectangle(Rectposition.X + 199, Rectposition.Y + 375, 87, 67);
            for (int i = 1; i < 4; i++)
            {
                RectC[i] = new Rectangle(Rectposition.X + 286 + 44*(i - 1), Rectposition.Y + 375, 44, 67);
            }
            for (int i = 4; i < 7; i++)
            {
                RectC[i] = new Rectangle(Rectposition.X + 67 + 44*(i - 4), Rectposition.Y + 375, 44, 67);
            }
            for (int i = 7; i < 9; i++)
            {
                RectC[i] = new Rectangle(Rectposition.X, Rectposition.Y + 375 + 34*(i - 7), 67, 33);
            }

            #endregion

            #region;;;;;;;;;;D  区;;;;;;;;;;;;;

            RectD[0] = new Rectangle(Rectposition.X + RectA[0].Width + RectB[0].Width, Rectposition.Y + 2, 300, 300);

            #endregion

            #region;;;;;;;;;E 区;;;;;;;;;;;;;;;;

            for (int i = 0; i < 4; i++)
            {
                RectE[i] = new Rectangle(Rectposition.X, Rectposition.Y + 442 + i*40, 67, 40);
            }
            RectE[4] = new Rectangle(Rectposition.X + 67, Rectposition.Y + 442, 290, 35);
            for (int i = 5; i < 9; i++)
            {
                RectE[i] = new Rectangle(Rectposition.X + 67, Rectposition.Y + 442 + (i - 5)*31 + 35, 290, 31);
            }
            for (int i = 9; i < 15; i++)
            {
                RectE[i] = new Rectangle(Rectposition.X + 357 + (i - 9)*62, Rectposition.Y + 442, 62, 62);
            }

            for (int i = 15; i < 20; i++)
            {
                RectE[i] = new Rectangle(Rectposition.X + 419 + (i - 15)*62, Rectposition.Y + 375, 62, 67);
            }
            RectE[20] = new Rectangle(Rectposition.X + 357, Rectposition.Y + 504, 62, 97);
            RectE[21] = new Rectangle(Rectposition.X + 419, Rectposition.Y + 504, 82, 97);
            RectE[22] = new Rectangle(Rectposition.X + 501, Rectposition.Y + 504, 145, 97);
            RectE[23] = new Rectangle(Rectposition.X + 646, Rectposition.Y + 504, 82, 97);



            for (int i = 0; i < 2; i++)
            {
                RectAreaE[i] = new Rectangle(Rectposition.X + 362, Rectposition.Y + 480 + i*56, 45, 53);

                RectAreaE[2 + i] = new Rectangle(Rectposition.X + 420, Rectposition.Y + 545 + i*21, 60, 21);
                RectAreaE[4 + i] = new Rectangle(Rectposition.X + 480, Rectposition.Y + 545 + i*21, 90, 21);
            }

            RectAreaE[6] = new Rectangle(Rectposition.X + 640, Rectposition.Y + 545, 80, 42);

            #endregion

            #region;;;;;;;;F 区;;;;;;;;;;;;;;

            for (int i = 0; i < 8; i++)
            {
                RectF[i] = new Rectangle(Rectposition.X + 728, Rectposition.Y + 75*i, 68, 68);
            }

            #endregion

            #region ;;;;;;子 菜 单 标 题 栏 的 初 始 化

            MeunTitle = new GtRectText();
            MeunTitle.SetBkColor(0, 0, 0);
            MeunTitle.SetTextColor(255, 255, 255);
            MeunTitle.SetTextStyle(16, FormatStyle.Center, true, "宋体");
            MeunTitle.SetTextRect(RectD[0].X + 15, RectD[0].Y + 10, 80, 30);

            #endregion
        }

    }
}
