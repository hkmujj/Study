using Subway.ShiJiaZhuangLine1.Interface.Enum;

namespace Subway.ShiJiaZhuangLine1.Interface.Model
{
    public interface IBrake : IViewModel
    {
        BrakeStatus Car1BrakeNormal1 { get; }
        BrakeStatus Car2BrakeNormal1 { get; }
        BrakeStatus Car3BrakeNormal1 { get; }
        BrakeStatus Car4BrakeNormal1 { get; }
        BrakeStatus Car5BrakeNormal1 { get; }
        BrakeStatus Car6BrakeNormal1 { get; }
        BrakeStatus Car1BrakeNormal2 { get; }
        BrakeStatus Car2BrakeNormal2 { get; }
        BrakeStatus Car3BrakeNormal2 { get; }
        BrakeStatus Car4BrakeNormal2 { get; }
        BrakeStatus Car5BrakeNormal2 { get; }
        BrakeStatus Car6BrakeNormal2 { get; }
        BrakeStatus Car1BrakeNormal3 { get; }
        BrakeStatus Car2BrakeNormal3 { get; }
        BrakeStatus Car3BrakeNormal3 { get; }
        BrakeStatus Car4BrakeNormal3 { get; }
        BrakeStatus Car5BrakeNormal3 { get; }
        BrakeStatus Car6BrakeNormal3 { get; }
        BrakeStatus Car1BrakeParking { get; }
        BrakeStatus Car2BrakeParking { get; }
        BrakeStatus Car3BrakeParking { get; }
        BrakeStatus Car4BrakeParking { get; }
        BrakeStatus Car5BrakeParking { get; }
        BrakeStatus Car6BrakeParking { get; }

    }
}