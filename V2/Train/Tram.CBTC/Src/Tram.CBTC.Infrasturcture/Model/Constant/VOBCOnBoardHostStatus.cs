namespace Tram.CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// VOBC车载主机状态
    /// 0、无
    /// 1、正常
    /// 2、部分故障
    /// 3、完全故障
    /// </summary>
    public enum VOBCOnBoardHostStatus
    {
        /// <summary>
        /// 无
        /// </summary>
        None,
        /// <summary>
        /// 正常
        /// </summary>
        Normal,
        /// <summary>
        /// 部分故障
        /// </summary>
        PartFault,
        /// <summary>
        /// 完全故障
        /// </summary>
        AllFault
    }
}
