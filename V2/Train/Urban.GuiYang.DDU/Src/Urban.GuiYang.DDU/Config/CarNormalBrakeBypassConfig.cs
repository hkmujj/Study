using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Urban.GuiYang.DDU.Config
{
    [ExcelLocation("Urban.GuiYang.DDU列车Car所有常用制动缓解旁路开关.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarNormalBrakeBypassConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int Index { get; private set; }

        [ExcelField("闭合")]
        public string OnIndex { get; private set; }

        [ExcelField("断开")]
        public string OffIndex { get; private set; }

        [ExcelField("未知")]
        public string UnkownIndex { get; private set; }

       
        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {
        }
    }

    
}
