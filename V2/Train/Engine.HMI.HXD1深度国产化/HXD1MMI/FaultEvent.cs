using System;

namespace HXD1.DeepDomestic
{
    /// <summary>
    /// 故障类
    /// 包括 故障对应的逻辑位 故障等级 所属机车号
    /// 故障代码 故障内容 发生时间 故障提示对应代码 结束时间等
    /// </summary>
    public class Fault : IComparable<Fault>
    {
        public Fault()
        {
            EndedTime = new DateTime();
        }

        public Fault(Fault item)
        {
            EndedTime = new DateTime();
            Level = item.Level;
            TrainNum = item.TrainNum;
            FaultID = item.FaultID;
            FaultText = item.FaultText;
            LogicalBit = item.LogicalBit;
            Helpinfo_V_Big_0 = item.Helpinfo_V_Big_0;
            HelpinfoV_Equal_0 = item.HelpinfoV_Equal_0;
            HelpInfo = item.HelpInfo;
            XXXXXX = item.XXXXXX;
        }

        public bool Equals(object o)
        {
            Fault fault = o as Fault;
            if (fault != null)
            {
                if (LogicalBit == fault.LogicalBit)
                    return true;
            }
            return false;
        }

        public int CompareTo(Fault other)
        {
            TimeSpan ts = HappenedTime - other.HappenedTime;
            if (ts.Seconds==0)
            {
                var rlt = FaultID.CompareTo(other.FaultID);
                if (rlt == 0)
                {
                    return FaultText.CompareTo(other.FaultText);
                }
                return rlt;
            }
            return ts.Seconds;
        }

        public FaultLevel Level { get; set; }

        public int TrainNum { get; set; }

        public int FaultID { get; set; }

        public string FaultText { get; set; }

        public DateTime HappenedTime { get; set; }

        public DateTime EndedTime { get; set; }

        public int LogicalBit { get; set; }

        public string HelpinfoV_Equal_0 { get; set; }

        public string Helpinfo_V_Big_0 { get; set; }

        public string HelpInfo { get; set; }

        public string XXXXXX { get; set; }
    }
}
