namespace Subway.XiaMenLine1.Interface.Model
{
    public interface IBrake : IViewModel
    {
        Enum.BrakeStatus Car1BrakeNormal1 { get; }
        Enum.BrakeStatus Car2BrakeNormal1 { get; }
        Enum.BrakeStatus Car3BrakeNormal1 { get; }
        Enum.BrakeStatus Car4BrakeNormal1 { get; }
        Enum.BrakeStatus Car5BrakeNormal1 { get; }
        Enum.BrakeStatus Car6BrakeNormal1 { get; }
        Enum.BrakeStatus Car1BrakeNormal2 { get; }
        Enum.BrakeStatus Car2BrakeNormal2 { get; }
        Enum.BrakeStatus Car3BrakeNormal2 { get; }
        Enum.BrakeStatus Car4BrakeNormal2 { get; }
        Enum.BrakeStatus Car5BrakeNormal2 { get; }
        Enum.BrakeStatus Car6BrakeNormal2 { get; }
        Enum.BrakeStatus Car1BrakeNormal3 { get; }
        Enum.BrakeStatus Car2BrakeNormal3 { get; }
        Enum.BrakeStatus Car3BrakeNormal3 { get; }
        Enum.BrakeStatus Car4BrakeNormal3 { get; }
        Enum.BrakeStatus Car5BrakeNormal3 { get; }
        Enum.BrakeStatus Car6BrakeNormal3 { get; }
        Enum.BrakeStatus Car1BrakeParking { get; }
        Enum.BrakeStatus Car2BrakeParking { get; }
        Enum.BrakeStatus Car3BrakeParking { get; }
        Enum.BrakeStatus Car4BrakeParking { get; }
        Enum.BrakeStatus Car5BrakeParking { get; }
        Enum.BrakeStatus Car6BrakeParking { get; }

    }
}