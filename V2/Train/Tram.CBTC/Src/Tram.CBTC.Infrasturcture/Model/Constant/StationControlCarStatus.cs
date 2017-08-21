namespace Tram.CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// 车站控车状态
    /// 0、正常运行
    /// 1、扣车 
    /// 2、跳停
    /// </summary>
    public enum StationControlCarStatus
    {
        /// <summary>
        /// 正常运行
        /// </summary>
        Normal,
        /// <summary>
        /// 扣车
        /// </summary>
        Deduction,
        /// <summary>
        /// 跳停
        /// </summary>
        JumStop
    }
}
