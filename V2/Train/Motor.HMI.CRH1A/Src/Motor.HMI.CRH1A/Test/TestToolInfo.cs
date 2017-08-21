using System.Diagnostics;

namespace Motor.HMI.CRH1A.Test
{
    /// <summary>
    /// 测试提示相关信息
    /// </summary>
    [DebuggerDisplay("LogicalBit={LogicalBit}, ToolInfo={ToolInfo}")]
    public class TestToolInfo
    {
        /// <summary>
        /// 接 口 位
        /// </summary>
        public int LogicalBit { get; set; }

        /// <summary>
        /// 提示xinxi
        /// </summary>
        public string ToolInfo { get; set; }
    }
}