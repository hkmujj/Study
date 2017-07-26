namespace Motor.ATP.Infrasturcture.Interface
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

        /// <summary>
        /// 最小
        /// </summary>
        Min = Unconnected,

        /// <summary>
        /// 正在连接
        /// </summary>
        Connecting = 1,

        /// <summary>
        /// 1个已连接
        /// </summary>
        Connected = 2,

        /// <summary>
        /// 连接失败
        /// </summary>
        ConnectFault = 3,

        /// <summary>
        /// 2个已连接
        /// </summary>
        Connected2 = 4,

        /// <summary>
        /// 最大
        /// </summary>
        Max = ConnectFault,
    }
}