using System.Drawing;

namespace CRH2MMI.Common
{
    /// <summary>
    /// 资源矩阵
    /// </summary>
    public class ResourceMatrix<T>
    {

        public T Resource { set; get; }

        /// <summary>
        /// 坐标偏移, 索引值,非像素
        /// </summary>
        public Point LocationOffset { set; get; }

    }
}
