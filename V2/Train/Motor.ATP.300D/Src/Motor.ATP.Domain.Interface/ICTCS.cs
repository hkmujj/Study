namespace Motor.ATP.Domain.Interface
{
    /// <summary>
    /// CTCS 等级
    /// </summary>
    public interface ICTCS : IATPPartial
    {
        CTCSType Type { get; } 

        /// <summary>
        /// 准备进入的CTCS
        /// </summary>
        ICTCS NextCTCS { get; }

        /// <summary>
        /// 已经生效
        /// </summary>
        bool InEffect { get; }
    }

    /// <summary>
    /// CTCS 等级 类型
    /// </summary>
    public enum CTCSType
    {
        Unkown,
        CTCS0,
        CTCS1,
        CTCS2,
        CTCS3,
    }
}