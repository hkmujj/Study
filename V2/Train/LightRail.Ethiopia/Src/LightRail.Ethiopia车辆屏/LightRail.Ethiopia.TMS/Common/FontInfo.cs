using System.Drawing;

namespace LightRail.Ethiopia.TMS.Common
{
    public class _FontStyle
    {
        public Font Font { get; set; }

        public StringFormat SF { get; set; }

        public SolidBrush SolidBrush { get; set; }
    }

    public class FontInfo
    {
        public static StringFormat SF_CC = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        public static StringFormat SF_CT = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near };
        public static StringFormat SF_LC = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
        public static StringFormat SF_LT = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near };
        public static StringFormat SF_RC = new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };
        public static StringFormat SF_LB = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Far };

        public static _FontStyle FontStyle_13_CC_B_Btn = new _FontStyle()
        {
            Font = new Font("宋体", 13),
            SF = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            SolidBrush = Brushs.BlackBrush
        };

        public static _FontStyle FontStyle_11_CC_B_Btn = new _FontStyle() { 
            Font = new Font("宋体", 11), 
            SF = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }, 
            SolidBrush = Brushs.BlackBrush
        };

        public static _FontStyle FontStyle_10_LC_B_Btn = new _FontStyle()
        {
            Font = new Font("宋体", 10),
            SF = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center },
            SolidBrush = Brushs.BlackBrush
        };

        public static _FontStyle FontStyle_105_CC_B_Btn = new _FontStyle()
        {
            Font = new Font("宋体", 10.5f),
            SF = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            SolidBrush = Brushs.BlackBrush
        };

        public static _FontStyle FontStyle_105_LC_W = new _FontStyle()
        {
            Font = new Font("宋体", 10.5f),
            SF = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center },
            SolidBrush = Brushs.WhiteBrush
        };

        public static _FontStyle FontStyle_10_CC_W_Btn = new _FontStyle()
        {
            Font = new Font("宋体", 10),
            SF = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            SolidBrush = Brushs.WhiteBrush
        };
    }
}
