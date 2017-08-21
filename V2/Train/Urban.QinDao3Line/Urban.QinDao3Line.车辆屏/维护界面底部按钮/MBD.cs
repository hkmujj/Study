using System.Collections.Generic;
using System.Drawing;
using Urban.QingDao3Line.MMI.底层共用;

namespace Urban.QingDao3Line.MMI
{
    class Mbd
    {
        private static List<RectangleF> m_Rects = new List<RectangleF>();
        public static List<RectangleF> Rects
        {
            get { return m_Rects; }
            set { Rects = value; }
        }

        private static readonly List<Point> m_Points=new List<Point>();

        public static List<Point> Points
        {
            get { return m_Points; } 
        }

        private static readonly List<Region> m_Regions = new List<Region>();

        public static List<Region> Region
        {
            get { return m_Regions; }
        }

        public static void InitData()
        {
            m_Points.Add(new Point(0, 547));
            m_Points.Add(new Point(800, 547));
            //按钮变色部分的矩形   /0-3
            Coordinate.AddRectangle(ViewIDName.TitleScreen, ref m_Rects, 26);
            for (int i = 0; i < 3; i++)
            {
                Coordinate.AddRectangle(ViewIDName.TitleScreen, ref m_Rects, 33 +i);
            }
            //按钮图片     /4-7
            Coordinate.AddRectangle(ViewIDName.TitleScreen, ref m_Rects, 36);
            for (int i = 0; i < 3; i++)
            {
                Coordinate.AddRectangle(ViewIDName.TitleScreen, ref m_Rects, 43 + i);
            }
            //记录鼠标是否在按钮所在区域按下
            for (int i=0;i<4;i++)
            {
                m_Regions.Add(new Region(m_Rects[i]));
            }
        }
    }
}
