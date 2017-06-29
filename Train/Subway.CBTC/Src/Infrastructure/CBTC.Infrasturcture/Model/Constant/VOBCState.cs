namespace CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// VOBC状态
    /// </summary>
    public enum VOBCState
    {
        None,

        /// <summary>
        /// 正常
        /// </summary>
        Normal,

        /// <summary>
        /// 部分故障
        /// </summary>
        PartialFault,

        /// <summary>
        /// 完全故障
        /// </summary>
        CompleteFault
    }
}