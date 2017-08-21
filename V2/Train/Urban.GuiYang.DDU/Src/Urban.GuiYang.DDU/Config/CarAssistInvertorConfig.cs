using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Urban.GuiYang.DDU.Config
{
    [ExcelLocation("Urban.GuiYang.DDU列车Car辅助逆变器配置.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarAssistInvertorConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int Index { get; private set; }

        [ExcelField("正常")]
        public string NormalIndex { get; private set; }

        [ExcelField("工作")]
        public string WorkingIndex { get; private set; }

        [ExcelField("未知")]
        public string UnkownIndex { get; private set; }

        [ExcelField("故障")]
        public string FaultIndex { get; private set; }

        [ExcelField("扩展供电闭合")]
        public string ExtendedPowerOnIndex { get; private set; }

        [ExcelField("扩展供电断开")]
        public string ExtendedPowerOffIndex { get; private set; }

        [ExcelField("扩展供电状态未知")]
        public string ExtendedPowerUnkownIndex { get; private set; }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {
        }
    }

    
}
