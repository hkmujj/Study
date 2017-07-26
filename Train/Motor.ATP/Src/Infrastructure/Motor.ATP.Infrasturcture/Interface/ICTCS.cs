using System.ComponentModel;

namespace Motor.ATP.Infrasturcture.Interface
{
    /// <summary>
    /// CTCS 等级
    /// </summary>
    public interface ICTCS : IATPPartial, IVisibility
    {
        /// <summary>
        /// 当前类型
        /// </summary>
        CTCSType CurrentCTCSType { get; }

        /// <summary>
        /// 准备进入的CTCS
        /// </summary>
        CTCSType NextCTCSType { get; }

        /// <summary>
        /// 已经生效
        /// </summary>
        bool InEffect { get; }
        /// <summary>
        /// 可见性
        /// </summary>
        bool Visible { set; get; }

    }

    #pragma warning disable 1591
    /// <summary>
    /// CTCS 等级 类型
    /// </summary>
    public enum CTCSType
    {
        [Description("")] Unkown = -1,

        [Description("CTCS 0")] CTCS0 = 0,

        [Description("CTCS 1")] CTCS1 = 1,

        [Description("CTCS 2")] CTCS2 = 2,

        [Description("CTCS 3")] CTCS3 = 3,

        [Description("CTCS 3D")] CTCS3D = 4,
    }
}