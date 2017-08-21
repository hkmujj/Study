namespace Motor.ATP.Domain.Interface.SpeedMonitoringSection
{
    /// <summary>
    /// 减速类型
    /// </summary>
    public enum SpeedChangeType
    {
        None,

        /// <summary>
        /// 加速
        /// </summary>
        Acceleration,

        /// <summary>
        /// 减速
        /// </summary>
        Decelerate,

        /// <summary>
        /// 减速到0
        /// </summary>
        DecelerateToZero,
    }
}
