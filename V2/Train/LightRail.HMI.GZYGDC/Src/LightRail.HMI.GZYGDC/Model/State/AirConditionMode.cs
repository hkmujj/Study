using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LightRail.HMI.GZYGDC.Model.State
{
    /// <summary>
    /// 空调模式
    /// </summary>
    public enum AirConditionMode
    {
        /// <summary>
        /// 无模式
        /// </summary>
        [Description("")]
        None,
        /// <summary>
        /// 自动模式
        /// </summary>
        [Description("自动模式")]
        Auto,
        /// <summary>
        /// 通风模式
        /// </summary>
        [Description("通风模式")]
        Ventilation,
        /// <summary>
        /// 关闭预冷
        /// </summary>
        [Description("关闭预冷")]
        ClosePrecold,
        /// <summary>
        /// 测试模式
        /// </summary>
        [Description("测试模式")]
        Test,
        /// <summary>
        /// 火灾模式
        /// </summary>
        [Description("火灾模式")]
        Fire,
    }
}
