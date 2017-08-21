namespace LightRail.HMI.SZLHLF.Model.EventModel
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
        /// 轻微
        /// </summary>
        Light = 1,
        /// <summary>
        /// 中等
        /// </summary>
        Medium = 2,
        /// <summary>
        /// 严重
        /// </summary>
        Grave = 3,
        /// <summary>
        /// 提示
        /// </summary>
        Prompt = 4,
    }
}
