namespace Tram.CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// 0、无
    /// 1、车载与调度中心dms系统通信良好
    /// 2、车载与调度中心dms系统通信中断
    /// </summary>
    public enum VehicleCommunicationStatus
    {
        /// <summary>
        /// 无
        /// </summary>
        None,
        /// <summary>
        /// 车载与调度中心dms系统通信良好
        /// </summary>
        Good,
        /// <summary>
        /// 车载与调度中心dms系统通信中断
        /// </summary>
        Interrupt
    }
}
