using Subway.XiaMenLine1.Interface.Attibutes;

namespace Subway.XiaMenLine1.Interface.Enum
{
    public enum IntervalDoorState
    {
        Null,
        [HelpDescription("间隔门打开")]
        UnlockOrOpen,
        [HelpDescription("间隔门锁闭")]
        Locked,
        [HelpDescription("间隔门状态未知")]
        UnKown,
    }
}