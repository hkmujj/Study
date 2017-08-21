namespace Tram.CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// OBCU状态
    /// 0、无
    /// 1、激活
    /// 2、待机
    /// 3、旁路
    /// 4、故障
    /// </summary>
    public enum OBCUStatus
    {
        /// <summary>
        /// 无
        /// </summary>
        None,
        /// <summary>
        /// 激活
        /// </summary>
        Active,
        /// <summary>
        /// 待机
        /// </summary>
        Standby,
        /// <summary>
        /// 旁路
        /// </summary>
        Bypass,
        /// <summary>
        /// 故障
        /// </summary>
        Fault,
    }
}
