using System;
using System.Drawing;

namespace CommonUtil.Controls
{
    /// <summary>
    /// 多边形
    /// </summary>
    [Obsolete("功能未完善")]
    public class PolygonRegion
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public PolygonRegion(Point[] points)
        {
            Points = points;
            int minx = int.MaxValue;
            int miny = int.MaxValue;
            int maxx = int.MinValue;
            int maxy = int.MinValue;
            foreach (var point in points)
            {
                if (minx >= point.X)
                {
                    minx = point.X;
                }
                if (miny >= point.Y)
                {
                    miny = point.Y;
                }
                if (maxx <= point.X)
                {
                    maxx = point.X;
                }
                if (maxy <= point.Y)
                {
                    maxy = point.Y;
                }
            }
            m_OutLineRectangle = new Rectangle(minx, miny, maxx - minx, maxy - miny);
        }

        //private List<Point> m_Points;

        /// <summary>
        /// 轮廓矩形
        /// </summary>
        private Rectangle m_OutLineRectangle;

        /// <summary>
        /// 所有的点
        /// </summary>
        public Point[] Points { private set; get; }

        /// <summary>
        /// TODO 准确化
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool Contains(Point point)
        {
            return m_OutLineRectangle.Contains(point);
        }

    }
}
