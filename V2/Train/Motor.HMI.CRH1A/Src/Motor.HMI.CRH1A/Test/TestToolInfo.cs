using System.Diagnostics;

namespace Motor.HMI.CRH1A.Test
{
    /// <summary>
    /// ������ʾ�����Ϣ
    /// </summary>
    [DebuggerDisplay("LogicalBit={LogicalBit}, ToolInfo={ToolInfo}")]
    public class TestToolInfo
    {
        /// <summary>
        /// �� �� λ
        /// </summary>
        public int LogicalBit { get; set; }

        /// <summary>
        /// ��ʾxinxi
        /// </summary>
        public string ToolInfo { get; set; }
    }
}