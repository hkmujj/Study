namespace Subway.CBTC.THALES.Model
{
    public interface ITargitDistanceScaleItem
    {
        double Value { get; set; }
        double DegreeLength { get; set; }
        double DegreeLocation { get; set; }
        string Text { get; set; }
    }
}
