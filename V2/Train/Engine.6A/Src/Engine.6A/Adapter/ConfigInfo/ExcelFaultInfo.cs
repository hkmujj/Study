using System;
using Engine._6A.Enums;
using Excel.Interface;

namespace Engine._6A.Adapter.ConfigInfo
{
    [ExcelLocation("FaultInfo.xls", "Sheet1")]
    public class ExcelFaultInfo : ISetValueProvider
    {
        [ExcelField("故障类型")]
        public FaultType FaultType { get; set; }
        [ExcelField("故障逻辑号")]
        public int LogicID { get; set; }
        [ExcelField("子系统")]
        public SubSystem SubSystem { get; set; }
        [ExcelField("内容")]
        public string Content { get; set; }
        [ExcelField("分类")]
        public Distinction Distinction { get; set; }
        [ExcelField("位置")]
        public string Position { get; set; }
        public DateTime DateTime { get; set; }

        public void SetValue(string name, string value)
        {
            switch (name)
            {
                case "FaultType":
                    FaultType faultType;
                    Enum.TryParse(value, true, out faultType);
                    FaultType = faultType;
                    break;
                case "LogicID":
                    LogicID = Convert.ToInt32(value);
                    break;
                case "SubSystem":
                    SubSystem subSystem;
                    Enum.TryParse(value, true, out subSystem);
                    SubSystem = subSystem;
                    break;
                case "Content":
                    Content = value;
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
