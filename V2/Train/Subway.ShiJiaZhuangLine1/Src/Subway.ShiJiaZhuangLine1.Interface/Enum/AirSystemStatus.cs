using Subway.ShiJiaZhuangLine1.Interface.Attibutes;

namespace Subway.ShiJiaZhuangLine1.Interface.Enum
{
    public enum AirSystemStatus
    {
        [HelpDescription("空调断开，无故障")]
        AirClosed,
        [HelpDescription("空调故障")]
        AirFault,
        [HelpDescription("空调运行，无故障")]
        AirRunning,

    }
}