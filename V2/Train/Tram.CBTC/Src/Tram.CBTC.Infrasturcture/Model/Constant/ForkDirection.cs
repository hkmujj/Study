using System.ComponentModel;

namespace Tram.CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// 道岔方向
    /// 0、未定义
    /// 1、正向
    /// 2、反向
    /// </summary>
    public enum ForkDirection
    {
        /// <summary>
        /// 未定义
        /// </summary>
        [Description("")]
        None,
        /// <summary>
        /// 正向
        /// </summary>
        [Description("正向")]
        Forward,
        /// <summary>
        /// 反向
        /// </summary>
        [Description("反向")]
        Opposite
    }
}
