using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Excel.Interface;

namespace Urban.GuiYang.DDU.Config
{
    [ExcelLocation("Urban.GuiYang.DDU列车Car充电机状态配置.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarBatteryChargerStateConfig : ISetValueProvider
    {

        [ExcelField("Car索引")]
        public int Index { get; private set; }

        [ExcelField("正常")]
        public string NormalIndex { get; private set; }

        [ExcelField("故障")]
        public string FaultIndex { get; private set; }

        [ExcelField("工作")]
        public string WorkingIndex { get; private set; }

        [ExcelField("未知")]
        public string UnkownIndex { get; private set; }

        [ExcelField("数值")]
        public string ValueIndex { get; private set; }



        public void SetValue(string propertyOrFieldName, string value)
        {
        }
    }
}
