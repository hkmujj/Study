namespace Motor.ATP.Domain.Interface
{
    /// <summary>
    /// 机车信号 
    /// </summary>
    public interface ICabSignal : IATPPartial
    {
        /// <summary>
        /// 机车信号码
        /// </summary>
        CabSignalCode Code { get; }

        /// <summary>
        /// 是否有效
        /// </summary>
        bool IsEffective { get; }
    }

    /// <summary>
    /// 机车信号码
    /// </summary>
    public enum CabSignalCode
    {
        /// <summary>
        /// 保留, 未定义的
        /// </summary>
        Reserve = 0,

        L6,
        L5,
        L4,
        L3,
        L2,
        L,
        LU,
        LU2,
        U,
        U2,
        U2S,
        UU,
        UUS,
        HB,
        HU,
        H,
        /// <summary>
        /// 无码
        /// </summary>
        NC,


    }
}
