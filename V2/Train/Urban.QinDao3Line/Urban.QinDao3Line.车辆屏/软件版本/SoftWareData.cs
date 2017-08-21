using System.Collections.Generic;
using System.Drawing;

namespace Urban.QingDao3Line.MMI
{
    class SoftWareData
    {
        private static readonly List<Rectangle> m_Rects = new List<Rectangle>();

        private static int i = 0;

        public static List<Rectangle> Rects
        {
            get { return m_Rects; }
        }

        private static readonly List<Region> m_Regions = new List<Region>();

        public static void InitData()
        {
            //维护：软件版本   /0
            m_Rects.Add(new Rectangle(320, 80, 160, 30));
            //车头1，2       /1-2
            for (i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(8 + 749 * i, 120, 34, 76));
            }
            //车厢3-8
            for (i = 0; i < 6; i++)
            {
                m_Rects.Add(new Rectangle(39+120*i, 119, 119, 77));
            }
            //6个白色的长矩形  //9-14
            for (i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(8 + 650 * i, 240, 130, 300));
            }
            for (i = 0; i < 4; i++)
            {
                m_Rects.Add(new Rectangle(173 + 130 * i, 240, 70, 300));
            }
            //PIS1等文字位置     //15-24
            for (i = 0; i < 5; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    m_Rects.Add(new Rectangle(3 + 65 * j, 240+60*i, 75, 30));
                }
            }
            for (i = 0; i < 4; i++)       //25-44        
            {
                for (int j = 0; j < 5; j++)
                {
                    m_Rects.Add(new Rectangle(168 + 130 * i, 240 + 60 * j, 80, 30));
                }
            }
            for (i = 0; i < 5; i++)          //45-54
            {
                for (int j = 0; j < 2; j++)
                {
                    m_Rects.Add(new Rectangle(653 + 65 * j, 240 + 60 * i, 75, 30));
                }
            }
            //1.0.1等文字
            for (i = 0; i < 5; i++)            //55-64
            {
                for (int j = 0; j < 2; j++)
                {
                    m_Rects.Add(new Rectangle(8 + 65 * j, 270 + 60 * i, 65, 30));
                }
            }
            for (i = 0; i < 4; i++)       //65-84        
            {
                for (int j = 0; j < 5; j++)
                {
                    m_Rects.Add(new Rectangle(173 + 130 * i, 270 + 60 * j, 70, 30));
                }
            }
            for (i = 0; i < 5; i++)          //85-94
            {
                for (int j = 0; j < 2; j++)
                {
                    m_Rects.Add(new Rectangle(658 + 65 * j, 270 + 60 * i, 65, 30));
                }
            }
            //横杠
            for (i = 0; i < 5; i++)                 //95-104
            {
                for (int j = 0; j < 2; j++)
                {
                    m_Rects.Add(new Rectangle(8 + 65 * j, 242 + 60 * i, 65, 60));
                }
            }
            for (i = 0; i < 4; i++)       //105-124       
            {
                for (int j = 0; j < 5; j++)
                {
                    m_Rects.Add(new Rectangle(173 + 130 * i, 242 + 60 * j, 70, 60));
                }
            }
            for (i = 0; i < 5; i++)          //125-134
            {
                for (int j = 0; j < 2; j++)
                {
                    m_Rects.Add(new Rectangle(658 + 65 * j, 242 + 60 * i, 65, 60));
                }
            }
        }
    }
}
