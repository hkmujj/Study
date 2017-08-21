namespace Tram.CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// 信号制动状态
    /// 0、无 
    /// 1、请求制动 
    /// 2、制动预警
    /// 3、常用制动
    /// 4、快速制动
    /// 5、停放制动
    /// 6、紧急制动
    /// 7、保持制动 
    /// 8、切除牵引
    /// 9、允许缓解
    /// </summary>
    public enum SignalBrakeStatus
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,
        /// <summary>
        /// 请求制动
        /// </summary>
        RequestBrake,
        /// <summary>
        /// 制动预警
        /// </summary>
        BrakeWarning,
        /// <summary>
        /// 常用制动
        /// </summary>
        Brake,
        /// <summary>
        /// 快速制动
        /// </summary>
        FastBrake,
        /// <summary>
        /// 停放制动
        /// </summary>
        ParkingBrake,
        /// <summary>
        /// 紧急制动
        /// </summary>
        EmergentBrake,
        /// <summary>
        /// 保持制动
        /// </summary>
        KeepBrake,
        /// <summary>
        ///  切除牵引
        /// </summary>
        CutTow,
        /// <summary>
        /// 允许缓解
        /// </summary>
        AllowRemission,
    }
}
