namespace Subway.WuHanLine6.Models.States
{
    /// <summary>
    /// 空调
    /// </summary>
    public enum AirConditionState
    {
        /// <summary>
        /// 空调故障
        /// </summary>
        Fault,

        /// <summary>
        /// 空调警告
        /// </summary>
        Warn,

        /// <summary>
        /// 紧急通风
        /// </summary>
        EmergencyFan,

        /// <summary>
        /// 通风
        /// </summary>
        Fan,

        /// <summary>
        /// 限制制冷
        /// </summary>
        Limit,

        /// <summary>
        /// 空调运行
        /// </summary>
        Run,

        /// <summary>
        /// 空调断开
        /// </summary>
        Off
    }
}