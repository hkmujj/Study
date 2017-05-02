using System.ComponentModel;

namespace Subway.WuHanLine6.Models.States
{
    /// <summary>
    /// 高速断路器状态
    /// </summary>
    public enum HighSpeedState
    {
        /// <summary>
        /// 故障
        /// </summary>
        [Description("故障")]
        Fault,

        /// <summary>
        /// 合上
        /// </summary>
        [Description("合上")]
        Close,

        /// <summary>
        /// 断开
        /// </summary>
        [Description("Open")]
        Open,
    }
}