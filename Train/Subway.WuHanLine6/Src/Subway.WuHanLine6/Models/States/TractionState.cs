namespace Subway.WuHanLine6.Models.States
{
    /// <summary>
    /// 牵引状态
    /// </summary>
    public enum TractionState
    {
        /// <summary>
        /// 默认
        /// </summary>
        Normal,

        /// <summary>
        /// 切除
        /// </summary>
        Cut,

        /// <summary>
        /// 故障
        /// </summary>
        Fault,

        /// <summary>
        /// 警告
        /// </summary>
        Warn,

        /// <summary>
        /// 运行
        /// </summary>
        Run,

        /// <summary>
        /// 断开
        /// </summary>
        Close
    }
}