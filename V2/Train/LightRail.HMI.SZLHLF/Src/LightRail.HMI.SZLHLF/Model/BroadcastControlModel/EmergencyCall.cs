namespace LightRail.HMI.SZLHLF.Model.BroadcastControlModel
{
    /// <summary>
    /// 紧急电话
    /// </summary>
    public enum EmergencyCall
    {
        /// <summary>
        /// 通话中
        /// </summary>
        Calling,

        /// <summary>
        /// 通话请求
        /// </summary>
        CallRequest,

        /// <summary>
        /// 严重故障
        /// </summary>
        SeriousTrouble,

        /// <summary>
        /// 通话结束
        /// </summary>
        CallOver,
    }
}
