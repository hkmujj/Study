using System.Drawing;

namespace Motor.ATP._200H
{
    // ReSharper disable once InconsistentNaming
    public class Common2D
    {
        public static readonly Rectangle rectposition = new Rectangle(2, 2, 800, 600);

        #region ;;;;;;;;;;信息显示区域设置;;;;;;;;;;;;;;;;;;;;;;;;;
        public static readonly Rectangle[] RectA = new Rectangle[3];//A区位置
        public static readonly Rectangle[] RectB = new Rectangle[6];//B区
        public static readonly Rectangle[] RectC = new Rectangle[9];//C区
        public static readonly Rectangle[] RectD = new Rectangle[5];//D区
        public static readonly Rectangle[] RectE = new Rectangle[24];//E区
        public static readonly Rectangle[] RectF = new Rectangle[8];//F区
        public static readonly RectText[] GText = new RectText[8];

        public static readonly RectText MeunTitle;//子菜单 的标题栏

        static Common2D()
        {
            #region;;;;;;;;A  区;;;;;;;;;;;;;;
            RectA[0] = new Rectangle(rectposition.X, rectposition.Y, 67, 67);
            RectA[1] = new Rectangle(rectposition.X, rectposition.Y + 67, 67, 278);
            RectA[2] = new Rectangle(rectposition.X, rectposition.Y + 345, 67, 30);
            #endregion

            #region ;;;;;;;;;B 区;;;;;;;;;;;;;;;;;;
            RectB[0] = new Rectangle(rectposition.X + RectA[0].Width, rectposition.Y, 350, 375);
            RectB[1] = new Rectangle(rectposition.X + RectA[0].Width + 15, rectposition.Y + 310, 45, 45);
            for (int i = 2; i < 5; i++)
            {
                RectB[i] = new Rectangle(rectposition.X + RectA[0].Width + 45 * (i - 2) + 75, rectposition.Y + 310, 45, 45);
            }
            RectB[5] = new Rectangle(rectposition.X + RectA[0].Width + 45 * 5, rectposition.Y + 310, 65, 45);
            #endregion

            #region ;;;;;;;;;C  区:::::::::::::::::::
            RectC[0] = new Rectangle(rectposition.X + 199, rectposition.Y + 375, 87, 67);
            for (int i = 1; i < 4; i++)
            {
                RectC[i] = new Rectangle(rectposition.X + 286 + 44 * (i - 1), rectposition.Y + 375, 44, 67);
            }
            for (int i = 4; i < 7; i++)
            {
                RectC[i] = new Rectangle(rectposition.X + 67 + 44 * (i - 4), rectposition.Y + 375, 44, 67);
            }
            for (int i = 7; i < 9; i++)
            {
                RectC[i] = new Rectangle(rectposition.X, rectposition.Y + 375 + 34 * (i - 7), 67, 33);
            }
            #endregion

            #region;;;;;;;;;;D  区;;;;;;;;;;;;;
            RectD[0] = new Rectangle(rectposition.X + RectA[0].Width + RectB[0].Width, rectposition.Y + 2, 300, 300);
            #endregion

            #region;;;;;;;;;E 区;;;;;;;;;;;;;;;;
            for (int i = 0; i < 4; i++)
            {
                RectE[i] = new Rectangle(rectposition.X, rectposition.Y + 442 + i * 40, 67, 40);
            }
            RectE[4] = new Rectangle(rectposition.X + 67, rectposition.Y + 442, 290, 35);
            for (int i = 5; i < 9; i++)
            {
                RectE[i] = new Rectangle(rectposition.X + 67, rectposition.Y + 442 + (i - 5) * 31 + 35, 290, 31);
            }
            for (int i = 9; i < 15; i++)
            {
                RectE[i] = new Rectangle(rectposition.X + 357 + (i - 9) * 62, rectposition.Y + 442, 62, 62);
            }

            for (int i = 15; i < 20; i++)
            {
                RectE[i] = new Rectangle(rectposition.X + 419 + (i - 15) * 62, rectposition.Y + 375, 62, 67);
            }
            RectE[20] = new Rectangle(rectposition.X + 357, rectposition.Y + 504, 62, 97);
            RectE[21] = new Rectangle(rectposition.X + 419, rectposition.Y + 504, 82, 97);
            RectE[22] = new Rectangle(rectposition.X + 501, rectposition.Y + 504, 145, 97);
            RectE[23] = new Rectangle(rectposition.X + 646, rectposition.Y + 504, 82, 97);

            #endregion

            #region;;;;;;;;F 区;;;;;;;;;;;;;;
            for (int i = 0; i < 8; i++)
            {
                RectF[i] = new Rectangle(rectposition.X + 728, rectposition.Y + 75 * i, 68, 75);
            }
            #endregion

            #region ;;;;;;子 菜 单 标 题 栏 的 初 始 化
            MeunTitle = new RectText();
            MeunTitle.SetBkColor(0, 0, 0);
            MeunTitle.SetTextColor(255, 255, 255);
            MeunTitle.SetTextStyle(16, FormatStyle.Center, true, "宋体");
            MeunTitle.SetTextRect(RectD[0].X + 15, RectD[0].Y + 10, 80, 30);

            #endregion
        }

        #endregion

        #region ;;;;;;可 重 用  的 画 笔;;;;;;;;;;;;
        public static readonly Pen LinePen = new Pen(Color.FromArgb(20, 20, 20), 1);//绘制网格的画笔
        //public static readonly Pen linePen=new Pen(Color.FromArgb(173,184,196),2);//绘制网格的画笔
        public static readonly Pen GrayPen2 = new Pen(Color.FromArgb(192, 192, 192), 2);
        public static readonly Pen GrayPen3 = new Pen(Color.FromArgb(192, 192, 192), 3);
        public static readonly Pen GrayPen4 = new Pen(Color.FromArgb(192, 192, 192), 4);

        public static readonly Pen GrayPen12 = new Pen(Color.FromArgb(192, 192, 192), 12);
        public static readonly Pen GrayPen20 = new Pen(Color.FromArgb(192, 192, 192), 20);

        public static readonly Pen DarkGrayPen12 = new Pen(Color.FromArgb(85, 85, 85), 12);
        public static readonly Pen DarkGrayPen20 = new Pen(Color.FromArgb(85, 85, 85), 20);


        public static readonly Pen WhitePen2 = new Pen(Color.FromArgb(255, 255, 255), 2);
        public static readonly Pen WhitePen3 = new Pen(Color.FromArgb(255, 255, 255), 3);
        public static readonly Pen WhitePen4 = new Pen(Color.FromArgb(255, 255, 255), 4);

        public static readonly Pen YellowPen3 = new Pen(Color.FromArgb(255, 255, 0), 3);
        public static readonly Pen YellowPen12 = new Pen(Color.FromArgb(255, 255, 0), 12);
        public static readonly Pen YellowPen20 = new Pen(Color.FromArgb(255, 255, 0), 20);

        public static readonly Pen RedPen12 = new Pen(Color.FromArgb(255, 0, 0), 12);
        public static readonly Pen RedPen18 = new Pen(Color.FromArgb(255, 0, 0), 18);
        public static readonly Pen RedPen20 = new Pen(Color.FromArgb(255, 0, 0), 20);

        public static readonly Pen OrangePen20 = new Pen(Color.FromArgb(255, 128, 64), 20);
        public static readonly Pen YellowlightPen2 = new Pen(Color.FromArgb(255, 255, 0), 2);

        public static readonly Pen VoicePen = new Pen(Color.FromArgb(32, 147, 49));

        #endregion

        #region 可 重 用 的 画 刷
        public static readonly SolidBrush GrayBrush = new SolidBrush(Color.FromArgb(192, 192, 192));
        public static readonly SolidBrush DarkGrayBrush = new SolidBrush(Color.FromArgb(95, 95, 95));
        public static readonly SolidBrush WhiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        public static readonly SolidBrush BlackBrush = new SolidBrush(Color.FromArgb(0, 0, 0));
        public static readonly SolidBrush DarkBlueBrush = new SolidBrush(Color.FromArgb(28, 81, 194));
        public static readonly SolidBrush LightBlueBrush = new SolidBrush(Color.FromArgb(69, 170, 255));
        public static readonly SolidBrush YellowlightBrush = new SolidBrush(Color.FromArgb(239, 239, 141));
        #endregion

        #region ;;;;;;;;;;;;可 从 用 的 画 笔;;;;;;;;;;;;;;;;;;;;;
        public static readonly Font Font10B = new Font("宋体", 10, FontStyle.Bold);
        public static readonly Font Font12B = new Font("宋体", 12, FontStyle.Bold);
        public static readonly Font Font14B = new Font("宋体", 14, FontStyle.Bold);
        public static readonly Font Font16B = new Font("宋体", 16, FontStyle.Bold);
        public static readonly Font Font18B = new Font("宋体", 18, FontStyle.Bold);

        public static readonly Font Font12 = new Font("宋体", 12);
        public static readonly Font Font14 = new Font("宋体", 14);
        public static readonly Font Font16 = new Font("宋体", 14);
        #endregion

    }
}
