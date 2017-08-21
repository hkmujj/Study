namespace Tram.CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// GPS工作状态
    /// 0、无
    /// 1、工作状态正常
    /// 2、长时间未刷新
    /// </summary>
    public enum GPSWorkStatus
    {
        /// <summary>
        /// 无
        /// </summary>
        None,
        /// <summary>
        /// 工作状态正常
        /// </summary>
        Nomal,
        /// <summary>
        /// 长时间未刷新
        /// </summary>
        NotRefresh
    }
}
