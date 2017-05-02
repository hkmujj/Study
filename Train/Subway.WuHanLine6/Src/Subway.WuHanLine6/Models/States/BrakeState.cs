namespace Subway.WuHanLine6.Models.States
{
    /// <summary>
    /// 制动状态
    /// </summary>
    public enum BrakeState
    {
        /// <summary>
        /// 停放制动施加
        /// </summary>
        Parking,

        /// <summary>
        /// 制动切除
        /// </summary>
        Cut,

        /// <summary>
        /// 制动自检激活
        /// </summary>
        AutoCheck,

        /// <summary>
        /// 制动故障
        /// </summary>
        Fault,

        /// <summary>
        /// 制动警告
        /// </summary>
        Warn,

        /// <summary>
        /// 常用制动施加
        /// </summary>
        Infliction,

        /// <summary>
        /// 常用制动缓解
        /// </summary>
        Remission,
    }
}