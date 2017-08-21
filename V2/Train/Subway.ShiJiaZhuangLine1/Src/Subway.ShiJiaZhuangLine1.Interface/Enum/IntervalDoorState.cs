using Subway.ShiJiaZhuangLine1.Interface.Attibutes;

namespace Subway.ShiJiaZhuangLine1.Interface.Enum
{
    public enum IntervalDoorState
    {
        UnKnow,
        [HelpDescription("正常")]
        Normal,
        [HelpDescription("通道门紧急解锁激活(闪烁)")]
        UnlockOrOpen,
        [HelpDescription("通道门紧急解锁未激活")]
        Locked,
    }
}