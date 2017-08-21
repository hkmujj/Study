using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Urban.GuiYang.DDU.Config
{
    [ExcelLocation("Urban.GuiYang.DDU列车Car制动缸压力配置.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarBrakePressureConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int Index { get; private set; }

        [ExcelField("架1数值")]
        public string Branch1ValueIndex { get; private set; }

        [ExcelField("架1未知")]
        public string Branch1UnkownIndex { get; private set; }

        [ExcelField("架2数值")]
        public string Branch2ValueIndex { get; private set; }

        [ExcelField("架2未知")]
        public string Branch2UnkownIndex { get; private set; }
        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }





    
}