using System.Drawing;
using System.Drawing.Drawing2D;

namespace CommonUtil.Util.Extension
{
    /// <summary>
    /// GraphicsPathExtension
    /// </summary>
    public static class GraphicsPathExtension
    {
        /// <summary>
        /// /// 平移
        /// </summary>
        /// <param name="path"></param>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        /// <returns></returns>
        public static GraphicsPath Translate(this GraphicsPath path, float offsetX, float offsetY)
        {
            var mat = new Matrix();
            mat.Translate(offsetX, offsetY);
            path.Transform(mat);
            return path;
        }

        /// <summary>
        /// 平移
        /// </summary>
        /// <param name="path"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static GraphicsPath Translate(this GraphicsPath path, Point offset)
        {
            return path.Translate(offset.X, offset.Y);
        }

        /// <summary>
        /// 平移
        /// </summary>
        /// <param name="path"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static GraphicsPath Translate(this GraphicsPath path, PointF offset)
        {
            return path.Translate(offset.X, offset.Y);
        }
    }
}
