namespace Tram.CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// 空转指示
    /// 0、无 
    /// 1、制动力不足 
    /// 2、正常空转或打滑
    /// 3、严重空转或打滑
    /// 3、打滑
    /// 4、制动力正常
    /// </summary>
    public enum RactingInstructions
    {
        /// <summary>
        /// 无
        /// </summary>
        None,
        /// <summary>
        /// 制动力不足
        /// </summary>
        BrakeLow,
        /// <summary>
        /// 正常空转或打滑
        /// </summary>
        NormalRactingOrSlip,
        /// <summary>
        /// 严重空转或打滑
        /// </summary>
        GraveRactingOrSlip,
        /// <summary>
        /// 打滑
        /// </summary>
        Slip,
        /// <summary>
        /// 制动力正常
        /// </summary>
        BrakeNomal
    }
}
