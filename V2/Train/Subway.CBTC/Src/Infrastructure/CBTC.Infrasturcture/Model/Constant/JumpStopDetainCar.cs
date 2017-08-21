namespace CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// 跳停扣车
    /// </summary>
    public enum JumpStopDetainCar
    {
        /// <summary>
        /// 不需显示时
        /// </summary>
        Normal,
        /// <summary>
        /// ATO正在执行扣车时
        /// </summary>
        DetainCar,
        /// <summary>
        /// ATO正在执行跳停时
        /// </summary>
        JumpStop,
        
    }
}