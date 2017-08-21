using System.Drawing;
using Engine.TCMS.HXD3D.底层共用;

namespace Engine.TCMS.HXD3D.HXD3D
{
    public static class FontsItems
    {
        // Fields
        public static readonly Font DefaultFont = new Font("宋体", 10.5f * Coordinate.Scaling, FontStyle.Regular);

        private static Font m_FontEx;

        // Methods
        public static Font Fonts_Regular(FontName strFont, float fontNumb, bool isBold)
        {
            m_FontEx = new Font(strFont.ToString(), fontNumb * Coordinate.Scaling, GetFontStyle(isBold) ? FontStyle.Bold : FontStyle.Regular);
            return m_FontEx;
        }

        private static bool GetFontStyle(bool isBold)
        {
            if (Coordinate.Scaling < 1f)
            {
                return false;
            }
            return isBold;
        }

        public static StringFormat TheAlignment(FontRelated fontrelate)
        {
            var format = new StringFormat();
            switch (fontrelate)
            {
                case FontRelated.居中:
                    format.LineAlignment = StringAlignment.Center;
                    format.Alignment = StringAlignment.Center;
                    return format;

                case FontRelated.靠左:
                    format.LineAlignment = StringAlignment.Center;
                    format.Alignment = StringAlignment.Near;
                    return format;

                case FontRelated.靠右:
                    format.LineAlignment = StringAlignment.Center;
                    format.Alignment = StringAlignment.Far;
                    return format;

                case FontRelated.靠左上:
                    format.LineAlignment = StringAlignment.Near;
                    format.Alignment = StringAlignment.Near;
                    return format;
            }
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;
            return format;
        }
    }
}