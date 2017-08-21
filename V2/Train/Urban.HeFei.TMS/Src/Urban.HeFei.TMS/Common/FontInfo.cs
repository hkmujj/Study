using System.Drawing;

namespace Urban.HeFei.TMS.Common
{
    public class _FontStyle
    {
        public Font Font { get; set; }

        public StringFormat Sf { get; set; }

        public SolidBrush SolidBrush { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class FontInfo
    {
        public static StringFormat SfCc = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        public static StringFormat SfCt = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near };
        public static StringFormat SfCb = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Far };
        public static StringFormat SfLc = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
        public static StringFormat SfLt = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near };
        public static StringFormat SfRc = new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };
        public static StringFormat SfLb = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Far };

        public static _FontStyle FontStyle13CcBBtn = new _FontStyle()
        {
            Font = new Font("宋体", 13),
            Sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            SolidBrush = Brushs.BlackBrush
        };

        public static _FontStyle FontStyle11CcBBtn = new _FontStyle() { 
            Font = new Font("宋体", 11), 
            Sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }, 
            SolidBrush = Brushs.BlackBrush
        };

        public static _FontStyle FontStyle10LcBBtn = new _FontStyle()
        {
            Font = new Font("宋体", 10),
            Sf = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center },
            SolidBrush = Brushs.BlackBrush
        };

        public static _FontStyle FontStyle105CcBBtn = new _FontStyle()
        {
            Font = new Font("宋体", 10.5f),
            Sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            SolidBrush = Brushs.BlackBrush
        };

        public static _FontStyle FontStyle105LcW = new _FontStyle()
        {
            Font = new Font("宋体", 10.5f),
            Sf = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center },
            SolidBrush = Brushs.WhiteBrush
        };

        public static _FontStyle FontStyle10CcWBtn = new _FontStyle()
        {
            Font = new Font("宋体", 10),
            Sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            SolidBrush = Brushs.WhiteBrush
        };
    }
}
