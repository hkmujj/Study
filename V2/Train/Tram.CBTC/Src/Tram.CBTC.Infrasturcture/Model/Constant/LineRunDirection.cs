using System.ComponentModel;

namespace Tram.CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// 线路运行方向
    /// 0、未定义
    /// 1、上行
    /// 2、下行
    /// </summary>
    public enum LineRunDirection
    {
        /// <summary>
        /// 未定义
        /// </summary>
        [Description("")]
        None,
        /// <summary>
        /// 上行
        /// </summary>
        [Description("上行")]
        Up,
        /// <summary>
        /// 下行
        /// </summary>
        [Description("下行")]
        Down
    }
}
