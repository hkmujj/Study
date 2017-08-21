using System.Collections.Generic;
using System.Drawing;

namespace Urban.QingDao3Line.MMI
{
    class DoorsTrainlineData
    {
        private static readonly List<Rectangle> m_Rects = new List<Rectangle>();

        public static List<Rectangle> Rects
        {
            get { return m_Rects; }
        }

        public static void Initdata()
        {
            //上方表格 左边2个矩形  /0-1
            for (int i=0;i<2;i++)
            {
                m_Rects.Add(new Rectangle(34,237+i*26,259,26));
            }
            //上方表格 右边18个矩形  从左到右，从上到下  /2-19
            for (int i=0;i<3;i++)
            {
                for (int j=0;j<6;j++)
                {
                    m_Rects.Add(new Rectangle(293+j*66,211+i*26,66,26));
                }
            }
            //下方表格 左边7个矩形   /20-26
            for (int i=0;i<7;i++)
            {
                m_Rects.Add(new Rectangle(128, 327 + i * 26, 318, 26));
            }
            //下方表格 右边16个矩形 从左到右，从上到下   /27-42 从上到下
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    m_Rects.Add(new Rectangle(446 + j* 66, 301+ i * 26, 66, 26));  
                }
            }
        } 
    }
}
