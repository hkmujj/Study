using System.Collections.Generic;
using System.Drawing;

namespace Urban.QingDao3Line.MMI
{
    internal class MaintanceData
    {
        public List<Rectangle> m_Rects = new List<Rectangle>();
        public List<Region> m_Regions = new List<Region>();

        public void Init()
        {
            //0-13
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    m_Rects.Add(new Rectangle(86 + j * 102, 121 + i * 95, 89, 89));
                }
            }
            //14
            m_Rects.Add(new Rectangle(86, 311, 89, 89));
            //15
            m_Rects.Add(new Rectangle(188, 311, 89, 89));
            //16
            m_Rects.Add(new Rectangle(350, 83, 306, 30));
            //17
            m_Rects.Add(new Rectangle(81, 490, 394, 30));

            //状态显示灯
            //18-31
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    m_Rects.Add(new Rectangle(154 + j * 102, 125 + i * 95, 15, 15));
                }
            }
            //32
            m_Rects.Add(new Rectangle(154, 315, 15, 15));
            //33
            m_Rects.Add(new Rectangle(256, 315, 15, 15));

            for (int i = 0; i < 15; i++)
            {
                m_Regions.Add(new Region(m_Rects[i]));
            }
        }
    }
}
