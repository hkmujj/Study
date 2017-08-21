using System;

namespace Engine.HMI.HXD1C.TPX21A
{
    /// <summary>
    /// 故障类
    /// 包括 故障对应的逻辑位 故障等级 所属机车号
    /// 故障代码 故障内容 发生时间 故障提示对应代码 结束时间等
    /// </summary>
    public class Fault
    {
        public enum FaultLevel //故障等级种类  分A B C D 四个等级
        {
            A,
            B,
            C,
            D
        }

        public FaultLevel Level { get; set; }

        public string FaultCategory { get; set; }

        public int FaultID { get; set; }

        public string FaultText { get; set; }

        public DateTime HappenedTime { get; set; }

        public DateTime EndedTime { get; set; }

        public int LogicalBit { get; set; }

        public string HelpinfoV { get; set; }

        public string HelpinfoV0 { get; set; }

        public int CaNum { get; set; }

        public string ProcedInfo { get; set; }
    }
}