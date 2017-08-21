using System.Collections.Generic;
using System.Drawing;

namespace Urban.QingDao3Line.MMI
{
    class BlackBoxData
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
            //维护：黑匣子   /0
            m_Rects.Add(new Rectangle(300, 80, 180, 30));
            #region::::::::左侧3个按钮:::::::::
            //按钮区域    /1-3
            m_Rects.Add(new Rectangle(12, 195, 50, 50));
            m_Rects.Add(new Rectangle(5, 270, 64, 50));
            m_Rects.Add(new Rectangle(7, 342, 60, 60));
            //按钮图片  /4-6
            for (int i = 0; i < 3; i++)
            {
                m_Rects.Add(new Rectangle(7, 190 + i * 75, 60, 60));
            }
            #endregion

            #region::::::::故障事件::::::::::::::
            //上方表格左边4个矩形    /7-10
            for (i = 0; i < 4; i++)
            {
                m_Rects.Add(new Rectangle(100, 130+25*i, 480, 25));
            }
            //上方表格右边8个矩形   /11-18
            for (i = 0; i < 4; i++)
            {
                for (int j = 0; j < 2;j++)
                { 
                    m_Rects.Add(new Rectangle(580+60*j, 130 + 25 * i, 60, 25));
                }    
            }
            //下方表格左边十个矩形   /19-28
            for (i = 0; i < 10; i++)
            {
                m_Rects.Add(new Rectangle(100, 255 + 25 * i, 480, 25));
            }
            //下方表格右边3个矩形  /29-31
            for (i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(580+60*i, 255, 60, 25));
            }
            m_Rects.Add(new Rectangle(580, 280, 120, 225));
            //故障清单中Tc1、Tc2状态   /32-49
            for (i = 0; i < 9; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    m_Rects.Add(new Rectangle(580 + 60 * j, 280 + 25 * i, 60, 25));
                }
            }
            #endregion

            #region:::::::列车线:::::::::
            //左侧15个矩形   /50-64
            for(i=0;i<15;i++)
            {
                m_Rects.Add(new Rectangle(100,150+25*i,460,25));
            }
            //右侧32个矩形  /65-96
            for (i = 0; i < 16; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    m_Rects.Add(new Rectangle(560+60*j, 125+25*i, 60, 25));
                }
            }
            //按钮图片    /97
            m_Rects.Add(new Rectangle(710, 380, 60, 60));
            //按钮区域    /98
            m_Rects.Add(new Rectangle(713, 383, 50, 50));
            #endregion

            #region::::::::数据::::::::::
            //表格框     /99-102
            for (i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    m_Rects.Add(new Rectangle(150 + 320 * i, 180 + 30 * j, 230, 30 + 220 * j));
                }
            }
            //string车辆允许距离   /103-104
            for (i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(150 + 320 * i, 250, 230, 25));
            }
            //kM数值    /105-106
            for (i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(150 + 320 * i, 275, 230, 25));
            }
            //string车轮直径   /107-108
            for (i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(150 + 320 * i, 325, 230, 25));
            }
            //mm值       /109-110
            for (i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(150 + 320 * i, 350, 230, 25));
            }
            //string列车速度    /111-112
            for (i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(150+320 * i, 400, 230, 25));
            }
            //kph值           /113-114
            for(i=0;i<2;i++)
            {
                m_Rects.Add(new Rectangle(150 + 320 * i, 425, 230, 30));
            }
            //1/3 文字        /115
            m_Rects.Add(new Rectangle(750, 555, 50, 20));
            #endregion
        }
    }
}
