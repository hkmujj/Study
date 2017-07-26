namespace Motor.ATP.Infrasturcture.Interface.UserAction
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDriverPopupView
    {
        /// <summary>
        /// 更新状态
        /// </summary>
        void UpdateState(IDriverInterface lastInterface, IDriverInterface currentInterface);

        /// <summary>
        /// 响应
        /// </summary>
        /// <param name="driverInterface"></param>
        void ResponseAction(IDriverInterface driverInterface);

        /// <summary>
        /// 结束响应
        /// </summary>
        /// <param name="driverInterface"></param>
        void FinishResponseAction(IDriverInterface driverInterface);

    }
}