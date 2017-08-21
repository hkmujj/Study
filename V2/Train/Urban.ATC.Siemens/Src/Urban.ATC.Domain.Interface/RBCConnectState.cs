namespace Motor.ATP.Domain.Interface
{
    /// <summary>
    /// RBC连接状态
    /// </summary>
    public enum RBCConnectState : int
    {
        /// <summary>
        /// 无效， 不显示
        /// </summary>
        Invalidate = -1,

        /// <summary>
        /// 未连接
        /// </summary>
        Unconnected = 0,

        Min = Unconnected,

        /// <summary>
        /// 正在连接
        /// </summary>
        Connecting = 1,

        /// <summary>
        /// 已连接
        /// </summary>
        Connected = 2,

        /// <summary>
        /// 连接失败
        /// </summary>
        ConnectFault = 3,

        Max = ConnectFault,
    }
}