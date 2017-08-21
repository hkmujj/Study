using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LightRail.HMI.GZYGDC.Model.State
{
    /// <summary>
    /// 工况
    /// </summary>
    public enum WorkCondition
    {
        /// <summary>
        /// 无模式
        /// </summary>
        [Description("无模式")]
        None,
        /// <summary>
        /// 牵引
        /// </summary>
        [Description("牵引")]
        Traction,
        /// <summary>
        /// 惰行
        /// </summary>
        [Description("惰行")]
        Lazy,
        /// <summary>
        /// 常用制动
        /// </summary>
        [Description("常用制动")]
        ConmonBrake,
        /// <summary>
        /// 自动紧急制动
        /// </summary>
        [Description("自动紧急制动")]
       AutoEmergencyBrake,
    }
}
