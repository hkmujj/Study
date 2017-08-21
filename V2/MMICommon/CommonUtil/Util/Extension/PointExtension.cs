using System.Drawing;

namespace CommonUtil.Util.Extension
{
    /// <summary>
    /// 
    /// </summary>
    public static class PointExtension
    {
        /// <summary>
        /// Point 定制 Tostring的方法
        /// </summary>
        /// <param name="point"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToString(this Point point, string format)
        {
            return string.Format(format, point.X, point.Y);
        }

        /// <summary>
        /// PointF 定制 Tostring的方法
        /// </summary>
        /// <param name="point"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToString(this PointF point, string format)
        {
            return string.Format(format, point.X, point.Y);
        }

        /// <summary>
        /// 平移
        /// </summary>
        /// <param name="point"></param>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        /// <returns></returns>
        public static Point Translate(this Point point, int offsetX, int offsetY)
        {
            point.X += offsetX;
            point.Y += offsetY;
            return point;
        }

        /// <summary>
        /// 平移
        /// </summary>
        /// <param name="point"></param>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        /// <returns></returns>
        public static PointF Translate(this PointF point, float offsetX, float offsetY)
        {
            point.X += offsetX;
            point.Y += offsetY;
            return point;
        }

        /// <summary>
        /// 平移
        /// </summary>
        /// <param name="point"></param>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        /// <returns></returns>
        public static PointF Translate(this PointF point, int offsetX, int offsetY)
        {
            point.X += offsetX;
            point.Y += offsetY;
            return point;
        }

    }
}
