using System.Drawing;

namespace MMITool.Common.Util.Extension
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
    }
}
