using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Excel.Interface;

namespace Urban.GuiYang.DDU.Config
{
    [ExcelLocation("Urban.GuiYang.DDU列车Car蓄电池温度配置.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarStorageBatterytemperatureConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int Index { get; private set; }

        [ExcelField("数值")]
        public string ValueIndex { get; private set; }

        [ExcelField("未知")]
        public string UnkownIndex { get; private set; }


        public void SetValue(string propertyOrFieldName, string value)
        {
        }
    }
}
