using System.ComponentModel;

namespace Subway.ShenZhenLine7.Interfaces
{
    /// <summary>
    /// 报站模式
    /// </summary>
    public enum StationModel
    {
        /// <summary>
        /// 自动
        /// </summary>
        [Description("自动")]
        Auto,
        /// <summary>
        /// 手动
        /// </summary>
        [Description("手动")]
        Manual,
        /// <summary>
        /// 库内
        /// </summary>
        [Description("库内")]
        Lib
    }

    /// <summary>
    /// 控制模式
    /// </summary>
    public enum ControlModel
    {
        /// <summary>
        /// 自动控制
        /// </summary>
        [Description("自动控制")]
        Auto,
    }

    /// <summary>
    /// 信号模式
    /// </summary>
    public enum SignalModel
    {
        /// <summary>
        /// ATO
        /// </summary>
        [Description("ATO")]
        ATO,
    }

    /// <summary>
    /// 工况
    /// </summary>
    public enum WorkState
    {
        /// <summary>
        /// 紧急制动
        /// </summary>
        [Description("紧急制动")]
        Emergency
    }

}