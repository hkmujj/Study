using System.Collections.Generic;
using System.Drawing;

namespace Urban.QingDao3Line.MMI
{
    class DoorsData
    {
        private static readonly List<Rectangle> m_Rects = new List<Rectangle>();

        private static readonly List<Rectangle> line_Rects = new List<Rectangle>();

        public static List<Rectangle> Line_Rects
        {
            get { return Line_Rects; }
        }
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
            //0-1 上侧两个按钮图片
            for (int i=0;i<2;i++)
            {
                m_Rects.Add(new Rectangle(10+i*720,180,60,60));
            }
            //2-3 下侧两个按钮图片
            for (int i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(90 + i * 540, 205, 60, 60));
            }
            //4-5 上侧两个按钮区域
            for (int i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(15 + i * 720, 185, 50, 50));
            }
            //6-7 下侧两个按钮区域
            for (int i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(95 + i * 540, 210, 50, 50));
            }
            //车厢图片位置     /8 
            m_Rects.Add(new Rectangle(156, 220, 465, 30));
            //车厢图片中车门位置矩形 从上到下、从左到右     /9-16
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    m_Rects.Add(new Rectangle(166+125*i,220+20*j,60,10));
                }
            }                                                                          
            //左侧DCU 左面10个矩形   /17-26
            for (int i=0;i<10;i++)
            {
                m_Rects.Add(new Rectangle(15, 280+i*25, 320, 25));
            }
            //左侧DCU5 右面11个矩形   /27-37
            for (int i = 0; i < 11; i++)
            {
                m_Rects.Add(new Rectangle(335, 255 + i * 25, 60, 25));
            }
            //右侧DCU5 左面10个        /38-47
            for (int i = 0; i < 10; i++)
            {
                m_Rects.Add(new Rectangle(410, 280 + i * 25, 320, 25));
            }
            //右侧DCU5 右面11个      /48-58
            for (int i = 0; i < 11; i++)
            {
                m_Rects.Add(new Rectangle(730, 255 + i * 25, 60, 25));
            }
            for (int i = 0; i < 4; i++)
            {
                m_Regions.Add(new Region(m_Rects[i]));
            }

            //列车绘制
            //车头1，2       /0-1
            for (int i = 0; i < 2; i++)
            {
                line_Rects.Add(new Rectangle (8 + 748 * i, 114, 34, 76));
            }
            //驾驶室1,2      /2-3
            for (int i = 0; i < 2; i++)
            {
                line_Rects.Add(new Rectangle(40 + 701 * i, 113, 16, 78));
            }
            //驾驶室1,2车门  /4-7
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    line_Rects.Add(new Rectangle(42 + 701 * i, 118 + 45 * j, 12, 24));
                }
            }
            //6节车厢       /8-13
            for (int i = 0; i < 6; i++)
            {
                line_Rects.Add(new Rectangle(58 + 114 * i, 113, 111, 78));
            }
            //车厢门        /14-61

            for (int i = 0; i < 6; i++)
            {
                for (int m = 0; m < 4; m++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        line_Rects.Add(new Rectangle(62 + 27 * m + 114 * i, 119 + 45 * j, 23, 23));
                    }
                }
            }    
        }
    }
}
