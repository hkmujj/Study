using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Urban.GuiYang.DDU.Config
{
    [ExcelLocation("Urban.GuiYang.DDU列车Car接地隔离开关配置.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarGroundConnectConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int Index { get; private set; }

        [ExcelField("车间电源位")]
        public string PowerIndex { get; private set; }

        [ExcelField("受电弓位")]
        public string PantographIndex { get; private set; }

        [ExcelField("未知")]
        public string UnkownIndex { get; private set; }

        [ExcelField("接地位")]
        public string GroundIndex { get; private set; }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {
        }
    }

    
}
