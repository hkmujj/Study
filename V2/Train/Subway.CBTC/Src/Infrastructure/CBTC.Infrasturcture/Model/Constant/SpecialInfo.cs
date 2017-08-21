namespace CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// 特殊信息
    /// </summary>
    public enum SpecialInfo
    {
        /// <summary>
        /// 没有特殊信息
        /// </summary>
        None,
        /// <summary>
        /// 列车位于转换轨
        /// </summary>
        TransferRail,
        /// <summary>
        /// 列车处于RM模式且未定位
        /// </summary>
        TrainUnPosition,
        /// <summary>
        /// 列车接近需要跳站车站
        /// </summary>
        SkipingStation,
    }
}