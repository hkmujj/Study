using Subway.ShiJiaZhuangLine1.Interface.Attibutes;

namespace Subway.ShiJiaZhuangLine1.Interface.Enum
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
        [HelpDescription("正常，未探测到烟温异常")]
        SurverControlNormal,
        [HelpDescription("探测控制器通讯故障")]
        SurverControlCommunicationFault,
    }
}