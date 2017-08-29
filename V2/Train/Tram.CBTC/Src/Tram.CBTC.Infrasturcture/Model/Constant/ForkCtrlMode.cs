using System.ComponentModel;

namespace Tram.CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// 道岔控制模式
    /// 0、未定义
    /// 1、手动
    /// 2、自动
    /// </summary>
    public enum ForkCtrlMode
    {
        /// <summary>
        /// 未定义
        /// </summary>
        [Description("")]
        None,
        /// <summary>
        /// 手动
        /// </summary>
        [Description("手动")]
        Hand,
        /// <summary>
        /// 自动
        /// </summary>
        [Description("自动")]
        Auto
    }
}
