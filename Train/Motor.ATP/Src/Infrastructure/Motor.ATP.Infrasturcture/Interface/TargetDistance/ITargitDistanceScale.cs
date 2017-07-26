using System.Collections.ObjectModel;
using System.Drawing;

namespace Motor.ATP.Infrasturcture.Interface.TargetDistance
{
    /// <summary>
    /// 目标距离刻度
    /// </summary>
    public interface ITargitDistanceScale
    {
        /// <summary>
        /// 
        /// </summary>
        ReadOnlyCollection<ITargitDistanceScaleItem> TargitDistanceScaleItems { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        double GetLocatoin(double value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        string GetDistanceText(double distance);
    }

    /// <summary>
    /// 
    /// </summary>
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


        /// <summary>
        /// 刻度 Pen
        /// </summary>
        Pen DegreePen { get; }

        /// <summary>
        /// 显示文本
        /// </summary>
        string Text { get; }

    }
}