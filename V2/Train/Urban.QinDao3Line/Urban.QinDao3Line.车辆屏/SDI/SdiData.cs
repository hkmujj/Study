using System.Collections.Generic;
using System.Drawing;

namespace Urban.QingDao3Line.MMI
{
    class SdiData
    {
        private static readonly List<Rectangle> m_Rects = new List<Rectangle>();

        public static List<Rectangle> Rects
        {
            get { return m_Rects; }
        }

        public static void InitData()
        {    
            //维护：烟火检测
            m_Rects.Add(new Rectangle(320, 100, 240, 30));
            //左边3个矩形   /1-3
            for (int i = 0; i < 3;i++)
            {
                m_Rects.Add(new Rectangle(70, 240+25*i, 350, 25));
            }
            //右边24个矩形,从左到右，从上到下  /4-27
            for (int i=0;i<4;i++)
            {
                for (int j = 0; j < 6;j++ )
                    m_Rects.Add(new Rectangle(420 + j * 50, 215+i*25, 50, 25));
            }
        }
    }
}
