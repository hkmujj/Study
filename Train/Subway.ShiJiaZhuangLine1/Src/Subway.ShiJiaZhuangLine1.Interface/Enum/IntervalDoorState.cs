using Subway.ShiJiaZhuangLine1.Interface.Attibutes;

namespace Subway.ShiJiaZhuangLine1.Interface.Enum
{
    public enum IntervalDoorState
    {
        UnKnow,
        [HelpDescription("正常")]
        Normal,
        [HelpDescription("间隔门未锁/打开(闪烁)")]
        UnlockOrOpen,
        [HelpDescription("间隔门锁闭")]
        Locked,
    }
}