using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Urban.GuiYang.DDU.Config
{
    [ExcelLocation("Urban.GuiYang.DDU列车Car供电接触器配置.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarPowerSwitchConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int Index { get; private set; }

        [ExcelField("闭合")]
        public string OnIndex { get; private set; }

        [ExcelField("断开")]
        public string OffIndex { get; private set; }

        [ExcelField("未知")]
        public string UnkownIndex { get; private set; }


        public void SetValue(string propertyOrFieldName, string value)
        {
        }
    }
}
