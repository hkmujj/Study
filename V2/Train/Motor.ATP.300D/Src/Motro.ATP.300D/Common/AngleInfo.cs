using System.Drawing;

namespace Motor.ATP._300D.Common
{
    /// <summary>
    /// Èý½ÇÐÎµÈÌî³ä
    /// </summary>
    public class AngleInfo
    {
        int FC_X = FrameButton2D.FrameChange_X;
        int FC_Y = FrameButton2D.FrameChange_Y;

        SolidBrush m_BkBrush;

        readonly PointF[] m_PointInfo = new PointF[3];

        public void SetBKcolor(int r, int g, int b)
        {
            m_BkBrush = new SolidBrush(Color.FromArgb(r, g, b));

        }
        public void SetPoint(PointF[] point)
        {
            m_PointInfo[0] = point[0];
            m_PointInfo[1] = point[1];
            m_PointInfo[2] = point[2];
        }

        public void OnDraw(Graphics g, PointF point)
        {
            var pointTmp = new PointF[3];
            var x = point.X - m_PointInfo[0].X;
            var y = point.Y - m_PointInfo[0].Y;
            for (int i = 0; i < 3; i++)
            {
                pointTmp[i].X = m_PointInfo[i].X+x;
                pointTmp[i].Y = m_PointInfo[i].Y+y;
            }
            g.FillPolygon(m_BkBrush, pointTmp);
        }

        public void OnDraw(Graphics g, float y)
        {
            var pointTmp = new PointF[3];
            for (int i = 0; i < 3; i++)
            {
                pointTmp[i].X = m_PointInfo[i].X;
                pointTmp[i].Y = m_PointInfo[i].Y - y;
            }
            g.FillPolygon(m_BkBrush, pointTmp);
        }

        public void OnDraw(Graphics g)
        {
            g.FillPolygon(m_BkBrush, m_PointInfo);
        }
    }
}