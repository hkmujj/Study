using System.Collections.Generic;
using System.Drawing;

namespace Urban.QingDao3Line.MMI
{
    class AuxiliaryFaultDatacs
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
            //从上到下，从左到右矩形框
            //左侧17个小矩形   
            //0-5
            for (int i=0;i<6;i++)
            {
                m_Rects.Add(new Rectangle(80, 90+i*24, 500, 24));
            }
            //左侧中间大矩形    /6
            m_Rects.Add(new Rectangle(80, 234, 500, 48));
            //左侧故障清单10个矩形   /7-16
            for (int i = 0; i < 10; i++)
            {
                m_Rects.Add(new Rectangle(80, 282 + i * 24, 500, 24));
            }  
            //右侧12个小矩形        /17-28
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 2; j++)
                    m_Rects.Add(new Rectangle(580+j*50, 90+i*24, 50, 24));
            }   
            //右侧中间两个矩形  /29-30
            for (int i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(580+i*50, 234, 50, 48));
            }
            //右侧中间两个矩形 /31-32
            for (int i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(580 + i * 50, 282, 50, 24));
            }
            //右侧下方最大的矩形  /33
            m_Rects.Add(new Rectangle(580, 306, 100, 216));
            //34-35 左侧上面的两个按钮
            for (int i=0;i<2;i++)
            {
                m_Rects.Add(new Rectangle(10, 234 + i * 69, 55, 55));
            }
            for (int i=0;i<2;i++)
            {
                m_Regions.Add(new Region(m_Rects[34 + i]));
            } 
        }
    }
}
