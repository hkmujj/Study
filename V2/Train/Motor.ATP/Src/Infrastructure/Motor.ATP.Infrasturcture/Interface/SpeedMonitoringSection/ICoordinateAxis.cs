using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Motor.ATP.Infrasturcture.Interface.SpeedMonitoringSection
{
    /// <summary>
    /// 坐标
    /// </summary>
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

    /// <summary>
    /// CoordinateAxisValueCompare
    /// </summary>
    public class CoordinateAxisValueCompare : IComparer<ICoordinateAxis>
    {
        /// <summary>
        /// 单例
        /// </summary>
        public static CoordinateAxisValueCompare Instance { private set; get; }

        static CoordinateAxisValueCompare()
        {
            Instance = new CoordinateAxisValueCompare();
        }

        /// <summary>比较两个对象并返回一个值，指示一个对象是小于、等于还是大于另一个对象。</summary>
        /// <returns>一个带符号整数，它指示 <paramref name="x" /> 与 <paramref name="y" /> 的相对值，如下表所示。值含义小于零<paramref name="x" /> 小于 <paramref name="y" />。零<paramref name="x" /> 等于 <paramref name="y" />。大于零<paramref name="x" /> 大于 <paramref name="y" />。</returns>
        /// <param name="x">要比较的第一个对象。</param>
        /// <param name="y">要比较的第二个对象。</param>
        public int Compare(ICoordinateAxis x, ICoordinateAxis y)
        {
            return x.Value.CompareTo(y.Value);
        }
    }
}