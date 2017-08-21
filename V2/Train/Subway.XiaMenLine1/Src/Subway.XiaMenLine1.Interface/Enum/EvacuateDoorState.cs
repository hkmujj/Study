using Subway.XiaMenLine1.Interface.Attibutes;

namespace Subway.XiaMenLine1.Interface.Enum
{
    /// <summary>
    /// 疏散门
    /// </summary>
    public enum EvacuateDoorState
    {
        Null,
        [HelpDescription("疏散门未锁/打开")]
        UnlockOrOpen,
        [HelpDescription("疏散门锁闭")]
        Locked,
        [HelpDescription("未知")]
        Unknow,
    }
}