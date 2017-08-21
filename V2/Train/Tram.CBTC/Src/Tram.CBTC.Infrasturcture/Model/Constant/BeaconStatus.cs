namespace Tram.CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// 信标状态
    /// 0、无
    /// 1、正常
    /// 2、信标丢失或漏读
    /// </summary>
    public enum BeaconStatus
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
        /// 信标丢失或漏读
        /// </summary>
        BeaconMissingOrMissedRead
    }
}
