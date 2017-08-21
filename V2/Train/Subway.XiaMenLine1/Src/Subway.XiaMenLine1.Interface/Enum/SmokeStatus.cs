using Subway.XiaMenLine1.Interface.Attibutes;

namespace Subway.XiaMenLine1.Interface.Enum
{
    public enum SmokeStatus
    {
        Normal,
        Selct,
        Smoke,
        SmokeTemperature,
        SmokeFault,
        SmokeNormal,

        [HelpDescription("探测到烟雾或高温")]
        SurveySmokeOrHightTemp,
        [HelpDescription("探测控制器故障")]
        SurverControlFault,
        [HelpDescription("探测控制器屏蔽")]
        SurverControlShield,
        [HelpDescription("正常，未探测到烟温异常")]
        SurverControlNormal,
        [HelpDescription("探测控制器通讯故障")]
        SurverControlCommunicationFault,
        [HelpDescription("探测控制器状态未知")]
        SurverControlUnKnow,
    }
}