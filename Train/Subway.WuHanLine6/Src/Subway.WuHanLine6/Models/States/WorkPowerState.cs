namespace Subway.WuHanLine6.Models.States
{
    /// <summary>
    /// 车间电源
    /// </summary>
    public enum WorkPowerState
    {
        /// <summary>
        /// 连接且供电
        /// </summary>
        ConnectPower,

        /// <summary>
        /// 连接未供电
        /// </summary>
        ConnectUnPower,

        /// <summary>
        /// 未连接
        /// </summary>
        UnConnect,

        /// <summary>
        /// 故障
        /// </summary>
        Fault
    }
}