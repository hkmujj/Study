using Subway.ShiJiaZhuangLine1.Interface.Attibutes;

namespace Subway.ShiJiaZhuangLine1.Interface.Enum
{
    public enum DriverDoorState
    {
        UnKnow,
        [HelpDescription("司机室侧门未锁/打开(闪烁)")]
        UnlockOrOpen,
        [HelpDescription("司机室侧门锁闭")]
        Locked,
    }
}