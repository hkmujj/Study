using System.Collections.Generic;
using System.Drawing;

namespace Urban.QingDao3Line.MMI
{
    class MVBNetData
    {
        private static readonly List<Rectangle> m_Rects = new List<Rectangle>();
        private static readonly List<Point> m_Points = new List<Point>();
        private static int i = 0;

        public static List<Rectangle> Rects
        {
            get { return m_Rects; }
        }

        public static List<Point> myPoints
        {
            get { return m_Points; }
        }

        public static void InitData()
        {
            #region:::::::矩形:::::::::::
            //维护：MVB网络   /0
            m_Rects.Add(new Rectangle(320, 80, 200, 30));
            //车头1，2       /1-2
            for (i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(8 + 749 * i, 120, 34, 76));
            }
            //车厢           /3-8
            for (i = 0; i < 6; i++)
            {
                m_Rects.Add(new Rectangle(39 + 120 * i, 119, 119, 77));
            }
            //string M      /9                             
            m_Rects.Add(new Rectangle(110, 225, 25, 25));
            //左右两边各10个矩形 共20个            /10-29               
            for (i = 0; i < 2; i++)
            {
                for (int m = 0; m < 5; m++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        m_Rects.Add(new Rectangle(10 + 72 * j + 660 * i, 250 + 23 * m, 50, 20));
                    }
                }
            }
            //左右两边各一个ATC                 /30-31
            for (i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(82 + 660 * i, 380, 50, 20));
            }
            //左右两边各 PCE、VAC、RIOM        /32-37
            for (int i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(142 + 396 * i, 296, 50, 20));
                m_Rects.Add(new Rectangle(214 + 396 * i, 296, 50, 20));
                m_Rects.Add(new Rectangle(214 + 396 * i, 319, 50, 20));
            }
            //中间两组PCE、BCU、VAC、RIOM      //38-45
            for (int i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(274 + 132 * i, 296, 50, 20));      //PCE
                m_Rects.Add(new Rectangle(346 + 132 * i, 296, 50, 20));      //VAC
                m_Rects.Add(new Rectangle(274 + 132 * i, 319, 50, 20));      //BCU
                m_Rects.Add(new Rectangle(346 + 132 * i, 319, 50, 20));      //RIOM
            }
            //下方48个矩形                    //46-93
            for (i = 0; i < 6; i++)
            {
                for (int m = 0; m < 4; m++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        m_Rects.Add(new Rectangle(10 + 72 * j + 132 * i, 450 + 23 * m, 50, 20));
                    }
                }
            }
            #endregion

            #region::::::::::::点:::::::::::::::::
            //DDU之间的10绿线          /0-19
            for (i = 10; i < 30; i++)
            {
                if (i % 2 == 0)
                {
                    m_Points.Add(new Point(m_Rects[i].X + m_Rects[i].Width,
                            m_Rects[i].Y + m_Rects[i].Height / 2));
                }
                else
                    m_Points.Add(new Point(m_Rects[i].X,
                        m_Rects[i].Y + m_Rects[i].Height / 2));
            }
            //ATC的两根红线         /20-23
            m_Points.Add(new Point(m_Rects[19].X + m_Rects[19].Width / 2,
                            m_Rects[19].Y + m_Rects[19].Height));
            m_Points.Add(new Point(m_Rects[30].X + m_Rects[30].Width / 2,
                            m_Rects[30].Y));
            m_Points.Add(new Point(m_Rects[29].X + m_Rects[29].Width / 2,
                            m_Rects[29].Y + m_Rects[29].Height));
            m_Points.Add(new Point(m_Rects[31].X + m_Rects[31].Width / 2,
                            m_Rects[31].Y));
            //PCE VAC红线         //24-27
            m_Points.Add(new Point(m_Rects[32].X + m_Rects[32].Width,
                            m_Rects[32].Y + m_Rects[32].Height / 2));
            m_Points.Add(new Point(m_Rects[33].X,
                        m_Rects[33].Y + m_Rects[33].Height / 2));
            m_Points.Add(new Point(m_Rects[35].X + m_Rects[35].Width,
                            m_Rects[35].Y + m_Rects[35].Height / 2));
            m_Points.Add(new Point(m_Rects[36].X,
                        m_Rects[36].Y + m_Rects[36].Height / 2));
            //PCE VAC  BCU RIOM红线4根          //28-35
            for (i = 38; i < 46; i++)
            {
                if (i % 2 == 0)
                {
                    m_Points.Add(new Point(m_Rects[i].X + m_Rects[i].Width,
                            m_Rects[i].Y + m_Rects[i].Height / 2));
                }
                else
                    m_Points.Add(new Point(m_Rects[i].X,
                        m_Rects[i].Y + m_Rects[i].Height / 2));
            }
            //最下方水平红线     //36-37
            m_Points.Add(new Point(m_Rects[46].X + m_Rects[46].Width / 2, 420));
            m_Points.Add(new Point(m_Rects[87].X + m_Rects[87].Width / 2, 420));
            //RIOM水平红线       //38-41
            for (int i = 0; i < 2; i++)
            {
                m_Points.Add(new Point(203 + 396 * i, m_Rects[34].Y + m_Rects[34].Height / 2));
                m_Points.Add(new Point(m_Rects[34].X + 396 * i, m_Rects [34].Y+m_Rects [34].Height/2));
            }
            //其他垂直红线18根   //42-77
            for (i = 0; i < 2; i++)
            {
                m_Points.Add(new Point(71 + 660 * i, m_Rects[10].Y + m_Rects[10].Height / 2));
                m_Points.Add(new Point(71 + 660 * i, 420));
            }
            for (i = 0; i < 6; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    m_Points.Add(new Point(m_Rects[46].X + m_Rects[46].Width / 2 + 72 * j + 132 * i, 420));
                    m_Points.Add(new Point(m_Rects[46].X + m_Rects[46].Width / 2 + 72 * j + 132 * i, m_Rects[46].Y));
                }
            }
            for (i = 0; i < 4; i++)
            {
                m_Points.Add(new Point(203 + 132 * i, m_Rects[14].Y + m_Rects[14].Height / 2));
                m_Points.Add(new Point(203 + 132 * i, 420));
            }
            //30根黄色的线        //78-137
            for (i = 0; i < 6; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    m_Points.Add(new Point(m_Rects[46].X + m_Rects[46].Width+132*i, m_Rects[46].Y + m_Rects[46].Height / 2+23*j));
                    m_Points.Add(new Point(m_Rects[47].X+132*i, m_Rects[46].Y + m_Rects[46].Height / 2+23*j));
                }
                m_Points.Add(new Point(71 + 132 * i, m_Rects[46].Y + m_Rects[46].Height / 2));
                m_Points.Add(new Point(71 + 132 * i, m_Rects[46].Y + m_Rects[46].Height / 2 + 69));
            }
            #endregion
        }
    }
}
