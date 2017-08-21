using System.Collections.Generic;
using System.Diagnostics;

namespace CRH2MMI.Fault
{
    /// <summary>
    /// 配置文件中的故障信息
    /// </summary>
    [DebuggerDisplay("FaultLogicIndex={FaultLogicIndex} FaultNo={FaultNo} 车厢{FaultCarNos} {FaultName}")]
    public class FaultNameInfo
    {
        /// <summary>
        /// 故障逻辑位索引
        /// </summary>
        public int FaultLogicIndex { set; get; }

        /// <summary>
        /// 故障车厢号
        /// </summary>
        public List<int> FaultCarNos { set; get; }

        /// <summary>
        /// 错误号
        /// </summary>
        public int FaultNo { set; get; }

        public string FaultName { set; get; }

        /// <summary>
        /// 保护装置
        /// </summary>
        public string ProtectedMachine { set; get; }

        /// <summary>
        /// 处理方法图片
        /// </summary>
        public string FaultProcessImageFile { set; get; }
    }
}
