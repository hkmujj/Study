using Subway.ShiJiaZhuangLine1.Interface.Enum;

namespace Subway.ShiJiaZhuangLine1.Interface.Model
{
    public interface IAirCondition : IViewModel
    {
        AirSystemStatus Car1Air1 { get; }
        AirSystemStatus Car2Air1 { get; }
        AirSystemStatus Car3Air1 { get; }
        AirSystemStatus Car4Air1 { get; }
        AirSystemStatus Car5Air1 { get; }
        AirSystemStatus Car6Air1 { get; }
        AirSystemStatus Car1Air2 { get; }
        AirSystemStatus Car2Air2 { get; }
        AirSystemStatus Car3Air2 { get; }
        AirSystemStatus Car4Air2 { get; }
        AirSystemStatus Car5Air2 { get; }
        AirSystemStatus Car6Air2 { get; }

    }
}