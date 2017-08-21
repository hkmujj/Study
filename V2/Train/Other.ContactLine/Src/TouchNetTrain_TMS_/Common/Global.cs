using System.Drawing;

namespace TouchNetTrain_TMS_
{
    public class Global
    {
        public static Font Font_Verdana_10 = new Font("Verdana", 10);
        public static Font Font_Verdana_12 = new Font("Verdana", 12);
        public static Font Font_Verdana_13 = new Font("Verdana", 13);
        public static Font Font_Verdana_11_B = new Font("Verdana", 11, FontStyle.Bold);
        public static Font Font_Verdana_12_B = new Font("Verdana", 12, FontStyle.Bold);
        public static Font Font_Verdana_13_B = new Font("Verdana", 13, FontStyle.Bold);
        public static Font Font_Verdana_15_B = new Font("Verdana", 15, FontStyle.Bold);
        public static Font Font_Verdanal_16_B = new Font("Verdana", 16, FontStyle.Bold);
        public static Font Font_Verdanal_23 = new Font("Verdana", 23);
        public static Font Font_Verdana_28_B = new Font("Verdana", 28, FontStyle.Bold);
        public static Font Font_Verdanal_28 = new Font("Verdana", 28);
        public static Font Font_Verdana_30_B = new Font("Verdana", 30, FontStyle.Bold);

        public static StringFormat SF_CC = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        public static StringFormat SF_NC = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
        public static StringFormat SF_FC = new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };
        public static StringFormat SF_NF = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Far };
        public static StringFormat SF_FN = new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Near };
        public static StringFormat SF_CF = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Far };
        public static StringFormat SF_CN = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near };
        public static StringFormat SF_NN = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near };
        public static StringFormat SF_FF = new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Far };

        public static SolidBrush SB_Title = new SolidBrush(Color.FromArgb(126, 137, 118));
        public static SolidBrush SB_Blue = new SolidBrush(Color.FromArgb(104, 137, 227));

        public static Pen Pen_White = new Pen(Brushes.White);
        public static Pen Pen_White_2 = new Pen(Brushes.White, 2);
        public static Pen Pen_Yellow_2 = new Pen(Brushes.Yellow, 2);
    }
}
