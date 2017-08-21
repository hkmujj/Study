using System.Collections.Generic;
using System.Drawing;

namespace Urban.QingDao3Line.MMI
{
    class BaterryData
    {
        private static readonly List<Rectangle> m_Rects = new List<Rectangle>();

        public static List<Rectangle> Rects
        {
            get { return m_Rects; }
        }

        public static void InitData()
        {
            //低图   /0
            m_Rects.Add(new Rectangle(49,229,743,275));
            //1-2 蓄电池容量
            for (int i=0;i<2;i++)
            {
                m_Rects.Add(new Rectangle(33+i*545, 153, 175, 23));
            }
            //顺序从上倒下，从左到右
            //3-4
            for (int i=0;i<2;i++)
            {
                m_Rects.Add(new Rectangle(182 , 272+i*48, 66, 22)); 
            }
            //5
            m_Rects.Add(new Rectangle(225, 362, 51, 22)); 
            //6
            m_Rects.Add(new Rectangle(345, 381, 51, 22)); 
            //7-8
            for (int i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(576, 272 + i * 48, 66, 22));
            }
            //9
            m_Rects.Add(new Rectangle(611, 362, 53, 22));
            //10
            m_Rects.Add(new Rectangle(731, 381, 55, 22));
            //11-12 蓄电池容量文字位置
            for (int i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(30 + i * 555, 120, 166, 31));
            }
            //13 维护:蓄电池
            m_Rects.Add(new Rectangle(350, 90, 150, 30));
        }

    }
}
