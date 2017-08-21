using System.Drawing;

namespace Motor.ATP._300D.Common
{
    /// <summary>
    /// //ÖÆ¶¯Ô¤¾¯
    /// </summary>
    public class RectWarn
    {
        //int FC_X = FrameButton2D.FrameChange_X;
        //int FC_Y = FrameButton2D.FrameChange_Y;

        SolidBrush m_BkBrush;
        RectangleF m_RectPosition;

        public void SetBKColor(int r, int g, int b)
        {
            m_BkBrush = new SolidBrush(Color.FromArgb(r, g, b));
        }

        public void SetRect(float width)
        {
            m_RectPosition.X = 33 - width/2f;
            m_RectPosition.Y = 35 - width / 2f;
            m_RectPosition.Height = width;
            m_RectPosition.Width = width;
        }

        public void OnDraw(Graphics e)
        {
            if (m_BkBrush != null)
            {
                e.FillRectangle(m_BkBrush, m_RectPosition.X, m_RectPosition.Y,
                            m_RectPosition.Width, m_RectPosition.Height);
            }
        }
    }
}
