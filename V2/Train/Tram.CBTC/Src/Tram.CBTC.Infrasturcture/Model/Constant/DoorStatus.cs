namespace Tram.CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// 车门状态
    /// 0、无效 
    /// 1、关闭 
    /// 2、开启 
    /// 3、异常打开
    /// 4、允许释放车门
    /// 5、不再监控车门
    /// 6、状态未知
    /// </summary>
    public enum DoorStatus
    {
        /// <summary>
        /// 无效
        /// </summary>
        Invalid,
        /// <summary>
        /// 关闭
        /// </summary>
        Closed,
        /// <summary>
        /// 开启
        /// </summary>
        Open,
        /// <summary>
        /// 异常打开
        /// </summary>
        AbnormalOpen,
        /// <summary>
        /// 允许释放车门
        /// </summary>
        AllowReleseDoor,
        /// <summary>
        /// 不再监控车门
        /// </summary>
        NotMonitorDoor,
        /// <summary>
        /// 状态未知
        /// </summary>
        Unkown,
    }
}
