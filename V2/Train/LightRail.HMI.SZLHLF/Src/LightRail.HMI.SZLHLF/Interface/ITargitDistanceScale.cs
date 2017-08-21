using System.Collections.ObjectModel;

namespace LightRail.HMI.SZLHLF.Interface
{
    /// <summary>
    /// 目标距离刻度
    /// </summary>
    public interface ITargitDistanceScale
    {
        ReadOnlyCollection<ITargitDistanceScaleItem> TargitDistanceScaleItems { get; }

        double GetLocatoin(double value);

        string GetDistanceText(double distance);

        //string GetDistanceText1(double distance);
    }
}
