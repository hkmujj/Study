namespace CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// 当前运行等级
    /// </summary>
    public enum OperatingGrade
    {
        /// <summary>
        /// 不显示
        /// </summary>
        Normal,
        /// <summary>
        /// 联锁级
        /// </summary>
        IXL,
        /// <summary>
        /// 点式通讯
        /// </summary>
        ITC,
        /// <summary>
        /// 连续通信
        /// </summary>
        CBTC,
        /// <summary>
        /// RM驾驶模式
        /// </summary>
        CBI,
        /// <summary>
        /// 后备
        /// </summary>
        BM,

    }
}