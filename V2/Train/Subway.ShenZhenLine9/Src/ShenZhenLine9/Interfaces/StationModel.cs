using System.ComponentModel;

namespace Subway.ShenZhenLine9.Interfaces
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

        /// <summary>
        /// 手动控制
        /// </summary>
        [Description("人工控制")]
        Manual,
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
        /// <summary>
        /// 
        /// </summary>
        [Description("ATP")]
        ATP,
        /// <summary>
        /// 
        /// </summary>
        [Description("RM")]
        RM,
        /// <summary>
        /// 
        /// </summary>
        [Description("ATC")]
        ATC,
        /// <summary>
        /// 
        /// </summary>
        [Description("OFF")]
        OFF,
        /// <summary>
        /// 
        /// </summary>
        [Description("ATB")]
        ATB,
        /// <summary>
        /// 
        /// </summary>
        [Description("RMR")]
        RMR,
        /// <summary>
        /// 
        /// </summary>
        [Description("RMF")]
        RMF,
        /// <summary>
        /// 
        /// </summary>
        [Description("MSHUNT")]
        MSHUNT
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
        Emergency,
        /// <summary>
        /// 牵引
        /// </summary>
        [Description("牵引")]
        Traction,
        /// <summary>
        /// 常用制动
        /// </summary>
        [Description("常用制动")]
        Brake,
        /// <summary>
        /// 停放制动
        /// </summary>
        [Description("停放制动")]
        ParkingBrake,
        /// <summary>
        /// 快速制动
        /// </summary>
        [Description("快速制动")]
        FastBrake,
        /// <summary>
        /// 保持制动
        /// </summary>
        [Description("保持制动")]
        KeepBrake
    }

}