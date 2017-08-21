using System.Drawing;

namespace Urban.Iran.HMI.Common
{
    public class FjButton
    {
        
        private  string m_BtnText;
        private  Bitmap m_BtnBkBitmap;
        private  StringFormat m_StrFormat;
        public BtnStatus m_Status;
        public int m_BtnIndex;
        public event OnMouseDown Click;
        public FjButton(string btnText, Bitmap btnBkBitmap, int index)
        {
            m_BtnText = btnText;
            m_StrFormat = new StringFormat();
            m_StrFormat.Alignment = StringAlignment.Center;
            m_StrFormat.LineAlignment = StringAlignment.Center;
            m_BtnBkBitmap = btnBkBitmap;
            m_Status = BtnStatus.Normal;
            m_BtnIndex = index;
        }

        public FjButton(string btnText, Bitmap btnBkBitmap)
        {
            m_BtnText = btnText;
            m_StrFormat = new StringFormat();
            m_StrFormat.Alignment = StringAlignment.Center;
            m_StrFormat.LineAlignment = StringAlignment.Center;
            m_BtnBkBitmap = btnBkBitmap;
            m_Status = BtnStatus.Normal;
            m_BtnIndex = 0;
        }

        public void OnMouseDown(Point pt)
        {
            Click(this, pt);
        }

        public void OnDraw(Graphics g, Rectangle btnRect)
        {
            g.DrawImage(m_BtnBkBitmap, btnRect);

            g.DrawString(m_BtnText, GdiCommon.Txt12Font, (m_Status == BtnStatus.Active) ? GdiCommon.WhiteBrush : GdiCommon.MediumGreyBrush, btnRect, m_StrFormat);
        }
    }
}