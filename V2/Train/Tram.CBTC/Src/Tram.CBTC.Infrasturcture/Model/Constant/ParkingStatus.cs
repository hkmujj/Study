namespace Tram.CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// 停车状态
    /// 0、无效
    /// 1、在车站且停准
    /// 2、在车站没有停准 
    /// 3、不在车站 
    /// 4、无效
    /// </summary>
    public enum ParkingStatus
    {
        /// <summary>
        /// 无效
        /// </summary>
        Invalid,
        /// <summary>
        /// 在车站且停准
        /// </summary>
        InStationQuasiStop,
        /// <summary>
        /// 在车站没有停准
        /// </summary>
        InStationNotStoped,
        /// <summary>
        /// 不在车站
        /// </summary>
        OutStation,

    }
}
