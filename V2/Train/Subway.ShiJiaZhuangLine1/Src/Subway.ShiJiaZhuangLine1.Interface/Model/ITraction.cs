using Subway.ShiJiaZhuangLine1.Interface.Enum;

namespace Subway.ShiJiaZhuangLine1.Interface.Model
{
    public interface ITraction : IViewModel
    {
        TractionStatus CarTwoTractionStatus { get; }
        TractionStatus CarThreeTractionStatus { get; }
        TractionStatus CarFourTractionStatus { get; }
        TractionStatus CarFiveTractionStatus { get; }

    }
}