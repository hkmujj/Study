using System.Collections.Generic;
using System.Drawing;

namespace Urban.QingDao3Line.MMI
{
    class PmData
    {
        private static readonly List<Rectangle> m_Rects = new List<Rectangle>();

        public static List<Rectangle> Rects
        {
            get { return m_Rects; }
        }

        public static void InitData()
        {
            //0-15  左边14个矩形
            for (int i = 0; i < 16; i++)
            {
                m_Rects.Add(new Rectangle(90, 109 + i * 25, 395, 25));
            }
            //16-19    PCE1、PCE2、PCE3、PCE4
            for (int i = 0; i < 4; i++)
            {
                m_Rects.Add(new Rectangle(485+51*i,109,51,25));
            }
            //20-35    状态显示
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                    m_Rects.Add(new Rectangle(485 + 51 * j, 134 + 25 * i, 51, 25));
            }
            //36-39   AB AB
            for (int i = 0; i < 4; i++)
            {
                m_Rects.Add(new Rectangle(485 + 51 * i, 234, 51, 25));
            }
            //40-43   PCE1 PCE2 PCE3 PCE4
            for (int i = 0; i < 4; i++)
            {
                m_Rects.Add(new Rectangle(485 + 51 * i, 259, 51, 25));
            }
            //44      右下角矩形
            m_Rects.Add(new Rectangle(485, 284, 204, 225));
            //45      牵引：维护
            m_Rects.Add(new Rectangle(340, 80, 140, 35));
            //46      警告说明
            m_Rects.Add(new Rectangle(90, 522, 170, 25));
            //47      车辆静止且已施加快速制动
            m_Rects.Add(new Rectangle(255, 522, 200, 25));
            //48      警告图标
            m_Rects.Add(new Rectangle(64, 512, 28, 28));
        }
    }
}