using System.Collections.Generic;
using System.Drawing;

namespace Urban.QingDao3Line.MMI
{
    class PerformanceData
    {
        private static List<Rectangle> m_Rects = new List<Rectangle>();
        private static List<Region> m_Regions = new List<Region>();
        public static List<Region> Regions
        {
            get { return m_Regions; }
        }
        public static List<Rectangle> Rects
        {
            get { return m_Rects; }
        }
        public static void InitData()
        {
            //维护：牵引及制动性能   /0
            m_Rects.Add(new Rectangle(280, 85, 300, 30));
            //加速测试、减速测试     /1-6
            for(int i=0;i<2;i++)
            {
                for(int j=0;j<3;j++)
                {
                    m_Rects.Add(new Rectangle(40 +420 * i, 180 + 55 * j, 200, 50));
                }
            }
            //制动距离              /7
            m_Rects.Add(new Rectangle(460, 345, 200, 50));
            //OK,NOK                /8-14
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    m_Rects.Add(new Rectangle(250 + 420 * i, 180 + 55 * j, 50, 50));
                }
            }
            m_Rects.Add(new Rectangle(670, 345, 50, 50));
            //开始按钮              /15
            m_Rects.Add(new Rectangle(285,450,180,60));
            //单位                 /16-20
            for (int i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(300,235+55*i,80,50));
            }
            for (int i = 0; i < 3;i++ )
            {
                m_Rects.Add(new Rectangle(720, 235 + 55 * i, 80, 50));
            }
            Regions.Add(new Region(Rects[8]));
            Regions.Add(new Region(Rects[11]));
            Regions.Add(new Region(Rects[15]));
        }
    }
}
