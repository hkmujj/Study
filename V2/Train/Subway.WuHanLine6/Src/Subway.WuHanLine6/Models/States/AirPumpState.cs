namespace Subway.WuHanLine6.Models.States
{
    /// <summary>
    /// 空压机
    /// </summary>
    public enum AirPumpState
    {
        /// <summary>
        /// 切除
        /// </summary>
        Cut,

        /// <summary>
        /// 故障
        /// </summary>
        Fault,

        /// <summary>
        /// 警告
        /// </summary>
        Warn,

        /// <summary>
        /// 运行
        /// </summary>
        Run,

        /// <summary>
        /// 断开
        /// </summary>
        Off
    }
}