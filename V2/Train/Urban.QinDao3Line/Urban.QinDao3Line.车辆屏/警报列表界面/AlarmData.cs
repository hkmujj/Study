using System.Collections.Generic;
using System.Drawing;

namespace Urban.QingDao3Line.MMI
{
    class AlarmData : NewQBaseclass
    {
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

            //RIOM R1标题位置     /0
            m_Rects.Add(new Rectangle(300, 75, 200, 30));
            //四个按钮图片   上、下、左、右       /1-4
            for (int i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(660, 280+120*i, 60, 60));
            }
            for (int i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(600+120*i, 340, 60, 60));
            }
            //四个按钮区域   上、下、左、右       /5-8
            for (int i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(665, 285 + 120 * i, 50, 50));
            }
            for (int i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(605 + 120 * i, 345, 50, 50));
            }
            //RIOM 选择                   /9
            m_Rects.Add(new Rectangle(660,340,60,60));
            //左边32个指示灯位置,从上到下，从左到右          /10-41             
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    m_Rects.Add(new Rectangle(30+250*i, 108+22*j, 25, 25));
                }
            }
            //右边6个指示灯位置                        /42-47
            for (int i = 0; i < 6; i++)
            {
                m_Rects.Add(new Rectangle(530, 108 + 22 * i, 25, 25));
            }
            //左边32个矩形位置                        /48-79
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    m_Rects.Add(new Rectangle(58 + 250 * i, 110 + 22 * j, 200, 22));
                }
            }
            //右边6个矩形位置                         /80-85
            for (int i = 0; i < 6; i++)
            {
                m_Rects.Add(new Rectangle(558, 110 + 22 * i, 200, 22));
            }
            //下边的4个表格 从左到右                /86-93
            for (int i = 0; i < 2;i++ )
            {
                m_Rects.Add(new Rectangle(58, 490 + 22 * i, 120, 22));
            }
            for (int i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(185, 490 + 22 * i, 200, 22));  
            }
            for (int i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(440, 490 + 22 * i, 120, 22));
            }
            for (int i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(567, 490 + 22 * i, 200, 22));
            }

            //Region区域
            for (int i = 0; i < 4; i++)
            {
                m_Regions.Add(new Region(m_Rects[1 + i]));
            }
        }
    }
}
