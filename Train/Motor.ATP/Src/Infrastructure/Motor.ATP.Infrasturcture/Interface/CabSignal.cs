
namespace Motor.ATP.Infrasturcture.Interface
{
    /// <summary>
    /// 机车信号 
    /// </summary>
    public interface ICabSignal : IATPPartial, IVisibility
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
#pragma warning disable 1591

        /// <summary>
        /// 保留, 未定义的
        /// </summary>
        Unknown = 0,

        L6 = 1,
        L5 = 2,
        L4 = 3,
        L3 = 4,
        L2 = 5,
        L = 6,
        LU = 7,
        LU2 = 8,
        U = 9,
        U2 = 10,
        U2S = 11,
        UU = 12,
        UUS = 13,
        HB = 14,
        HU = 15,
        H = 16,

        Code25 = 17,

        Code27 = 18,

        /// <summary>
        /// 无码
        /// </summary>
        NC = 19,
    }
}
