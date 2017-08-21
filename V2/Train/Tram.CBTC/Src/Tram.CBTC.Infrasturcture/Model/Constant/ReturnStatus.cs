namespace Tram.CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// 折返信息
    /// 0、无折返
    /// 1、本站折返
    /// 2、下一站折返
    /// </summary>
    public enum ReturnInfo
    {
        /// <summary>
        /// 无折返
        /// </summary>
        None,
        /// <summary>
        /// 本站折返
        /// </summary>
        CurrentStationReturn,
        /// <summary>
        /// 下一站折返
        /// </summary>
        NextStationReurn
    }
}
