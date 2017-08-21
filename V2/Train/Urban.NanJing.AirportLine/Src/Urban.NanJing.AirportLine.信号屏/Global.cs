using System.Drawing;

namespace Urban.NanJing.AirportLine.ATC.Casco
{
    public class Global
    {
        public static Font m_FontArial11B = new Font("Arial", 11, FontStyle.Bold);
        public static Font m_FontArial12B = new Font("Arial", 12, FontStyle.Bold);
        public static Font m_FontArial13B = new Font("Arial", 13, FontStyle.Bold);
        public static Font m_FontArial15B = new Font("Arial", 15, FontStyle.Bold);
        public static Font m_FontArial16B = new Font("Arial", 16, FontStyle.Bold);
        public static Font m_FontArial23 = new Font("Arial", 23);
        public static Font m_FontArial28B = new Font("Arial", 28, FontStyle.Bold);
        public static Font m_FontArial28 = new Font("Arial", 28);
        public static Font m_FontArial30B = new Font("Arial", 30, FontStyle.Bold);

        public static StringFormat m_SfCc = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        public static StringFormat m_SfNc = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
        public static StringFormat m_SfFc = new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };
        public static StringFormat m_SfNf = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Far };
        public static StringFormat m_SfFn = new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Near };
        public static StringFormat m_SfCf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Far };
        public static StringFormat m_SfCn = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near };
        public static StringFormat m_SfNn = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near };
        public static StringFormat m_SfFf = new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Far };

        public static SolidBrush m_SbBlue = new SolidBrush(Color.FromArgb(72, 174, 250));
    }
}
