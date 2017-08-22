using System.ComponentModel;

namespace LightRail.HMI.GZYGDC.Model.State
{
    /// <summary>
    /// 风速模式
    /// </summary>
    public enum WindSpeedMode
    {
        /// <summary>
        /// 无模式
        /// </summary>
        [Description("")]
        None,
        /// <summary>
        /// 强风
        /// </summary>
        [Description("强风")]
        Strong,
        /// <summary>
        /// 中风
        /// </summary>
        [Description("中风")]
        Middle,
        /// <summary>
        /// 弱风
        /// </summary>
        [Description("弱风")]
        Weak,
    }
}
