namespace Motor.ATP.Infrasturcture.Interface.SpeedMonitoringSection
{
    /// <summary>
    /// 监视类型
    /// </summary>
    public enum SpeedMonitoringSectionType
    {
        /// <summary>
        /// 没有监视
        /// </summary>
        None = 0,

        /// <summary>
        /// ceiling speed monitor section
        /// </summary>
        CSM,

        /// <summary>
        /// target speed monitor section
        /// </summary>
        TSM,

        /// <summary>
        /// Relase speed monitoring 开口速度监控
        /// </summary>
        RSM,
    }
}