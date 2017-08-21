using System;
using System.Drawing;
using CommonUtil.Model;

namespace CommonUtil.Util.Extension
{
    /// <summary>
    /// 矩形 的扩展方法
    /// </summary>
    public static class RectangleExtension
    {
        /// <summary>
        /// 获得指定方向的矩形中点
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static Point GetMidPoint(this Rectangle rect, Direction direction)
        {
            return rect.GetPoint(direction, 0.5f);
        }

        /// <summary>
        /// 获得指定方向的矩形的指定比例的点
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="direction"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        public static Point GetPoint(this Rectangle rect, Direction direction, float rate)
        {
            switch (direction)
            {
                case Direction.Up:
                    return Point.Round(new PointF((rect.Left + rect.Right) * rate, rect.Y));
                case Direction.Down:
                    return Point.Round(new PointF((rect.Left + rect.Right) * rate, rect.Bottom));
                case Direction.Left:
                    return Point.Round(new PointF(rect.Left, (rect.Y + rect.Bottom) * rate));
                case Direction.Right:
                    return Point.Round(new PointF(rect.Right, (rect.Y + rect.Bottom)* rate));
                default:
                    throw new ArgumentOutOfRangeException("direction");
            }
        }

        /// <summary>
        /// 获得中心点
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static Point GetCenterPoint(this Rectangle rect)
        {
            return new Point((rect.Left + rect.Right) / 2, (rect.Top + rect.Bottom) / 2);
        }

        /// <summary>
        /// 获得中心点
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static PointF GetCenterPoint(this RectangleF rect)
        {
            return new PointF((rect.Left + rect.Right) / 2, (rect.Top + rect.Bottom) / 2);
        }

        /// <summary>
        /// 获得指定方向的矩形中点
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static PointF GetMidPoint(this RectangleF rect, Direction direction)
        {
            return rect.GetPoint(direction, 0.5f);
        }

        /// <summary>
        /// 获得指定方向的矩形的指定比例的点
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="direction"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        public static PointF GetPoint(this RectangleF rect, Direction direction, float rate)
        {
            switch (direction)
            {
                case Direction.Up:
                    return new PointF((rect.Left + rect.Right) * rate, rect.Y);
                case Direction.Down:
                    return new PointF((rect.Left + rect.Right) * rate, rect.Bottom);
                case Direction.Left:
                    return new PointF(rect.Left, (rect.Y + rect.Bottom) * rate);
                case Direction.Right:
                    return new PointF(rect.Right, (rect.Y + rect.Bottom) * rate);
                default:
                    throw new ArgumentOutOfRangeException("direction");
            }
        }
    }

}
