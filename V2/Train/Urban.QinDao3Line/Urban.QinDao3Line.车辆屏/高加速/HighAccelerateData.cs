using System.Collections.Generic;
using System.Drawing;

namespace Urban.QingDao3Line.MMI
{
    internal class HighAccelerateData
    {
        public static List<Rectangle> m_Rects = new List<Rectangle>();
        public static List<Region> m_Regions = new List<Region>();

        static public void Init()
        {
            m_Rects.Add(new Rectangle(320, 300, 200, 30));                   //0 高加速已激活
            for (int i = 0; i < 2; i++)                                       //1-2 是否按钮
            {
                m_Rects.Add(new Rectangle(260 + 260 * i, 410, 60, 60));
            }
            for (int i = 0; i < 2; i++)
            {
                m_Regions.Add(new Region(m_Rects[1 + i]));
            }
        }
    }
}
