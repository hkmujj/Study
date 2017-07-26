namespace Motor.ATP.Infrasturcture.Interface
{
    /// <summary>
    /// GSM-R状态
    /// </summary>
    public enum GSMRState : int
    {
        /// <summary>
        /// 无效， 不显示
        /// </summary>
        Invalidate = -1,

        /// <summary>
        /// 无连接
        /// </summary>
        None = 0,

        /// <summary>
        /// 最小
        /// </summary>
        Min = None,

        /// <summary>
        /// 有一个连接
        /// </summary>
        HasOne = 1,

        /// <summary>
        /// 有两个连接
        /// </summary>
        HasTwo = 2,

        /// <summary>
        /// 有三个连接
        /// </summary>
        HasThree = 3,

        /// <summary>
        /// 最大
        /// </summary>
        Max = HasThree,
    }
}