using System.Collections.Generic;
using System.Drawing;

namespace Urban.QingDao3Line.MMI
{
    class AuxiliaryData
    {
        private static readonly List<Rectangle> m_Rects = new List<Rectangle>();
        private static readonly List<PointF> m_Points = new List<PointF>();

        public static List<Rectangle> Rects
        {
            get { return m_Rects; }
        }

        public static List<PointF> Points
        {
            get { return m_Points; }
        }

        private static readonly List<Region> m_Regions = new List<Region>();

        public static List<Region> Regions
        {
            get { return m_Regions; }
        }

        public static void InitData()
        {
            //底图
            m_Rects.Add(new Rectangle(0, 113, 794, 424));
            //矩形框上半部分
            //1
            m_Rects.Add(new Rectangle(10,175,50,24));
            //2
            m_Rects.Add(new Rectangle(167, 152, 38, 20));
            //3
            m_Rects.Add(new Rectangle(318, 152, 38, 20));
            //4
            m_Rects.Add(new Rectangle(485, 140, 39, 19));
            //5-6
            for (int i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(530, 225 + i * 28, 50, 24));
            }
            //7-8
            for (int i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(608, 197 + i * 28, 50, 24));
            }
            //9
            m_Rects.Add(new Rectangle(613, 141, 50, 24));
            //矩形框下半部分
            //10
            m_Rects.Add(new Rectangle(10, 368, 50, 24));
            //11
            m_Rects.Add(new Rectangle(167, 345, 38, 20));
            //12
            m_Rects.Add(new Rectangle(318, 345, 38, 20));
            //13
            m_Rects.Add(new Rectangle(485, 332, 39, 19));
            //14-15
            for (int i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(530, 417 + i * 28, 50, 24));
            }
            //16-17
            for (int i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(608, 389 + i * 28, 50, 24));
            }
            //18
            m_Rects.Add(new Rectangle(613, 333, 50, 24));
            
            //19-20 cvs1-cvs2
            for (int i=0;i<2;i++)
            {
                m_Rects.Add(new Rectangle(326, 115+i*190, 140, 46));
            }
            //左侧按钮 21
            m_Rects.Add(new Rectangle(7, 462, 58, 58));
            //扩展供电接触  /22
            m_Rects.Add(new Rectangle(730,294,54,30));
            //故障事件  /23
            m_Rects.Add(new Rectangle(12, 469, 48, 48));
            //维护：辅助逆变器   /24
            m_Rects.Add(new Rectangle(350, 90, 200, 30));
            m_Regions.Add(new Region(m_Rects[21]));

            //Filter1横线  断开线、 闭合线   0-3
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    m_Points.Add(new PointF(115 + 20 * j, 194+5*i));
                }
                    
            }
            //Filter1横线  断开线、 闭合线   4-7
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    m_Points.Add(new PointF(115 + 20 * j, 218 + 5 * i));
                }
            }
            //AOIK横线  断开线、 闭合线   8-11
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    m_Points.Add(new PointF(495 + 20 * j, 271 + 5 * i));
                }
            }
            //K Bat横线  断开线、 闭合线   12-15
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    m_Points.Add(new PointF(585 + 20 * j, 164 + 5 * i));
                }
            }
            //下部分图横线
            //Filter1横线  断开线、 闭合线   16-19
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    m_Points.Add(new PointF(115 + 20 * j, 384 + 5 * i));
                }

            }
            //Filter1横线  断开线、 闭合线   20-23
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    m_Points.Add(new PointF(115 + 20 * j, 408 + 5 * i));
                }
            }
            //AOIK横线  断开线、 闭合线   24-27
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    m_Points.Add(new PointF(495 + 20 * j, 461 + 5 * i));
                }
            }
            //K Bat横线  断开线、 闭合线   28-31
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    m_Points.Add(new PointF(585 + 20 * j, 354 + 5 * i));
                }
            }
            //扩展供电接触器处横线   断开线、闭合线 32-35
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    m_Points.Add(new PointF(735 - 5 * i, 349 + 20 * j));
                }
            }
        }

    }
}
