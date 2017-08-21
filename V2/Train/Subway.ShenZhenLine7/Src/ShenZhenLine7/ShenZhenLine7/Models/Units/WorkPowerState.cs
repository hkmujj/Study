using System.ComponentModel;

namespace Subway.ShenZhenLine7.Models.Units
{
    /// <summary>
    /// 车间电源状态
    /// </summary>
    public enum WorkPowerState
    {
        /// <summary>
        /// 连接且供电
        /// </summary>
        [Description("车间电源连接且供电")]
        ConnectPower,
        /// <summary>
        /// 连接未供电
        /// </summary>
        [Description("车间电源连接，未供电")]
        Connect,
        /// <summary>
        /// 未连接
        /// </summary>
        [Description("车间电源未连接")]
        UnConnect,
    }

    /// <summary>
    /// 受电弓
    /// </summary>
    public enum PantographState
    {
        /// <summary>
        /// 升起故障
        /// </summary>
        [Description("受电弓升起，故障")]
        UpFault,
        /// <summary>
        /// 降下故障
        /// </summary>
        [Description("受电弓降下，故障")]
        DownFault,
        /// <summary>
        /// 升起
        /// </summary>
        [Description("受电弓升起")]
        Up,
        /// <summary>
        /// 降下
        /// </summary>
        [Description("受电弓降下")]
        Down,
    }

    /// <summary>
    /// 高速断路器状态
    /// </summary>
    public enum HighSpeedState
    {
        /// <summary>
        /// 闭合故障
        /// </summary>
        [Description("HSCB闭合但故障")]
        CloseFault,
        /// <summary>
        /// 断开故障
        /// </summary>
        [Description("HSCB断开但故障")]
        OpedFault,
        /// <summary>
        /// 断开
        /// </summary>
        [Description("HSCB断开无故障")]
        Open,
        /// <summary>
        /// 闭合
        /// </summary>
        [Description("HSCB闭合无故障")]
        Close,
    }
}