namespace CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// 门状态
    /// </summary>
    public enum DoorState
    {
        Unkown,

        /// <summary>
        /// 关闭
        /// </summary>
        Closed,

        /// <summary>
        /// 开
        /// </summary>
        Opend,

        /// <summary>
        /// 异常， 如ATC没有授权情况下车门打开
        /// </summary>
        Abnormal,

    }
}