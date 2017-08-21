using System.ComponentModel;

namespace LightRail.HMI.GZYGDC.Model.State
{
    /// <summary>
    /// 列车方向
    /// </summary>
    public enum TrainDirection
    {
        /// <summary>
        /// 向前
        /// </summary>
        [Description("向前")]
        Forward,
        /// <summary>
        /// 无方向
        /// </summary>
        [Description("无方向")]
        None,
        /// <summary>
        /// 向后
        /// </summary>
        [Description("向后")]
        Back,
    }
}
