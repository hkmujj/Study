using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Urban.GuiYang.DDU.Config
{
    [ExcelLocation("Urban.GuiYang.DDU列车Car机组1新风阀状态配置.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarGroup1NewAirValveConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int Index { get; private set; }

        [ExcelField("新风阀1全开")]
        public string Valve1ThreeIndex { get; private set; }

        [ExcelField("新风阀1三分之一开")]
        public string Valve1OneIndex { get; private set; }

        [ExcelField("新风阀1三分之二开")]
        public string Valve1TwoIndex { get; private set; }

        [ExcelField("新风阀1关闭")]
        public string Valve1ClosedIndex { get; private set; }

        [ExcelField("新风阀1故障")]
        public string Valve1FaultIndex { get; private set; }

        [ExcelField("新风阀1未知")]
        public string Valve1UnkownIndex { get; private set; }


        [ExcelField("新风阀2全开")]
        public string Valve2ThreeIndex { get; private set; }

        [ExcelField("新风阀2三分之一开")]
        public string Valve2OneIndex { get; private set; }

        [ExcelField("新风阀2三分之二开")]
        public string Valve2TwoIndex { get; private set; }

        [ExcelField("新风阀2关闭")]
        public string Valve2ClosedIndex { get; private set; }

        [ExcelField("新风阀2故障")]
        public string Valve2FaultIndex { get; private set; }

        [ExcelField("新风阀2未知")]
        public string Valve2UnkownIndex { get; private set; }


        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {
        }
    }

    
}
