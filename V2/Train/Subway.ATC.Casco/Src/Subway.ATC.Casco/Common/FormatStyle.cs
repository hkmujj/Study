using System.Drawing;

namespace Subway.ATC.Casco.Common
{
    /// <summary>
    /// 字体画笔线条格式
    /// </summary>
    public class FormatStyle
    {
        public static int menu = 0;
        public const int Center = 2;
        public const string strFont = "Arial";

        //线条
        //白色
        public static Pen WhitePen = new Pen(Color.FromArgb(255, 255, 255), 1);
        public static Pen WhitePen2 = new Pen(Color.FromArgb(255, 255, 255), 2);
        public static Pen WhitePen3 = new Pen(Color.FromArgb(255, 255, 255), 3);
        public static Pen WhitePen4 = new Pen(Color.FromArgb(255, 255, 255), 4);
        //黑色
        public static Pen BlackPen = new Pen(Color.FromArgb(0, 0, 0), 1);
        public static Pen BlackPen2 = new Pen(Color.FromArgb(0, 0, 0), 2);
        //浅灰、中灰、深灰
        public static Pen LightGreyPen = new Pen(Color.FromArgb(195, 195, 195));
        public static Pen MediumGreyPen = new Pen(Color.FromArgb(150, 150, 150));
        public static Pen DarkGreyPen = new Pen(Color.FromArgb(85, 85, 85));
        //蓝色、深蓝
        public static Pen BluePen = new Pen(Color.FromArgb(3, 17, 34));
        public static Pen VeryDarkBluePen = new Pen(Color.FromArgb(4, 12, 25));
        //淡绿、深绿
        public static Pen LightGreenPen = new Pen(Color.FromArgb(45, 144, 51));
        public static Pen LightGreenPen_2 = new Pen(Color.FromArgb(45, 144, 51), 2);
        public static Pen DarkGreenPen = new Pen(Color.FromArgb(12, 58, 12));
        //黄色、深黄色、橙色
        public static Pen YellowPen = new Pen(Color.FromArgb(223, 223, 0), 2);
        public static Pen DarkYellowPen = new Pen(Color.FromArgb(117, 105, 0));
        public static Pen OrangePen = new Pen(Color.FromArgb(234, 145, 0));
        //红色
        public static Pen RedPen = new Pen(Color.FromArgb(191, 0, 2));
        public static Pen RedPen_2 = new Pen(Color.FromArgb(191, 0, 2), 2);
        //灰蓝色、蓝灰色、淡蓝灰色
        public static Pen PurplePen = new Pen(Color.FromArgb(255, 0, 255), 1);
        public static Pen Gary_BluePen = new Pen(Color.FromArgb(81, 91, 109));
        public static Pen Blue_GrayPen = new Pen(Color.FromArgb(37, 69, 93));
        public static Pen LightBlue_GrayPen = new Pen(Color.FromArgb(128, 139, 158));

        //画笔
        //白色
        public static SolidBrush WhiteSolidBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        //黑色
        public static SolidBrush BlackSolidBrush = new SolidBrush(Color.FromArgb(0, 0, 0));
        //浅灰、中灰、深灰
        public static SolidBrush LightGreySolidBrush = new SolidBrush(Color.FromArgb(195, 195, 195));
        public static SolidBrush MediumGreySolidBrush = new SolidBrush(Color.FromArgb(150, 150, 150));
        public static SolidBrush DarkGreySolidBrush = new SolidBrush(Color.FromArgb(85, 85, 85));
        //蓝色、深蓝
        public static SolidBrush BlueSolidBrush = new SolidBrush(Color.FromArgb(3, 17, 34));
        public static SolidBrush VeryDarkBlueSolidBrush = new SolidBrush(Color.FromArgb(4, 12, 25));
        //淡绿、深绿
        public static SolidBrush LightGreenSolidBrush = new SolidBrush(Color.FromArgb(45, 144, 51));
        public static SolidBrush DarkGreenSolidBrush = new SolidBrush(Color.FromArgb(12, 58, 12));
        //青色
        public static SolidBrush BluenessSolidBrush = new SolidBrush(Color.FromArgb(0, 194, 194));
        //黄色、深黄色、橙色
        public static SolidBrush YellowSolidBrush = new SolidBrush(Color.FromArgb(223, 223, 0));
        public static SolidBrush DarkYellowSolidBrush = new SolidBrush(Color.FromArgb(117, 105, 0));
        public static SolidBrush OrangeSolidBrush = new SolidBrush(Color.FromArgb(234, 145, 0));
        //红色
        public static SolidBrush RedSolidBrush = new SolidBrush(Color.FromArgb(191, 0, 2));
        //灰蓝色、蓝灰色、淡蓝灰色
        public static SolidBrush Gary_BlueSolidBrush = new SolidBrush(Color.FromArgb(81, 91, 109));
        public static SolidBrush Blue_GraySolidBrush = new SolidBrush(Color.FromArgb(37, 69, 93));
        public static SolidBrush LightBlue_GraySolidBrush = new SolidBrush(Color.FromArgb(128, 139, 158));

        #region :::::::::::::::::::::::::::::::: 字体 ::::::::::::::::::::::::::::::::::::::::::
        public static Font Font10 = new Font(strFont, 10f);
        public static Font Font12 = new Font(strFont, 12f);
        public static Font Font14 = new Font(strFont, 14f);
        public static Font Font16 = new Font(strFont, 16f);
        public static Font Font18 = new Font(strFont, 18f);
        public static Font Font20 = new Font(strFont, 20f);
        public static Font Font22 = new Font(strFont, 22f);
        public static Font Font24 = new Font(strFont, 24f);
        public static Font Font26 = new Font(strFont, 26f);
        public static Font Font32 = new Font(strFont, 32f);
        public static Font Font34 = new Font(strFont, 34f);
        public static Font Font38 = new Font(strFont, 38f);
        public static Font Font64 = new Font(strFont, 64f);

        public static Font Font8 = new Font(strFont, 8f, FontStyle.Bold);
        public static Font Font10B = new Font(strFont, 10f, FontStyle.Bold);
        public static Font Font12B = new Font(strFont, 12f, FontStyle.Bold);
        public static Font Font14B = new Font(strFont, 14f, FontStyle.Bold);
        public static Font Font16B = new Font(strFont, 16f, FontStyle.Bold);
        public static Font Font18B = new Font(strFont, 18f, FontStyle.Bold);
        public static Font Font20B = new Font(strFont, 20f, FontStyle.Bold);
        public static Font Font22B = new Font(strFont, 22f, FontStyle.Bold);
        public static Font Font24B = new Font(strFont, 24f, FontStyle.Bold);
        public static Font Font26B = new Font(strFont, 26f, FontStyle.Bold);
        public static Font Font32B = new Font(strFont, 32f, FontStyle.Bold);
        public static Font Font34B = new Font(strFont, 34f, FontStyle.Bold);
        public static Font Font38B = new Font(strFont, 38f, FontStyle.Bold);
        public static Font Font64B = new Font(strFont, 64f, FontStyle.Bold);
        #endregion
    }
}