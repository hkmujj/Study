using System;

namespace Motor.HMI.CRH1A.Alarm.Fault
{
    /// <summary>
    /// 故障状态
    /// </summary>
    [Flags]
    public enum FaultState
    {
        /// <summary>
        /// 新产生
        /// </summary>
        New = 0,

        /// <summary>
        /// 已确认
        /// </summary>
        Sure = 2,

        /// <summary>
        /// 已解决
        /// </summary>
        Fixed = 4,

        /// <summary>
        /// 已确认并已解决
        /// </summary>
        SureAndFixed = Sure | Fixed,
    }
}
