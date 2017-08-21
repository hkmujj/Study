using MMI.Facility.Interface;
using System;
using System.Drawing;


namespace Urban.QingDao.VT
{
    class SpeedPointer
    {
        // Fields
        private float m_Anglev = 0f;
        private readonly int m_DialAnglev;
        private Image m_DrawPic;
        private readonly int m_InitalAnglev;
        private readonly float m_MaxSpeed;
        private readonly Image m_OriginalPic;
        private Graphics m_PicAnglev;
        private PointF m_TopPoint;
        private readonly int m_InFloatIndex;
        private readonly baseClass m_BaseClass;
        // Methods
        public SpeedPointer(Image tmpPic, int maxAnglev, int initAnglev, float maxValue, RectangleF rectangle, int infloatIndex, baseClass baseclss)
        {
            m_OriginalPic = new Bitmap(tmpPic, new Size((int)rectangle.Width, (int)rectangle.Height));
            m_DialAnglev = maxAnglev;
            m_InitalAnglev = initAnglev;
            m_MaxSpeed = maxValue;
            m_TopPoint = new PointF(rectangle.X, rectangle.Y);
            m_InFloatIndex = infloatIndex;
            m_BaseClass = baseclss;
        }

        public void PaintPointer(Graphics g)
        {

            float speed = m_BaseClass.FloatList[m_InFloatIndex];
            Graphics graphics = Graphics.FromImage(m_OriginalPic);
            graphics.DrawImage(m_OriginalPic, 0, 0, new Rectangle(0, 0, m_OriginalPic.Width, m_OriginalPic.Height), GraphicsUnit.Pixel);
            m_DrawPic = new Bitmap(m_OriginalPic.Width, m_OriginalPic.Height, graphics);
            m_PicAnglev = Graphics.FromImage(m_DrawPic);
            m_PicAnglev.TranslateTransform(m_DrawPic.Width / 2, m_DrawPic.Height / 2);
            if (speed <= m_MaxSpeed)
            {
                m_Anglev = ((speed * m_DialAnglev) / m_MaxSpeed) + m_InitalAnglev;
            }
            m_PicAnglev.RotateTransform(m_Anglev);
            m_PicAnglev.DrawImage(m_OriginalPic, new Point(-m_OriginalPic.Width / 2, -m_OriginalPic.Height / 2));
            m_PicAnglev.ResetTransform();
            if (m_DrawPic == null)
            {
                return;
            }
            g.DrawImage(m_DrawPic, m_TopPoint.X, m_TopPoint.Y, new RectangleF(0f, 0f, Convert.ToSingle(m_DrawPic.Width), Convert.ToSingle(m_DrawPic.Height)), GraphicsUnit.Pixel);
            m_DrawPic.Dispose();
        }

    }
}