using System.Collections.Generic;
using System.Drawing;

namespace Urban.QingDao3Line.MMI
{
    class AirData
    {
        private static readonly List<Rectangle> m_Rects = new List<Rectangle>();

        public static List<Rectangle> Rects
        {
            get { return m_Rects; }
        }

        public static void InitData()
        {
            //0-1
            for (int i=0;i<2;i++)
            {
                m_Rects.Add(new Rectangle(177 + i*250, 210, 182, 148));
            }
            //2-3
            for (int i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(177 + i * 250, 210, 182, 28));
            }
            //4-5
            for (int i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(235 + i * 250, 317, 85, 24));
            }
            //6-11 图标的位置
            for (int i=0;i<2;i++)
            {
                for (int j=0;j<3;j++)
                {
                    m_Rects.Add(new Rectangle(218+i*250,243+j*22,23,23));
                }
            }
            //12-17 文字的位置
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    m_Rects.Add(new Rectangle(230 + i*250, 247 + j*22, 64, 20));
                }
            }
            //维护：空气压缩机   /18
            m_Rects.Add(new Rectangle(330, 130, 180, 30));
        }
    }
}
