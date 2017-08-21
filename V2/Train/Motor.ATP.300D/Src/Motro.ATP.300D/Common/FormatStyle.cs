using System;
using System.Drawing;

namespace Motor.ATP._300D.Common
{
    public class FormatStyle
    {
        //int FC_X = FrameButton2D.FrameChange_X;
        //int FC_Y = FrameButton2D.FrameChange_Y;

        public static int Menu = 0;
        public const int DirectionRightToLeft = 1;
        public const int Center = 2;
        public const int DirectionLeftToRight = 0;
        public const String StrFont = "Arial";
        public const int LineAlignmentCenter = 1;
        public static SolidBrush BlueBrush = new SolidBrush(Color.FromArgb(0, 0, 255));
        public static SolidBrush PurpleBrush = new SolidBrush(Color.FromArgb(255, 0, 255));
        public static SolidBrush PBrush = new SolidBrush(Color.FromArgb(255, 128, 0));

        public static SolidBrush YellowBrush = new SolidBrush(Color.FromArgb(255, 255, 0));
        public static SolidBrush WhiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        public static SolidBrush BlackBrush = new SolidBrush(Color.FromArgb(0, 0, 0));
        public static Font Font12 = new Font(StrFont, 12f);
        public static Font Font14 = new Font(StrFont, 14f);
        public static Font Font16 = new Font(StrFont, 16f);
        public static Font Font30 = new Font(StrFont, 30f);
        public static Font Font30y = new Font("свт╡", 30f);
        

    }
}