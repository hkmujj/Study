using System.Collections.Generic;
using System.Drawing;

namespace Urban.QingDao3Line.MMI
{
    class EventData
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
            //MPU事件清单       /0
            m_Rects.Add(new Rectangle(380, 95, 220, 50));
            //重载按钮图片      /1
            m_Rects.Add(new Rectangle(225, 80, 60, 60));
            //翻页键按钮图片        /2-3
            for (int i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(735, 250 + 68 * i, 60, 60));
            }
            //重载按钮区域      /4
            m_Rects.Add(new Rectangle(220, 45, 50, 50));
            //翻页按钮区域         /5-6
            for (int i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(740, 255 + 68 * i, 50, 50));
            }
            //编号位置         /7-10
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    m_Rects.Add(new Rectangle(8+100*i, 85 + 24 * j, 100, 24));
                }
            }
            //下方表格      /11-25
            for (int i = 0; i < 15; i++)
            {
                m_Rects.Add(new Rectangle(8, 160+24*i, 150, 24));
            }
            //26-40
            for (int i = 0; i < 15; i++)
            {
                m_Rects.Add(new Rectangle(158, 160 + 24 * i, 110, 24));
            }
            //41-55
            for (int i = 0; i < 15; i++)
            {
                m_Rects.Add(new Rectangle(268, 160 + 24 * i, 50, 24));
            }
            //56-70
            for (int i = 0; i < 15; i++)
            {
                m_Rects.Add(new Rectangle(318, 160 + 24 * i, 410, 24));
            }
            for (int i = 0; i < 3; i++)
                m_Regions.Add(new Region(m_Rects[1 + i]));
        }
    }
}
