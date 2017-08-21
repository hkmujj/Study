using System.Drawing;

namespace CommonUtil.Util
{
    /// <summary>
    /// 线的帮助类
    /// </summary>
    public class LineHelper
    {
        /// <summary>
        /// 获得两个点的中点
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static Point GetMidPoint(Point start, Point end)
        {
            return new Point((start.X + end.X) / 2, (start.Y + end.Y) / 2);
        }

        /// <summary>
        /// 获得两个点的中点
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static PointF GetMidPoint(PointF start, PointF end)
        {
            return new PointF((start.X + end.X) / 2, (start.Y + end.Y) / 2);
        }


    }
}
