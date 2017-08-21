namespace Tram.CBTC.Infrasturcture.Model.Constant
{

    /// <summary>
    /// 系统状态
    /// 0、无
    /// 1、系统正常
    /// 2、系统完全故障
    /// 3、系统部分故障
    /// </summary>
    public enum SystemStatus
    {
        /// <summary>
        /// 无
        /// </summary>
        None,
        /// <summary>
        /// 系统正常
        /// </summary>
        Normal,
        /// <summary>
        /// 系统完全故障
        /// </summary>
        AllFault,
        /// <summary>
        /// 系统部分故障
        /// </summary>
        PartFault
    }
}
