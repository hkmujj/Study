namespace Motor.HMI.CRH380D.Models.State
{
    /// <summary>
    /// 事件模式   故障/警报
    /// </summary>
    public enum EventPageState
    {
        /// <summary>
        /// 默认
        /// </summary>
        Normal,
        /// <summary>
        /// 故障
        /// </summary>
        Fault = 1,
        /// <summary>
        /// 警报
        /// </summary>
        Warring = 2,
    }
}
