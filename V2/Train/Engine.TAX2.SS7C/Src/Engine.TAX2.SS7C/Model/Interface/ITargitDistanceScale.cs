using System.Collections.ObjectModel;

namespace Engine.TAX2.SS7C.Model.Interface
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
}