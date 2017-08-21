namespace Subway.XiaMenLine1.Interface.Model
{
    public interface ITraction : IViewModel
    {
        Enum.TractionStatus CarTwoTractionStatus { get; }
        Enum.TractionStatus CarThreeTractionStatus { get; }
        Enum.TractionStatus CarFourTractionStatus { get; }
        Enum.TractionStatus CarFiveTractionStatus { get; }

    }
}