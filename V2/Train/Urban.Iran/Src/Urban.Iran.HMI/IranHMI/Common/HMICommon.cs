using System.Drawing;

namespace Urban.Iran.HMI.Common
{
    public static class HMICommon
    {
        /// <summary>
        /// 中间显示区域， 除去title ， buttom, active event 后的区域
        /// </summary>
        public static Rectangle ContentRectangle { private set; get; }

        static HMICommon()
        {
            ContentRectangle = new Rectangle(0, 66, 800, 404);
        }
    }
}