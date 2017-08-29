namespace Motor.HMI.CRH380D.Models.State
{
    /// <summary>
    /// 故障等级
    /// </summary>
    public enum EventLevel
    {
        /// <summary>
        /// 默认
        /// </summary>
        Normal,
        /// <summary>
        /// A类警报
        /// </summary>
        AWarring = 1,
        /// <summary>
        /// B类警报
        /// </summary>
        BWarring = 2,
        /// <summary>
        /// 故障
        /// </summary>
        Fault = 3,
        /// <summary>
        /// 提示
        /// </summary>
        Prompt = 4,
    }
}
