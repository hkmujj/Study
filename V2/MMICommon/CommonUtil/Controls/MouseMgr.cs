using System.Drawing;

namespace CommonUtil.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class MouseMgr
    {
        private static MouseMgr m_Instance;

        /// <summary>
        /// 
        /// </summary>
        public static MouseMgr Instance
        {
            get { return m_Instance ?? (m_Instance = new MouseMgr()); }
        }

        private MouseMgr()
        {

        }

        /// <summary>
        /// 鼠标点下的点
        /// </summary>
        public Point MouseDownPoint { set; get; }

        /// <summary>
        /// 鼠标释放的点
        /// </summary>
        public Point MouseUpPoint { set; get; }
    }
}
