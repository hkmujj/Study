using Subway.ShiJiaZhuangLine1.Interface.Enum;

namespace Subway.ShiJiaZhuangLine1.Interface.Model
{
    public interface IAirPunp : IViewModel
    {
        double Car2CompressValue { get; }
        double Car5CompressValue { get; }
        AirPumpStatus Car3AirPumpStatus { get; }
        AirPumpStatus Car4AirPumpStatus { get; }

    }
}