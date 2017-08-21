using System.Drawing;

namespace Engine.HMI.HXD1C.TPX21A
{
    public class TriangleMark
    {
        private readonly Point[] m_Point = new Point[3]; //三角图标左
        private Color m_BackColor;

        public enum ColorType
        {
            Gray = 1,
            Blue,
            Red
        };

        /// <summary>
        /// 初始化构造函数
        /// </summary>
        /// <param name="x">左顶点横坐标</param>
        /// <param name="y">左顶点纵坐标</param>
        public TriangleMark(int x, int y)
        {
            m_Point[0] = new Point(x, y);
            m_Point[1] = new Point(x + 19, y + 6);
            m_Point[2] = new Point(x + 19, y - 6);
            m_BackColor = Color.Gray;
        }

        /// <summary>
        /// 设置图标背景颜色
        /// </summary>
        /// <param name="colorType">0为蓝色 非零为红色</param>
        public void SetColor(ColorType type)
        {
            switch (type)
            {
                case ColorType.Gray:
                    m_BackColor = Color.Gray;
                    break;
                case ColorType.Red:
                    m_BackColor = Color.Red;
                    break;
                default:
                    m_BackColor = Color.Blue;
                    break;
            }
        }

        /// <summary>
        ///改变图标的位置 
        /// </summary>
        /// <param name="x">左顶点横坐标</param>
        /// <param name="y">左顶点纵坐标</param>
        public void SetPosition(int x, int y)
        {
            m_Point[0].X = x;
            m_Point[0].Y = y;
            m_Point[1].X = x + 19;
            m_Point[1].Y = y + 6;
            m_Point[2].X = x + 19;
            m_Point[2].Y = y - 6;
        }

        /// <summary>
        /// 根据顶点绘制图标 
        /// </summary>
        /// <param name="g"></param>
        public void OnDraw(Graphics g)
        {
            g.FillPolygon(new SolidBrush(m_BackColor), m_Point);
            g.DrawPolygon(new Pen(Color.FromArgb(200, 199, 243), 2), m_Point);
            g.DrawLine(new Pen(Color.FromArgb(200, 199, 243), 1), m_Point[0].X + 7, m_Point[0].Y, m_Point[0].X + 12,
                m_Point[0].Y);
            g.DrawLine(new Pen(Color.FromArgb(200, 199, 243), 1), m_Point[0].X + 8, m_Point[0].Y - 2, m_Point[0].X + 8,
                m_Point[0].Y + 2);
        }
    }
}