using System.Drawing;

namespace Urban.Iran.HMI.Common
{
    public class FjButtonEx
    {
        public int BtnIndex { set; get; }
        public BtnStatus Status { set; get; }
        public Font BtnFont { set; get; }
        public Bitmap BtnBkBmp { set; get; }

        private readonly string m_BtnText;
        
        private readonly Rectangle m_BtnRect;

        public event OnMouseDownEx MouseDown;

        public event OnLostFoucse LostFoucse;

        public FjButtonEx(int btnIndex, string btnText, Rectangle btnRect)
        {
            BtnIndex = btnIndex;
            m_BtnText = btnText;
            BtnBkBmp = GdiCommon.BtnBkBitmap;
            m_BtnRect = btnRect;
            Status = BtnStatus.Normal;
            BtnFont = GdiCommon.Txt12Font;
            Visibility = true;
        }

        public FjButtonEx(int btnIndex, string btnText, Rectangle btnRect, Bitmap btnBkBmp)
        {
            BtnIndex = btnIndex;
            m_BtnText = btnText;
            BtnBkBmp = btnBkBmp;
            m_BtnRect = btnRect;
            Status = BtnStatus.Normal;
            BtnFont = GdiCommon.Txt12Font;
            Visibility = true;
        }

        public bool IsVisible(Point pt)
        {
            return m_BtnRect.Contains(pt);
        }

        public void OnMouseDown(Point pt)
        {
            if (MouseDown != null && m_BtnRect.Contains(pt))
            {
                MouseDown(this, pt);
            }
        }

        public void OnLostFoucse(Point pt)
        {
            LostFoucse(this, pt);
        }

        public void OnDraw(Graphics g)
        {
            if (Visibility)
            {
                g.DrawImage(BtnBkBmp, m_BtnRect);

                g.DrawString(m_BtnText, BtnFont, (Status == BtnStatus.Active) ? GdiCommon.WhiteBrush : GdiCommon.MediumGreyBrush, m_BtnRect, GdiCommon.CenterFormat);
            }
        }
        public bool Visibility { get;  set; }

    }
}