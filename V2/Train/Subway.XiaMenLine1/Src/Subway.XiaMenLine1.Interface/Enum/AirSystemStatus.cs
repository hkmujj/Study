using Subway.XiaMenLine1.Interface.Attibutes;

namespace Subway.XiaMenLine1.Interface.Enum
{
    public enum AirSystemStatus
    {
        [HelpDescription("空调断开，无故障")]
        AirClosed,
        [HelpDescription("空调故障")]
        AirFault,
        [HelpDescription("空调运行，无故障")]
        AirRunning,
        [HelpDescription("空调系统通讯故障")]
        AirConmmunicationFault

    }
}