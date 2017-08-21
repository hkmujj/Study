namespace Motor.HMI.CRH1A.Alarm.Fault
{
    /// <summary>
    /// 故障类型
    /// </summary>
    public enum FaultType
    {
        /// <summary>
        /// 操作错误
        /// </summary>
        [FaultShow("Black", FaultShowStyle.PopViewAndShowFeet)]
        OperError = 1,

        /// <summary>
        /// 乘客警报事件
        /// </summary>
        [FaultShow(FaultShowStyle.Unkown)]
        Passenger,

        /// <summary>
        /// A 类故障
        /// </summary>
        [FaultShow("Red", FaultShowStyle.PopViewAndShowFeet)]
        A = 3,

        /// <summary>
        /// B 类故障
        /// </summary>
        [FaultShow("Yellow", FaultShowStyle.ShowFeet)]
        B = 4,

        /// <summary>
        /// 设备故障, 由TCMS探测到的故障
        /// </summary>
        [FaultShow("Yellow", FaultShowStyle.ShowFeet)]
        E = 5,

        /// <summary>
        /// 手工输入的故障
        /// </summary>
        [FaultShow(FaultShowStyle.Unkown)]
        Manaul = 6,

        /// <summary>
        /// 事件
        /// </summary>
        [FaultShow(FaultShowStyle.Unkown)]
        Event = 7,

        /// <summary>
        /// 警告性信息, 弹框, 不可操作
        /// </summary>
        [FaultShow(FaultShowStyle.FullScreen)]
        Warning = 8,

        /// <summary>
        /// 信息性信息,弹框, 不可操作
        /// </summary>
        [FaultShow(FaultShowStyle.FullScreen)]
        Info = 9,

        /// <summary>
        /// 用户自定义, 死机
        /// </summary>
        [FaultShow(FaultShowStyle.FullScreen)]
        UserDefinedSystemHalted = 10,

        /// <summary>
        /// 用户自定义, 休眠
        /// </summary>
        [FaultShow(FaultShowStyle.FullScreen)]
        UserDefinedSystemSleep = 11,

    }
}
