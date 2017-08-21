using Subway.XiaMenLine1.Interface.Attibutes;

namespace Subway.XiaMenLine1.Interface.Enum
{
    public enum TractionStatus
    {
        Normal,
        Select,
        Cut,
        Falut,
        [HelpDescription("牵引故障")]
        TractionFault,
        [HelpDescription("牵引激活，无故障")]
        TractionActive,
        [HelpDescription("牵引切除")]
        TractionCut,
        [HelpDescription("牵引断开，无故障")]
        TractionClosed,
        [HelpDescription("牵引系统通讯故障")]
        TractionCommunicationFault,
        [HelpDescription("牵引系统未知")]
        TractionUnknow,
    }
}