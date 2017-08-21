using System.Drawing;

namespace Motor.ATP._300D.Common
{
    public class RectProgress
    {
        //int FC_X = FrameButton2D.FrameChange_X;
        //int FC_Y = FrameButton2D.FrameChange_Y;

        RectangleF m_RectPosition;
        SolidBrush m_PrBrush;
        float m_Level;
        readonly PointF[] m_LinePoint = new PointF[4];

        readonly Pen m_DrawlinePen = new Pen(Color.FromArgb(255, 255, 255), 2);

        public void SetPRcolor(int r, int g, int b)
        {
            m_PrBrush = new SolidBrush(Color.FromArgb(r, g, b));
        }

        public void SetRect(float x, float y, float width, float height)
        {
            m_RectPosition.X = x;
            m_RectPosition.Y = y - height;
            m_RectPosition.Height = height;
            m_RectPosition.Width = width;

            m_LinePoint[0].X = m_RectPosition.X;
            m_LinePoint[0].Y = m_RectPosition.Y;

            m_LinePoint[1].X = m_RectPosition.X + width;
            m_LinePoint[1].Y = m_RectPosition.Y;

            m_LinePoint[2].X = m_RectPosition.X + width;
            m_LinePoint[2].Y = m_RectPosition.Y + height;

            m_LinePoint[3].X = m_RectPosition.X;
            m_LinePoint[3].Y = m_RectPosition.Y + height;
        }

        public void SetRect(float height)
        {
            SetRect(m_RectPosition.X, m_RectPosition.Y + m_RectPosition.Height,
                m_RectPosition.Width, height);
        }

        public void Setlevel(float level)
        {
            m_Level = level;

            if (level >= 1.0f)
            {
                m_Level = 1.0f;
            }
            if (level <= 0)
            {
                m_Level = 0.0f;
            }
        }

        public void OnDraw(Graphics g)
        {
            if (m_RectPosition.Height < 3)
                return;
            if (m_PrBrush != null)
            {
                g.FillRectangle(m_PrBrush, m_RectPosition.X, m_RectPosition.Y + m_RectPosition.Height - m_RectPosition.Height * m_Level,
                    m_RectPosition.Width, m_RectPosition.Height * m_Level);

            }
            g.DrawPolygon(m_DrawlinePen, m_LinePoint);
        }
    }
}