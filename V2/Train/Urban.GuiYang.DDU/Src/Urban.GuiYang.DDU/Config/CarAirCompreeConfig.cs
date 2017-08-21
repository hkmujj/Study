using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Urban.GuiYang.DDU.Config
{
    [ExcelLocation("Urban.GuiYang.DDU列车Car空压机配置.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarAirCompreeConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int Index { get; private set; }

        [ExcelField("运行")]
        public string RunningIndex { get; private set; }

        [ExcelField("停止")]
        public string StopedIndex { get; private set; }

        [ExcelField("未知")]
        public string UnkownIndex { get; private set; }

        [ExcelField("故障")]
        public string FaultIndex { get; private set; }

        
        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {
        }
    }

    
}
