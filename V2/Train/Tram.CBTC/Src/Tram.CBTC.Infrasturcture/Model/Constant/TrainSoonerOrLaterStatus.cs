namespace Tram.CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// 列车早晚点状态
    /// 0、无
    /// 1、早点时分
    /// 2、晚点时分
    /// 3、正点
    /// </summary>
    public enum TrainSoonerOrLaterStatus
    {
        /// <summary>
        /// 无
        /// </summary>
        None,
        /// <summary>
        /// 早点
        /// </summary>
        TrainBreakfast,
        /// <summary>
        /// 晚点
        /// </summary>
        TrainLater,
        /// <summary>
        /// 正点
        /// </summary>
        TrainScheduled

    }
}
