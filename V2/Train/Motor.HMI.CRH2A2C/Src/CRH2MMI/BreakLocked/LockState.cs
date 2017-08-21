namespace CRH2MMI.BreakLocked
{
    /// <summary>
    /// 抱死状态
    /// </summary>
    internal enum LockState
    {
        /// <summary>
        /// 正常
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
    }
}
