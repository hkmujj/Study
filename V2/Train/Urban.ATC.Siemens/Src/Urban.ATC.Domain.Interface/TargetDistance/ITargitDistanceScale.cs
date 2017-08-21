using System.Collections.ObjectModel;
using System.Drawing;

namespace Motor.ATP.Domain.Interface.TargetDistance
{
    /// <summary>
    /// 目标距离刻度
    /// </summary>
    public interface ITargitDistanceScale
    {
        ReadOnlyCollection<ITargitDistanceScaleItem> TargitDistanceScaleItems { get; }

        double GetLocatoin(double value);

        string GetDistanceText(double distance);
    }

    public interface ITargitDistanceScaleItem
    {
        /// <summary>
        /// 刻度值
        /// </summary>
        double Value { get; }

        /// <summary>
        /// 刻度长度， 比例值
        /// </summary>
        double DegreeLength { get; }

        /// <summary>
        /// 刻度位置 ， 比例值
        /// </summary>
        double DegreeLocation { get; }


        Pen DegreePen { get; }

        /// <summary>
        /// 显示文本
        /// </summary>
        string Text { get; }

    }
}