using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Motor.ATP.Domain.Interface.SpeedMonitoringSection
{
    public interface ICoordinateAxis 
    {
        /// <summary>
        /// 值
        /// </summary>
        float Value { get; }

        /// <summary>
        /// 位置, 百分比
        /// </summary>
        float LocationPercent { get; }

        /// <summary>
        /// 坐标轴的长度, 高度从上到下, 宽度从左到右
        /// </summary>
        Padding Padding { get; }

        /// <summary>
        /// 轴线
        /// </summary>
        Pen AxisPen { get; }

        /// <summary>
        /// 显示内容
        /// </summary>
        string Text { get; }

        /// <summary>
        /// 文本字体 
        /// </summary>
        Font TextFont { get; }

        /// <summary>
        /// 文本画刷
        /// </summary>
        Brush TextBrush { get; }

    }

    public class CoordinateAxisValueCompare : IComparer<ICoordinateAxis>
    {
        public static CoordinateAxisValueCompare Instance { private set; get; }

        static CoordinateAxisValueCompare()
        {
            Instance = new CoordinateAxisValueCompare();
        }

        public int Compare(ICoordinateAxis x, ICoordinateAxis y)
        {
            return x.Value.CompareTo(y.Value);
        }
    }
}