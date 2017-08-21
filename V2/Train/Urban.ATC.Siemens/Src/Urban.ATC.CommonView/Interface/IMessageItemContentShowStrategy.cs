using System.Drawing;
using Motor.ATP.CommonView.View.RegionE;

namespace Motor.ATP.CommonView.Interface
{
    public interface IMessageItemContentShowStrategy
    {
        /// <summary>
        /// 行数
        /// </summary>
        int LineCount { get; }

        void Paint(MessageItemControl control, Graphics g);

    }
}