using System.Collections.Generic;
using System.Drawing;

namespace Urban.QingDao3Line.MMI
{
    class TrainLinesData
    {

        private static readonly List<Rectangle> m_Rects = new List<Rectangle>();

        public static List<Rectangle> Rects
        {
            get { return m_Rects; }
        }

        private static readonly List<Region> m_Regions = new List<Region>();

        public static List<Region> Regions
        {
            get { return m_Regions; }
        }

        public static void InitData()
        {
            //左侧5个矩形框  /0-4
            for (int i = 0; i < 5; i++)
            {
                m_Rects.Add(new Rectangle(142, 232 + i * 23, 257, 23));
            }
            //右侧24个矩形框 /5-28
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    m_Rects.Add(new Rectangle(399 + j * 80, 209 + i * 23, 80, 23));
                }
            }
            //左侧3个按钮string区域   /29-31
            m_Rects.Add(new Rectangle(12, 195, 50, 50));
            m_Rects.Add(new Rectangle(5, 270, 64, 55));
            m_Rects.Add(new Rectangle(7, 345, 60, 55));
            //左侧5个string位置   /32-36
            for (int i = 0; i < 5; i++)
            {
                m_Rects.Add(new Rectangle(142, 236 + i * 23, 256, 23));
            }
            //3个网关阀string位置   /37-40
            for (int i = 0; i < 4; i++)
            {
                m_Rects.Add(new Rectangle(399 + i * 80, 212, 80, 23)); 
            }
            //左侧三个按钮图片  /41-43
            for (int i = 0; i < 3; i++)
            {
                m_Rects.Add(new Rectangle(7, 190 + i * 75, 60, 60));
            }
            //维护：制动    /44
            m_Rects.Add(new Rectangle(350, 90, 150, 30));
            for (int i = 0; i < 3; i++)
            {
                m_Regions.Add(new Region(m_Rects[41 + i]));
            }
            //右侧状态显示图片位置  /45-64
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    m_Rects.Add(new Rectangle(429 + j * 80, 232 + i * 23, 24, 24));
                }
            }
        }
    }
}

