namespace Motor.ATP.Infrasturcture.Interface
{
    /// <summary>
    /// 连接状态
    /// </summary>
    public interface IConnectState : ITrainInfoPartial, IVisibility
    {
        /// <summary>
        /// GSM 状态
        /// </summary>
        GSMRState GSMRState { get; }

        /// <summary>
        /// RBC 状态
        /// </summary>
        RBCConnectState RBCConnectState { get; }

        /// <summary>
        /// RBC ID
        /// </summary>
        string RBCID { get; }

        /// <summary>
        /// 
        /// </summary>
        string TelNumber { get; }
    }
}