using System.Drawing;

namespace NJ_MMI
{
    public class Global
    {
        public static Font Font_Arial_08_B = new Font("Arial", 08, FontStyle.Bold);
        public static Font Font_Arial_11_B = new Font("Arial", 11, FontStyle.Bold);
        public static Font Font_Arial_12_B = new Font("Arial", 12, FontStyle.Bold);
        public static Font Font_Arial_13_B = new Font("Arial", 13, FontStyle.Bold);
        public static Font Font_Arial_14_B = new Font("Arial", 14, FontStyle.Bold);
        public static Font Font_Arial_15_B = new Font("Arial", 15, FontStyle.Bold);
        public static Font Font_Arial_16_B = new Font("Arial", 16, FontStyle.Bold);
        public static Font Font_Arial_23 = new Font("Arial", 23);
        public static Font Font_Arial_28_B = new Font("Arial", 28, FontStyle.Bold);
        public static Font Font_Arial_28 = new Font("Arial", 28);
        public static Font Font_Arial_30_B = new Font("Arial", 30, FontStyle.Bold);

        public static Font font=new Font("宋体", 12, FontStyle.Bold);
        public static Font font1 = new Font("宋体", 10, FontStyle.Bold);

        public static Brush brush1 = new SolidBrush(Color.White);
        public static Brush brush2 = new SolidBrush(Color.Blue);
        public static Brush brush3 = new SolidBrush(Color.Red);
        public static Brush brush4 = new SolidBrush(Color.YellowGreen);
        public static Brush brush5 = new SolidBrush(Color.SkyBlue);

        public static StringFormat SF_CC = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        public static StringFormat SF_NC = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
        public static StringFormat SF_FC = new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };
        public static StringFormat SF_NF = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Far };
        public static StringFormat SF_FN = new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Near };
        public static StringFormat SF_CF = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Far };
        public static StringFormat SF_CN = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near };
        public static StringFormat SF_NN = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near };
        public static StringFormat SF_FF = new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Far };

        public static SolidBrush SB_Blue = new SolidBrush(Color.FromArgb(72, 174, 250));
    }
}
