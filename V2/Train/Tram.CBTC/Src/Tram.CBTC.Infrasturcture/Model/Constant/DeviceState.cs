namespace Tram.CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// 设备状态
    /// </summary>
    public enum DeviceState
    {
        /// <summary>
        /// 本端正常
        /// </summary>
       // CurrentNormal,
        /// <summary>
        /// 远端正常
        /// </summary>
       // RemoteNormal,
        /// <summary>
        /// 两端正常
        /// </summary>
        AllNormal,
        /// <summary>
        /// 本端故障
        /// </summary>
        CurrentFault,
        /// <summary>
        /// 远端故障
        /// </summary>
        RemoteFault,
        /// <summary>
        /// 两端故障
        /// </summary>
        AllFault,
       

    }
}
