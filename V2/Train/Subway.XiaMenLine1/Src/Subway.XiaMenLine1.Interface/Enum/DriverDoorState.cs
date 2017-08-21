using Subway.XiaMenLine1.Interface.Attibutes;

namespace Subway.XiaMenLine1.Interface.Enum
{
    public enum DriverDoorState
    {
        Null,
        [HelpDescription("司机室侧门打开")]
        UnlockOrOpen,
        [HelpDescription("司机室侧门锁闭")]
        Locked,
        [HelpDescription("司机室侧门状态未知")]
        UnKnow
    }
}