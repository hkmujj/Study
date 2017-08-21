using System.Collections.ObjectModel;

namespace Motor.TCMS.CRH400BF.Model.Interface
{
    public interface ITargitDistanceScale
    {
        ReadOnlyCollection<ITargitDistanceScaleItem> TargitDistanceScaleItems { get; }

        double GetLocatoin(double value);

        string GetDistanceText(double distance);
    }
}
