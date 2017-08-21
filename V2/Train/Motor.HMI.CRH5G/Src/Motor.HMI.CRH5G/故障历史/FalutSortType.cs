using System.ComponentModel;

namespace Motor.HMI.CRH5G.故障历史
{
    internal enum FalutSortType
    {
        None,
        [Description("排序按时间")]
        Time,
        [Description("排序按车号")]
        CarNo,
        [Description("排序按设备")]
        Dev,
    }
}