namespace CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// 发车状态
    /// </summary>
    public enum ATCRequestState
    {
        None,

        /// <summary>
        /// 请求车门关闭 
        /// </summary>
        RequestClose,

        /// <summary>
        /// ATC请求列车离站
        /// </summary>
        ATCRequestLeave,

        /// <summary>
        ///  关闭门指令
        /// </summary>
        CloseDoorOrder
    }
}