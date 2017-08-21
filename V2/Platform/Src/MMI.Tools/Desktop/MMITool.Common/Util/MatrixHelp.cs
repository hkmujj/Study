using System.Drawing.Drawing2D;

namespace MMITool.Common.Util
{
    /// <summary>
    /// MatrixHelper
    /// </summary>
    public static class MatrixHelper
    {
        /// <summary>
        /// 创建一个翻转矩阵
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="orientation"></param>
        /// <returns></returns>
        public static Matrix CreateTurnMatrix(float mid, TurnOrientation orientation)
        {
            if (orientation == TurnOrientation.Vertical)
            {
                var mat = new Matrix(1, 0, 0, -1, 0, 0);
                mat.Translate(0, -mid, MatrixOrder.Prepend);
                mat.Translate(0, mid, MatrixOrder.Append);
                return mat;
            }
            else
            {
                var mat = new Matrix(-1, 0, 0, 1, 0, 0);
                mat.Translate(-mid, 0, MatrixOrder.Prepend);
                mat.Translate(mid, 0, MatrixOrder.Append);
                return mat;
            }
        }
    }

    /// <summary>
    /// 翻转方向
    /// </summary>
    public enum TurnOrientation
    {
        /// <summary>
        /// 水平
        /// </summary>
        Horizontal,

        /// <summary>
        /// 垂直
        /// </summary>
        Vertical,
    }
}
