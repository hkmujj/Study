using Subway.ShiJiaZhuangLine1.Interface.Attibutes;

namespace Subway.ShiJiaZhuangLine1.Interface.Enum
{
    /// <summary>
    /// 疏散门
    /// </summary>
    public enum EvacuateDoorState
    {
        UnKnow,
        [HelpDescription("正常")]
        Normal,
        [HelpDescription("疏散门未锁/打开(闪烁)")]
        UnlockOrOpen,
        [HelpDescription("疏散门锁闭")]
        Locked,
    }
}