using System.Drawing;
using Urban.Iran.DMI.Model;

namespace Urban.Iran.DMI.Controls
{
    public class FjButtonEx
    {
        public int m_BtnIndex;
        public Bitmap m_BmpClicked;
        public Bitmap m_BmpNormal;
        private readonly Rectangle m_BtnRect;
        private readonly Region m_BtnRegion;
        public BtnStatus m_Status;
        public event OnMouseDownEx onMouseDown;
        public event OnLostFoucse onLostFoucse;

        public FjButtonEx(int btnIndex, Rectangle btnRect, Bitmap btnNormalBmp)
        {
            m_BtnIndex = btnIndex;
            m_BtnRect = btnRect;
            m_BmpNormal = btnNormalBmp;
            m_BmpClicked = btnNormalBmp;
            m_BtnRegion = new Region(btnRect);
            m_Status = BtnStatus.Normal;
        }

        public FjButtonEx(int btnIndex, Rectangle btnRect, Bitmap btnNormalBmp, Bitmap btnClickedBmp)
        {
            m_BtnIndex = btnIndex;
            m_BtnRect = btnRect;
            m_BmpNormal = btnNormalBmp;
            m_BmpClicked = btnClickedBmp;
            m_BtnRegion = new Region(btnRect);
            m_Status = BtnStatus.Normal;
        }

        public bool IsVisible(Point pt)
        {
            return m_BtnRegion.IsVisible(pt);
        }
        public void OnMouseDown(Point pt)
        {
            onMouseDown(this, pt);
        }

        public void OnLostFoucse(Point pt)
        {
            onLostFoucse(this, pt);
        }
        public void OnDraw(Graphics g)
        {
            g.DrawImage((m_Status == BtnStatus.Normal) ? m_BmpNormal : m_BmpClicked, m_BtnRect);
        }

    }
}