using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Urban.GuiYang.DDU.Config
{
    [ExcelLocation("Urban.GuiYang.DDU列车Car机组2回风阀状态配置.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarGroup2BackAirValveConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int Index { get; private set; }

        [ExcelField("回风阀1打开")]
        public string Valve1OpenedIndex { get; private set; }

        [ExcelField("回风阀1关闭")]
        public string Valve1ClosedIndex { get; private set; }

        [ExcelField("回风阀1故障")]
        public string Valve1FaultIndex { get; private set; }

        [ExcelField("回风阀1未知")]
        public string Valve1UnkownIndex { get; private set; }


        [ExcelField("回风阀2打开")]
        public string Valve2OpenedIndex { get; private set; }

        [ExcelField("回风阀2关闭")]
        public string Valve2ClosedIndex { get; private set; }

        [ExcelField("回风阀2故障")]
        public string Valve2FaultIndex { get; private set; }

        [ExcelField("回风阀2未知")]
        public string Valve2UnkownIndex { get; private set; }


        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {
        }
    }

    
}
