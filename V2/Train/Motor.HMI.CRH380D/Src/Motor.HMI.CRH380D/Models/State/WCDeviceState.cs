namespace Motor.HMI.CRH380D.Models.State
{
    /// <summary>
    ///  卫生设备状态
    /// </summary>
    public enum WCDeviceState
    {
        /// <summary>
        /// 无人使用
        /// </summary>
        None,
        /// <summary>
        ///　正在使用
        /// </summary>
        Using,
        /// <summary>
        /// 故障
        /// </summary>
        Fault,
    }
}