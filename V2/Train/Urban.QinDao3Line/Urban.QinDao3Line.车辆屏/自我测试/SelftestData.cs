using System.Collections.Generic;
using System.Drawing;

namespace Urban.QingDao3Line.MMI
{
    class SelftestData
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
            //self test Condition   /0
            m_Rects.Add(new Rectangle(191, 128, 172, 20));
            //左侧3个矩形框  /1-3
            for (int i = 0; i < 3; i++)
            {
                m_Rects.Add(new Rectangle(162, 232 + i * 23, 237, 23));
            }
            //右侧16个矩形框 /4-19
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    m_Rects.Add(new Rectangle(399 + j * 80, 209 + i * 23, 80, 23));
                }
            }
            //左侧4个按钮string区域   /20-23
            m_Rects.Add(new Rectangle(12, 195, 50, 50));
            m_Rects.Add(new Rectangle(5, 270, 64, 55));
            m_Rects.Add(new Rectangle(7, 345, 60, 55));
            m_Rects.Add(new Rectangle(7, 441, 60, 40));
            //To be define    /24
            m_Rects.Add(new Rectangle(209, 144, 172, 20));
            //左侧3个string位置   /25-27
            for (int i = 0; i < 3; i++)
            {
                m_Rects.Add(new Rectangle(162, 234 + i * 23, 236, 23));
            }
            //网关阀string位置   /28-31
            for (int i = 0; i < 4; i++)
            {
                m_Rects.Add(new Rectangle(399 + i * 80, 212, 80, 23));
            }
            //左侧四个按钮图片  /32-35
            for (int i = 0; i < 4; i++)
            {
                m_Rects.Add(new Rectangle(7, 190 + i * 75, 60, 60));
            }
            //维护：制动   /36
            m_Rects.Add(new Rectangle(350, 90, 150, 30));
            for (int i = 0; i < 4; i++)
            {
                m_Regions.Add(new Region(m_Rects[32 + i]));
            }
            
        }
    }
}
