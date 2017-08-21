namespace CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// 客室门控制模式显示
    /// </summary>
    public enum DoorControlMode
    {
        /// <summary>
        /// 不许显示时
        /// </summary>
        Unkown,

        /// <summary>
        /// 自动开门/自动关门
        /// </summary>
        AOAC,

        /// <summary>
        /// 自动开门/人工关门
        /// </summary>
        AOMC,

        /// <summary>
        /// 人工开门/人工关门
        /// </summary>
        MOMC,
    }
}