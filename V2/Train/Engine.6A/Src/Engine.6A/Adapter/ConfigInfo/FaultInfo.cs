using System;
using Engine._6A.Enums;
using Engine._6A.Interface;
using Excel.Interface;

namespace Engine._6A.Adapter.ConfigInfo
{
    [ExcelLocation("Messages.xls", "FaultInfo")]
    public class FaultInfo : IFaultInfo, ISetValueProvider
    {
        [ExcelField("故障类型")]
        public FaultType Type { get; set; }
        [ExcelField("故障逻辑号")]
        public int LogicId { get; set; }
        public DateTime DataTime { get; set; }
        [ExcelField("子系统")]
        public SubSystem SubSystem { get; set; }
        [ExcelField("内容")]
        public string Context { get; set; }
        [ExcelField("分类")]
        public Distinction Distinction { get; set; }
        [ExcelField("位置")]
        public string Position { get; set; }
        public string Speed { get; set; }
        public void SetValue(string name, string value)
        {
            switch (name)
            {
                case "Type":
                    FaultType faultType;
                    Enum.TryParse(value, true, out faultType);
                    Type = faultType;
                    break;
                case "LogicId":
                    LogicId = Convert.ToInt32(value);
                    break;
                case "SubSystem":
                    SubSystem subSystem;
                    Enum.TryParse(value, true, out subSystem);
                    SubSystem = subSystem;
                    break;
                case "Context":
                    Context = value;
                    break;
                case "Distinction":
                    Distinction distinction;
                    Enum.TryParse(value, true, out distinction);
                    Distinction = distinction;
                    break;
                case "Position":
                    Position = value;
                    break;

            }
        }
    }
}