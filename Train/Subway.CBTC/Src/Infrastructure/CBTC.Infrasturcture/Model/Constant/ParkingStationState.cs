namespace CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// 列车停站信息
    /// </summary>
    public enum ParkingStationState
    {
        /// <summary>
        /// 无效
        /// </summary>
        Unkown,

        /// <summary>
        ///  在车站且对准
        /// </summary>
        Aligned,

        /// <summary>
        /// 在车站没对准
        /// </summary>
        NONAligned,

        /// <summary>
        /// 不在车站
        /// </summary>
        NoAtStation,

        /// <summary>
        /// 无效
        /// </summary>
        Invalid,
    }
}