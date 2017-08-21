using Subway.XiaMenLine1.Interface.Attibutes;

namespace Subway.XiaMenLine1.Interface.Enum
{
    public enum FrsmHighSpeed
    {
        Normal,
        Select,
        Fault,
        [HelpDescription("高速断路器合上")]
        HighJoin,
        [HelpDescription("高速断路器断开")]
        HighDisconnect,
        [HelpDescription("高速断路器故障")]
        HighFalut,
        [HelpDescription("高速断路器通讯故障")]
        HighCommunicationFault,

        [HelpDescription("车间电源连接，且供电")]
        FramConect,
        [HelpDescription("车间电源连接，未供电")]
        FramCut,
        [HelpDescription("车间电源故障")]
        FramFault,
        [HelpDescription("车间电源状态未知")]
        FramUnKnow,

        [HelpDescription("受电弓正常升起（过程中闪烁）")]
        PantographUp,
        [HelpDescription("受电弓正常降下（过程中闪烁）")]
        PantographDown,
        [HelpDescription("受电弓故障（升起或降下）")]
        PantographFault,
        [HelpDescription("受电弓降弓故障")]
        PantographDownFault,
        [HelpDescription("受电弓升弓故障")]
        PantographUpFault,
        [HelpDescription("受电弓切除")]
        PantographCut,
        [HelpDescription("受电弓状态未知")]
        PantographStateUnkown,
    }
}