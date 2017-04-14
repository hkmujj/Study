using System.Drawing;
using MMITool.Common.Model;

namespace MMITool.Common.Util.Extension
{
    /// <summary>
    /// IRectangle 的扩展方法
    /// </summary>
    public static class ExOfIRectangleAndIRectangleF
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iRectangle"></param>
        /// <returns></returns>
        public static Rectangle GetRectangle(this IRectangle iRectangle)
        {
            return new Rectangle(iRectangle.X, iRectangle.Y, iRectangle.Width, iRectangle.Height);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iRectangle"></param>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public static void SetRectangle(this IRectangle iRectangle, Rectangle rectangle)
        {
            iRectangle.X = rectangle.X;
            iRectangle.Y = rectangle.Y;
            iRectangle.Width = rectangle.Width;
            iRectangle.Height = rectangle.Height;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iRectangle"></param>
        /// <returns></returns>
        public static RectangleF GetRectangleF(this IRectangleF iRectangle)
        {
            return new RectangleF(iRectangle.X, iRectangle.Y, iRectangle.Width, iRectangle.Height);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iRectangleF"></param>
        /// <param name="rectangleF"></param>
        /// <returns></returns>
        public static void SetRectangleF(this IRectangleF iRectangleF, RectangleF rectangleF)
        {
            iRectangleF.X = rectangleF.X;
            iRectangleF.Y = rectangleF.Y;
            iRectangleF.Width = rectangleF.Width;
            iRectangleF.Height = rectangleF.Height;
        }

    }
}
