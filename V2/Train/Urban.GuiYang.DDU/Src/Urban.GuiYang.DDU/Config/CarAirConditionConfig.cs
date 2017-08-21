using System;
using System.Diagnostics.CodeAnalysis;
using Excel.Interface;
using Urban.GuiYang.DDU.Model.Constant;

namespace Urban.GuiYang.DDU.Config
{
    [ExcelLocation("Urban.GuiYang.DDU列车Car空调状态配置.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarAirConditionConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int Index { get; private set; }

        [ExcelField("空调控制模式")]
        public AirConditionItemType ItemType { get; private set; }

        [ExcelField("机组1空调状态停止")]
        public string Group1StopedIndex { get; private set; }

        [ExcelField("机组1空调状态预冷")]
        public string Group1PreCoolIndex { get; private set; }

        [ExcelField("机组1空调状态制冷")]
        public string Group1MakeCoolIndex { get; private set; }

        [ExcelField("机组1空调状态全暖")]
        public string Group1AllWarmIndex { get; private set; }

        [ExcelField("机组1空调状态半暖")]
        public string Group1HalfWarmIndex { get; private set; }

        [ExcelField("机组1空调状态预热")]
        public string Group1PreWarmIndex { get; private set; }

        [ExcelField("机组1空调状态紧急通风")]
        public string Group1EmergencyWindIndex { get; private set; }

        [ExcelField("机组1空调状态正常通风")]
        public string Group1NormalWindIndex { get; private set; }

        [ExcelField("机组1空调状态故障")]
        public string Group1FaultIndex { get; private set; }

        [ExcelField("机组1空调状态未知")]
        public string Group1UnkownIndex { get; private set; }


        [ExcelField("机组2空调状态停止")]
        public string Group2StopedIndex { get; private set; }

        [ExcelField("机组2空调状态预冷")]
        public string Group2PreCoolIndex { get; private set; }

        [ExcelField("机组2空调状态制冷")]
        public string Group2MakeCoolIndex { get; private set; }

        [ExcelField("机组2空调状态全暖")]
        public string Group2AllWarmIndex { get; private set; }

        [ExcelField("机组2空调状态半暖")]
        public string Group2HalfWarmIndex { get; private set; }

        [ExcelField("机组2空调状态预热")]
        public string Group2PreWarmIndex { get; private set; }

        [ExcelField("机组2空调状态紧急通风")]
        public string Group2EmergencyWindIndex { get; private set; }

        [ExcelField("机组2空调状态正常通风")]
        public string Group2NormalWindIndex { get; private set; }

        [ExcelField("机组2空调状态故障")]
        public string Group2FaultIndex { get; private set; }

        [ExcelField("机组2空调状态未知")]
        public string Group2UnkownIndex { get; private set; }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {
            switch (propertyOrFieldName)
            {
                case nameof(ItemType):
                    ItemType = (AirConditionItemType)Enum.Parse(typeof(AirConditionItemType), value);
                    break;
            }
        }
    }

    
}
