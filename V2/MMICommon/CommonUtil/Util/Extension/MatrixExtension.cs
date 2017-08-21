using System.Drawing;
using System.Drawing.Drawing2D;

namespace CommonUtil.Util.Extension
{
    /// <summary>
    /// MatrixEx
    /// </summary>
    public static class MatrixExtension
    {
        /// <summary>
        /// 转换单个坐标
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Point TransformPoint(this Matrix matrix, Point point)
        {
            var p = new[] {point};
            matrix.TransformPoints(p);
            return p[0];
        }

        /// <summary>
        /// 转换单个坐标
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static PointF TransformPoint(this Matrix matrix, float x, float y)
        {
            var p = new[] { new PointF(x, y)  };
            matrix.TransformPoints(p);
            return p[0];
        }

        /// <summary>
        /// 转换单个坐标
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Point TransformPoint(this Matrix matrix, int x, int y )
        {
            var p = new[] { new PointF(x, y) };
            matrix.TransformPoints(p);
            return Point.Truncate(p[0]);
        }

        /// <summary>
        /// 转换单个坐标
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static PointF TransformPoint(this Matrix matrix, PointF point)
        {
            var p = new[] { point };
            matrix.TransformPoints(p);
            return p[0];
        }
    }
}
