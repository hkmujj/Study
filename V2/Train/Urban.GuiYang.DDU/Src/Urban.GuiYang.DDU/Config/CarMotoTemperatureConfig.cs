using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Urban.GuiYang.DDU.Config
{
    [ExcelLocation("Urban.GuiYang.DDU列车Car电机温度配置.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarMotoTemperatureConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int Index { get; private set; }

        [ExcelField("电机1数值")]
        public string Moto1ValueIndex { get; private set; }

        [ExcelField("电机1未知")]
        public string Moto1UnkownIndex { get; private set; }

        [ExcelField("电机2数值")]
        public string Moto2ValueIndex { get; private set; }

        [ExcelField("电机2未知")]
        public string Moto2UnkownIndex { get; private set; }

        [ExcelField("电机3数值")]
        public string Moto3ValueIndex { get; private set; }

        [ExcelField("电机3未知")]
        public string Moto3UnkownIndex { get; private set; }

        [ExcelField("电机4数值")]
        public string Moto4ValueIndex { get; private set; }

        [ExcelField("电机4未知")]
        public string Moto4UnkownIndex { get; private set; }

        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }





    
}