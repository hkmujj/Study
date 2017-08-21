using System.Collections.Generic;
using System.Drawing;

namespace Urban.QingDao3Line.MMI
{
    class PassengerData
    {
        private static readonly List<Rectangle> m_Rects = new List<Rectangle>();

        public static List<Rectangle> Rects
        {
            get { return m_Rects; }
        }

        public static void InitData()
        {
            //维护：乘客信息系统    /0
            m_Rects.Add(new Rectangle(310, 85, 280, 30));
            //左边16个矩形   /1-16
            for (int i = 0; i < 16; i++)
            {
                m_Rects.Add(new Rectangle(70, 130 + 25 * i, 550, 25));
            }
            //右边14个小矩形,从左到右，从上到下  /17-30
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 2; j++)
                    m_Rects.Add(new Rectangle(620 + j * 50, 130 + i * 25, 50, 25));
            }
            //右边的1个大矩形      /31
            m_Rects.Add(new Rectangle(620, 305, 100, 225));
        }
    }
}
