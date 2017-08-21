using System.Drawing;

namespace Motor.ATP._200H
{
    /// <summary>
    /// 制动预警图标
    /// </summary>
    public class RectWarn
    {
        public SolidBrush m_BKBrush;
        public RectangleF m_RectPosition;

        public void SetBKColor(int r, int g, int b)
        {
            m_BKBrush = new SolidBrush(Color.FromArgb(r, g, b));
        }

        public void SetRect(float width)
        {
            m_RectPosition.X = 32 - width / 2f;
            m_RectPosition.Y = 32 - width / 2f;
            m_RectPosition.Height = width;
            m_RectPosition.Width = width;
        }

        public void OnDraw(Graphics e)
        {
            if (m_BKBrush != null)
            {
                e.FillRectangle(m_BKBrush, m_RectPosition.X, m_RectPosition.Y,
                            m_RectPosition.Width, m_RectPosition.Height);
            }
        }
    }

}
