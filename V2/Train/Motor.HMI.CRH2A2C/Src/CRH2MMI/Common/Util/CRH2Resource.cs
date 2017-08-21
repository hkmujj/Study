using System;
using System.Drawing;

namespace CRH2MMI.Common.Util
{
    /// <summary>
    /// 公共资源类
    /// </summary>
    public class CRH2Resource
    {   /// <summary>
        /// 全局变量 
        /// </summary>
        public const int DirectionRightToLeft = 1;
        public const int Center = 2;
        public const int DirectionLeftToRight = 0;
        public const String StrFont = "Arial";
        public const int LineAlignmentCenter = 1;

        #region 绘制相关 画笔、画刷、字体
        public static Pen WhitePen = new Pen(Color.FromArgb(255, 255, 255), 1);
        public static Pen GreenPen = new Pen(Color.FromArgb(0, 255, 0), 2);
        public static Pen RedPen = new Pen(Color.FromArgb(255, 0, 0), 2);
        public static Pen PurplePen = new Pen(Color.FromArgb(255, 0, 255), 1);

        public static SolidBrush WhiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        public static SolidBrush BlackBrush = new SolidBrush(Color.FromArgb(0, 0, 0));
        public static SolidBrush RedBrush = new SolidBrush(Color.FromArgb(255, 0, 0));
        public static SolidBrush GreenBrush = new SolidBrush(Color.FromArgb(0, 255, 0));
        public static SolidBrush YellowBrush = new SolidBrush(Color.FromArgb(255, 255, 0));
        public static SolidBrush PurpleBrush = new SolidBrush(Color.FromArgb(255, 0, 255));

        public static SolidBrush WWBrush = new SolidBrush(Color.FromArgb(0, 255, 255));


        public static readonly Color ActiveBkColor = Color.GreenYellow;
        public static readonly Color DeactiveBkColor = Color.White;

        public static Font Font8 = new Font(StrFont, 8);
        public static Font Font10 = new Font(StrFont, 10f);
        public static Font Font12 = new Font(StrFont, 12f);
        public static Font Font13 = new Font(StrFont, 13f);
        public static Font Font14 = new Font(StrFont, 14f);

        public static Font Font16 = new Font(StrFont, 16f);
        public static Font Font24 = new Font(StrFont, 24f);
        public static Font Font32 = new Font(StrFont, 32f);
        public static Font Font64 = new Font(StrFont, 64f);
        public static StringFormat DrawFormat = new StringFormat();

        public static StringFormat RightFormat = new StringFormat();
        #endregion


    }
}