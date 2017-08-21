using System.Collections.ObjectModel;

namespace Subway.CBTC.THALES.Model
{
    public interface ITargitDistanceScale
    {
        ReadOnlyCollection<ITargitDistanceScaleItem> Items { get; }
        double GetLocation(double value);
        string GetDistanceText(double distance);
        double GetDgreeLenth(double distance);
    }
}