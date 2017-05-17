using Subway.ShiJiaZhuangLine1.Interface.Attibutes;

namespace Subway.ShiJiaZhuangLine1.Interface.Enum
{
    public enum AirPumpStatus
    {
        Normal,
        Select,
        Falut,

        [HelpDescription("空气压缩机严重故障")]
        AirFault,
        [HelpDescription("空气压缩机运行，无故障")]
        AirRunning,
        [HelpDescription("空气压缩机断开")]
        AirClosed,
        [HelpDescription("空气压缩机状态未知")]
        AirStateUnknown,
    }
}