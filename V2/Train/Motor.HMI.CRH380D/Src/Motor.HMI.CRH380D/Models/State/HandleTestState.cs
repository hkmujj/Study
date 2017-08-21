namespace Motor.HMI.CRH380D.Models.State
{
    /// <summary>
    /// 手柄测试状态
    /// </summary>
    public enum HandleTestState
    {
       /// <summary>
        /// 手柄测试失败
       /// </summary>
        HandleTestFailed,
       /// <summary>
        /// 手柄测试成功
       /// </summary>
        HandleTestSuccess,
        /// <summary>
        /// 手柄测试未开始
        /// </summary>
        HandleTestNotStart,
    }
}