using System.Collections.Generic;
using System.Drawing;

namespace Urban.QingDao3Line.MMI
{
    class FaultInformationData
    {
        private static readonly List<Point> m_Points = new List<Point>();

        public static List<Point> Points
        {
            get { return m_Points; }
        }
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
            //五根横线
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    m_Points.Add(new Point(94 + j * 577, 127 + i * 21));
                }
            }
            //六根竖线
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    m_Points.Add(new Point(394 + i * 70, 127 + j * 84));
                }
            }
            //左侧10个小的矩形框 /0-9
            for (int i = 0; i < 10; i++)
            {
                m_Rects.Add(new Rectangle(94, 236 + i * 22, 300, 22));
            }
            //右侧上面6个小的矩形框  /10-15
            for (int i = 0; i < 6; i++)
            {
                m_Rects.Add(new Rectangle(394 + i * 48, 236, 48, 22));
            }
            //右侧底部大的矩形框   /16
            m_Rects.Add(new Rectangle(394, 258, 288, 198));
            //左侧侧3个按钮区域        /17-19
            m_Rects.Add(new Rectangle(12, 195, 50, 50));
            m_Rects.Add(new Rectangle(5, 270, 64, 55));
            m_Rects.Add(new Rectangle(7, 342, 60, 60));
            //右侧2个按钮区域     /20-21
            for (int i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(707, 288 + i * 70, 48, 48));
            }

            //上面4个大矩形框  /22-25
            for (int i = 0; i < 4; i++)
            {
                m_Rects.Add(new Rectangle(95, 128 + i * 21, 300, 21));
            }
            //右侧上面4个小矩形框  /26-29
            for (int i = 0; i < 4; i++)
            {
                m_Rects.Add(new Rectangle(394 + i * 70, 130, 70, 21));
            }
            //右侧上面12个小矩形  /30-41
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    m_Rects.Add(new Rectangle(415 + j * 70, 145 + i * 21, 70, 21));
                }
            }
            // 左侧3个按钮的图片  /42-44
            for (int i = 0; i < 3; i++)
            {
                m_Rects.Add(new Rectangle(7, 190 + i * 75, 60, 60));
            }
            //右侧2个按钮的图片  /45-46
            for (int i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(702, 283 + i * 70, 58, 58));
            }
            //47-100
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    m_Rects.Add(new Rectangle(404 + j * 48, 258 + i * 22, 48, 22));
                }
            }
            //维护：制动    /101
            m_Rects.Add(new Rectangle(350, 90, 150, 30));

            //12个显示灯图片  /102-113
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    m_Rects.Add(new Rectangle(419 + j * 70, 146 + i * 21, 24, 24));
                }
            }
            for (int i = 0; i < 5; i++)
            {
                m_Regions.Add(new Region(m_Rects[17 + i]));
            }
        }
    }
}
