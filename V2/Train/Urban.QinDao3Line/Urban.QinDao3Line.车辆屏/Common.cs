using System;
using System.Drawing;

namespace Urban.QingDao3Line.MMI
{
    class Common
    {
        private static readonly float m_Scale = 1.0f;
        /// <summary>
        /// 比例尺寸
        /// </summary>
        public static float Scale { get { return m_Scale; } }

        private static readonly int m_ScreenMoveX = 0;
        /// <summary>
        /// 屏横向移动像素
        /// </summary>
        public static int ScreenMoveX { get { return m_ScreenMoveX; } }

        private static readonly int m_ScreenMoveY = 0;
        /// <summary>
        /// 屏纵向移动像素
        /// </summary>
        public static int ScreenMoveY { get { return m_ScreenMoveY; } }

        #region :::::::::::::::::::::::::::::: 线条 :::::::::::::::::::::::::::::::::::::::
        /// <summary>
        /// 宽1白线
        /// </summary>
        public static Pen m_WhitePen = new Pen(Color.FromArgb(255, 255, 255), 1 * m_Scale);
        /// <summary>
        /// 宽2白线
        /// </summary>
        public static Pen m_WhitePen1 = new Pen(Color.FromArgb(255, 255, 255), 2 * m_Scale);
        /// <summary>
        /// 宽3白线
        /// </summary>
        public static Pen m_WhitePen2 = new Pen(Color.FromArgb(255, 255, 255), 3 * m_Scale);


        /// <summary>
        /// 宽1黑线
        /// </summary>
        public static Pen m_BlackPen = new Pen(Color.FromArgb(0, 0, 0), 1 * m_Scale);
        public static Pen m_BlackPen1 = new Pen(Color.FromArgb(0, 0, 0), 2 * m_Scale);
        public static Pen m_BlackPen2 = new Pen(Color.FromArgb(0, 0, 0), 3 * m_Scale);

        /// <summary>
        /// 宽3中灰
        /// </summary>
        public static Pen m_MediumGreyPen = new Pen(Color.FromArgb(150, 150, 150), 3 * m_Scale);

        /// <summary>
        /// 宽1深灰
        /// </summary>
        public static Pen m_DarkGreyPen = new Pen(Color.FromArgb(85, 85, 85));
        /// <summary>
        /// 红1
        /// </summary>
        public static Pen m_RedPen = new Pen(Color.FromArgb(191, 0, 2));
        /// <summary>
        /// 红2
        /// </summary>
        public static Pen m_RedPen2 = new Pen(Color.FromArgb(191, 0, 2), 2 * m_Scale);
        /// <summary>
        /// 红3
        /// </summary>
        public static Pen m_RedPen3 = new Pen(Color.FromArgb(191, 0, 2), 3 * m_Scale);
        /// <summary>
        /// 蓝1
        /// </summary>
        public static Pen m_BluePen = new Pen(Color.FromArgb(0, 0, 255));
        /// <summary>
        /// 蓝2
        /// </summary>
        public static Pen m_BluePen3 = new Pen(Color.FromArgb(0, 0, 255),3*m_Scale);
        /// <summary>
        /// 黄1
        /// </summary>
        public static Pen m_YelloPen = new Pen(Color.FromArgb(255, 255, 0));
        /// <summary>
        /// 黄3
        /// </summary>
        public static Pen m_YelloPen3 = new Pen(Color.FromArgb(255, 255, 0), 3 * m_Scale);


        #endregion

        #region :::::::::::::::::::::::::::::: 颜色 :::::::::::::::::::::::::::::::::::::::
        /// <summary>
        /// 白色
        /// </summary>
        public static SolidBrush m_WhiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255));

        /// <summary>
        /// 黑色
        /// </summary>
        public static SolidBrush m_BlackBrush = new SolidBrush(Color.FromArgb(0, 0, 0));

        /// <summary>
        /// 淡绿
        /// </summary>
        public static SolidBrush m_LightGreenBrush = new SolidBrush(Color.FromArgb(45, 200, 51));

        /// <summary>
        /// 深灰
        /// </summary>
        public static SolidBrush m_DarkGreyBrush = new SolidBrush(Color.FromArgb(97, 112, 131));

        /// <summary>
        /// 中灰
        /// </summary>
        public static SolidBrush m_MediumGreySolidBrush = new SolidBrush(Color.FromArgb(150, 150, 150));

        /// <summary>
        /// 50%灰
        /// </summary>
        public static SolidBrush m_HalfPGreySolidBrush = new SolidBrush(Color.FromArgb(128, 128, 128));

        /// <summary>
        /// 黄色
        /// </summary>
        public static SolidBrush m_YellowBrush = new SolidBrush(Color.FromArgb(255, 255, 0));

        /// <summary>
        /// 淡黄色
        /// </summary>
        public static SolidBrush m_SoftYellowBrush = new SolidBrush(Color.FromArgb(247, 240, 213));

        /// <summary>
        /// 桔黄色
        /// </summary>
        public static SolidBrush m_OrangeBrush = new SolidBrush(Color.Orange);

        /// <summary>
        /// 红色
        /// </summary>
        public static SolidBrush m_RedBrush = new SolidBrush(Color.FromArgb(191, 0, 2));

        public static SolidBrush m_RedBrush1 = new SolidBrush(Color.FromArgb(255, 0, 0));

        /// <summary>
        /// 蓝色
        /// </summary>
        public static SolidBrush m_BlueBrush = new SolidBrush(Color.FromArgb(0, 0, 255));
        /// <summary>
        /// 浅灰
        /// </summary>
        public static SolidBrush m_GreyBrush = new SolidBrush(Color.FromArgb(85, 85, 85));
        /// <summary>
        /// 绿色
        /// </summary>
        public static SolidBrush m_GreenBrush = new SolidBrush(Color.FromArgb(0, 255, 0));
        /// <summary>
        /// 白灰
        /// </summary>
        public static SolidBrush m_BgrBursh = new SolidBrush(Color.FromArgb(170, 170, 170));
        /// <summary>
        ///淡蓝色
        /// </summary>
        public static SolidBrush m_ThinBlue = new SolidBrush(Color.FromArgb(0, 109, 189));
        /// <summary>
        /// 灰
        /// </summary>
        public static SolidBrush m_GreyBrushsBrush = new SolidBrush(Color.FromArgb(210, 210, 210));
        /// <summary>
        /// 浅灰
        /// </summary>
        public static SolidBrush m_GreyBrus = new SolidBrush(Color.FromArgb(143, 148, 144));
        /// <summary>
        /// 灰白
        /// </summary>
        public static SolidBrush m_GreyWhite = new SolidBrush(Color.FromArgb(225, 227, 248));
        /// <summary>
        /// 浅灰
        /// </summary>
        public static SolidBrush m_ThinGreys = new SolidBrush(Color.FromArgb(192, 192, 192));

        #endregion

        #region :::::::::::::::::::::::::::::: 字体 :::::::::::::::::::::::::::::::::::::::
        public const string StrFont = "宋体";
        public static Font m_Font8 = new Font(StrFont, 8f * m_Scale);
        public static Font m_Font9 = new Font(StrFont, 9f * m_Scale);
        public static Font m_Font10 = new Font(StrFont, 10f * m_Scale);
        public static Font m_Font11 = new Font(StrFont, 11f * m_Scale);
        public static Font m_Font12 = new Font(StrFont, 12f * m_Scale);
        public static Font m_Font14 = new Font(StrFont, 14f * m_Scale);
        public static Font m_Font16 = new Font(StrFont, 16f * m_Scale);
        public static Font m_Font18 = new Font(StrFont, 18f * m_Scale);
        public static Font m_Font20 = new Font(StrFont, 20f * m_Scale);
        public static Font m_Font22 = new Font(StrFont, 22f * m_Scale);
        public static Font m_Font24 = new Font(StrFont, 24f * m_Scale);
        public static Font m_Font26 = new Font(StrFont, 26f * m_Scale);
        public static Font m_Font32 = new Font(StrFont, 32f * m_Scale);
        public static Font m_Font34 = new Font(StrFont, 34f * m_Scale);
        public static Font m_Font38 = new Font(StrFont, 38f * m_Scale);
        public static Font m_Font64 = new Font(StrFont, 64f * m_Scale);

        public static Font m_Font8B = new Font(StrFont, 8f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font9B = new Font(StrFont, 9f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font10B = new Font(StrFont, 10f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font11B = new Font(StrFont, 11f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font12B = new Font(StrFont, 12f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font14B = new Font(StrFont, 14f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font16B = new Font(StrFont, 16f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font18B = new Font(StrFont, 18f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font20B = new Font(StrFont, 20f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font22B = new Font(StrFont, 22f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font24B = new Font(StrFont, 24f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font26B = new Font(StrFont, 26f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font32B = new Font(StrFont, 32f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font34B = new Font(StrFont, 34f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font38B = new Font(StrFont, 38f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font64B = new Font(StrFont, 64f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);

        public static Font underline_Font10B = new Font(StrFont, 10f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Underline);
        public static Font underline_Font11B = new Font(StrFont, 11f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Underline);
        public static Font underline_Font12B = new Font(StrFont, 12f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Underline);
        #endregion

        #region :::::::::::::::::::::::::::::: 文字 :::::::::::::::::::::::::::::::::::::::
 
        public static string[] m_Str2 =
        {
            "Urban.QingDao3Line.MMI railway station", "People Hall", "Huiquan Sp", "Zhongshan", "Taipingjiao l",
            "Zhan Shan", "Wusi Squan", "Rushan Suo", "Ningxia Lu", "DunHua Lu", "Cuobu Ling", "Qingjiang Lu",
            "Shuang Sha", "Bao Er", "He Xi", "He Dong", "Wannian Quan Lu", "Li Cun", "Junfeng Lu", "Xiliu Zhuang",
            "Yongping Lu", "North Railway Station"
        };
        #endregion
        #region 字体对齐方式
        public static StringFormat m_DrawFormat = new StringFormat();
        public static StringFormat m_RightFormat = new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Far };
        public static StringFormat m_RightCenterFormat = new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };
        public static StringFormat m_RightBottomFormat = new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Near };

        public static StringFormat m_LeftFormat = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near };
        public static StringFormat m_MFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        public static StringFormat m_LeftCenterFormat = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Far };
        public static StringFormat m_LeftMidumFormat = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center};
        public static StringFormat m_CenterFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Far };
        public static StringFormat m_TopFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near };
        public static StringFormat m_BottomFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Far };

        #endregion
    }
}
