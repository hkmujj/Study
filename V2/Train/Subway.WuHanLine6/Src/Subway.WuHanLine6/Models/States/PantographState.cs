using System.ComponentModel;

namespace Subway.WuHanLine6.Models.States
{
    /// <summary>
    /// 受电弓状态
    /// </summary>
    public enum PantographState
    {
        /// <summary>
        /// 未知
        /// </summary>
        UnKnown,

        /// <summary>
        /// 升弓到位
        /// </summary>
        [Description("升弓到位")]
        Up,

        /// <summary>
        /// 降弓到位
        /// </summary>
        [Description("降弓到位")]
        Down,

        /// <summary>
        /// 受电弓动作中
        /// </summary>
        [Description("受电弓动作中")]
        Motion,

        /// <summary>
        /// 通信异常
        /// </summary>
        [Description("")]
        UnUsual,
    }
}