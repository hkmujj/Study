using System.Collections.Generic;
using System.Drawing;

namespace Urban.QingDao3Line.MMI
{
    class HvacData
    {
        private static readonly List<Rectangle> m_Rects = new List<Rectangle>();

        private static readonly List<Point> m_myPoints = new List<Point>();

        public static List<Point> myPoints
        {
            get { return m_myPoints; }
        }

        public static List<Rectangle> Rects
        {
            get { return m_Rects; }
        }

        public static void InitData()
        {
            //左侧15个   /0-14
            for (int i = 0; i < 15; i++)
            {
                m_Rects.Add(new Rectangle(3, 158 + i * 25, 250, 25));
            }
            //右侧第一行6个  /15-20
            for (int i = 0; i < 6; i++)
            {
                m_Rects.Add(new Rectangle(253 + i * 90, 108, 90, 25));
            }
            //右侧第二行12个   /21-32
            for (int i = 0; i < 12; i++)
            {
                m_Rects.Add(new Rectangle(253 + i * 45, 133, 45, 25));
            }
            //右侧第三至第六行24个   /33-56
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    m_Rects.Add(new Rectangle(253 + j * 90, 158 + i * 25, 90, 25));
                }
            }

            //57-188
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    m_Rects.Add(new Rectangle(253 + j * 45, 258 + i * 25, 45, 25));
                }
            }

            //新风温度传感器   /189-200
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    m_Rects.Add(new Rectangle(260 + 90 * i + 65 * j, 215, 12, 12));
                }
            }
            //回风温度传感器   /201-212
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    m_Rects.Add(new Rectangle(260 + 90 * i + 65 * j, 240, 12, 12));
                }
            }
            //新风及回风温度值 /213-224
            for (int i = 0; i < 2;i++ )
            {
                for (int j = 0; j < 6; j++)
                {
                    m_Rects.Add(new Rectangle(253+j*90,213+i*25,90,20));
                }
            }
            //维护：HVAC  /225
            m_Rects.Add(new Rectangle(330, 75, 180, 30));

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    m_myPoints.Add(new Point(253 + i * 90, 108 + j * 425));
                }
            }
            for (int i = 0; i < 2; i++)
            {
                m_myPoints.Add(new Point(3 + 880 * i, 983));
            }

        }
    }
}
