using Subway.XiaMenLine1.Interface.Attibutes;

namespace Subway.XiaMenLine1.Interface.Enum
{
    public enum AssistPowerStatus
    {
        [HelpDescription("辅助逆变器")]
        AssistOrganDisconnect,
        [HelpDescription("辅助逆变器")]
        AssistOrganFault,
        [HelpDescription("辅助逆变器")]
        AssistOrganRunning,
        [HelpDescription("辅助逆变器")]
        AssistOrganCommunicationFault,
        [HelpDescription("辅助逆变器")]
        AssistOrganUnKonow,
        [HelpDescription("辅助逆变器切除")]
        AssistOrganCut,
        [HelpDescription("充电机故障")]
        AssistChargeFault,
        [HelpDescription("充电机运行，无故障")]
        AssistChargeRunning,
        [HelpDescription("充电机断开，无故障")]
        AssistChargeDisconnect,
        [HelpDescription("充电机通讯故障")]
        AssistChargeCommunicationFault,
        [HelpDescription("充电机未知")]
        AssistChargeUnKonow,
    }
}