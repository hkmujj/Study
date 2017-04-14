namespace MMI.Communacation.Control.ConcreateProtocol
{
    public enum ApplicationRestartCommand
    {
        /// <summary>
        /// 不做任何操作
        /// </summary>
        Nomal = 0,
        /// <summary>
        /// 重启计算机
        /// </summary>
        ReBoot = 1,
        /// <summary>
        /// 关闭计算机
        /// </summary>
        Shutdown = 2,
        /// <summary>
        /// 注销用户
        /// </summary>
        LogOff = 3,
        /// <summary>
        /// 重启程序
        /// </summary>
        ReStart = 4,
        /// <summary>
        /// 关闭程序
        /// </summary>
        Close = 5,

    }
}