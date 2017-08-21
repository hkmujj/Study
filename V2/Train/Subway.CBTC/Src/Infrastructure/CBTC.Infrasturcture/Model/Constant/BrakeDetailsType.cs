namespace CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// 超速报警及输出紧急制动
    /// </summary>
    public enum BrakeDetailsType
    {
        /// <summary>
        /// 不需显示时
        /// </summary>
        Normal,
        /// <summary>
        /// 超速报警时
        /// </summary>
        OverSpeed,
        /// <summary>
        /// 实际速度达到紧急制动出发速度时
        /// </summary>
        EnmergencyBrake,
        
    }
    
}
