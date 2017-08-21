namespace Tram.CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// 工况
    /// 0、无
    /// 1、牵引工况
    /// 2、紧急工况
    /// 3、常用制动工况
    /// 4、快速制动工况
    /// 5、停放制动工况
    /// 6、保持制动
    /// 7、惰行工况
    /// </summary>
    public enum WorkState
    {
        /// <summary>
        /// 无
        /// </summary>
        None,
        /// <summary>
        /// 牵引工况
        /// </summary>
        Traction,
        /// <summary>
        /// 紧急工况
        /// </summary>
        Emergent,
        /// <summary>
        /// 常用制动工况
        /// </summary>
        Brake,
        /// <summary>
        /// 快速制动工况
        /// </summary>
        FastBrake,
        /// <summary>
        /// 停放制动工况
        /// </summary>
        ParkingBrake,
        /// <summary>
        /// 保持制动
        /// </summary>
        KeepBrake,
        /// <summary>
        /// 惰行工况
        /// </summary>
        Coasting,

    }
}
