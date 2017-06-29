namespace CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// 车辆实时工况
    /// </summary>
    public enum RealTimeWokeStatus
    {
        None,

        /// <summary>
        /// 牵引工况
        /// </summary>
        Traction,

        /// <summary>
        /// 紧急工况
        /// </summary>
        EB,

        /// <summary>
        /// 常用制动工况
        /// </summary>
        CommonBrake,

        /// <summary>
        /// 快速制动工况
        /// </summary>
        FastBrake,

        /// <summary>
        /// 停放制动工况
        /// </summary>
        ParkBrake,

        /// <summary>
        /// 保持制动
        /// </summary>
        KeepBrake,

        /// <summary>
        /// 惰行工况
        /// </summary>
        Coast

    };
}