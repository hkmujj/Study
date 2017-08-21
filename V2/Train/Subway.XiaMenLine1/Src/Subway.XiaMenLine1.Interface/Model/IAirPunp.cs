namespace Subway.XiaMenLine1.Interface.Model
{
    public interface IAirPunp : IViewModel
    {
        double Car2CompressValue { get; }
        double Car5CompressValue { get; }
        Enum.AirPumpStatus Car3AirPumpStatus { get; }
        Enum.AirPumpStatus Car4AirPumpStatus { get; }

    }
}