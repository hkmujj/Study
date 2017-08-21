using System;
using System.Drawing;

namespace Urban.Wuxi.TMS.维护
{
    public class NumbKeyboard
    {
        public NumbKeyboard(PointF top)
        {
            //TODO:
            m_Points[0] = top;
            Init();
        }

        #region :::::::::::: func ::::::::::::::
        public void DrawKeyboard(Graphics e, ref bool[] btnIsDown, StringFormat format)
        {
            m_BtnIsDown = btnIsDown;
            if (m_BtnIsDown.Length < 13) return;

            for (int i = 0; i < 13; i++)
            {
                if (m_BtnIsDown[i])
                    e.FillRectangle(m_BlueBrush, m_Rects[i]);
                else
                    e.FillRectangle(FormatStyle.m_BlueBrush, m_Rects[i]);

                e.DrawString(m_Str[i], FormatStyle.m_Font24, FormatStyle.m_BlackBrush,
                    m_Rects[i], format);
            }
        }
        #endregion

        #region :::::::::::: init ::::::::::::::
        private void Init()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    m_Rects[i * 3 + j] = new RectangleF(j * 127, i * 57, 125, 55);
                }
            }
            m_Rects[9] = new RectangleF(0, 171, 125, 55);
            m_Rects[10] = new RectangleF(127, 171, 252, 55);

            for (int i = 0; i < 2; i++)
            {
                m_Rects[11 + i] = new RectangleF(i * 254, 250, 125, 55);
            }

            //
            for (int i = 0; i < m_Rects.Length; i++)
            {
                m_Rects[i].X = (m_Rects[i].X + m_Points[0].X) * FormatStyle.Scale;
                m_Rects[i].Y = (m_Rects[i].Y + m_Points[0].Y) * FormatStyle.Scale;
                m_Rects[i].Width *= FormatStyle.Scale;
                m_Rects[i].Height *= FormatStyle.Scale;
            }
        }
        #endregion

        #region :::::::::::: var :::::::::::::::
        private readonly PointF[] m_Points = new PointF[10];
        public PointF[] Points { get { return m_Points; } }

        private readonly RectangleF[] m_Rects = new RectangleF[20];
        public RectangleF[] Rects { get { return m_Rects; } }

        private bool[] m_BtnIsDown;

        private readonly SolidBrush m_BlueBrush = new SolidBrush(Color.Blue);

        private readonly String[] m_Str = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "DEL", "确定", "取消" };
        #endregion
    }
}