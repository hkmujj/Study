using System.Collections.ObjectModel;

namespace Tram.CBTC.Infrasturcture.Model.Signal.Warning
{
    /// <summary>
    /// 目标距离刻度
    /// </summary>
    public interface ITargitDistanceScale
    {
        /// <summary>
        /// 
        /// </summary>
        ReadOnlyCollection<TargitDistanceScaleItem> TargitDistanceScaleItems { get; }

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
}