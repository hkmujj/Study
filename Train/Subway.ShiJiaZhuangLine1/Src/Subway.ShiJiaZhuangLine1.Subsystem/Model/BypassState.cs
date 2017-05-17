using Subway.ShiJiaZhuangLine1.Interface.Attibutes;

namespace Subway.ShiJiaZhuangLine1.Subsystem.Model
{
    public enum BypassState
    {
        [HelpDescription("分")]
        SwitchOff,
        [HelpDescription("合")]
        SwitchOn,
        [HelpDescription("故障")]
        Fault,
        [HelpDescription("未知")]
        Unknown,
    }
}