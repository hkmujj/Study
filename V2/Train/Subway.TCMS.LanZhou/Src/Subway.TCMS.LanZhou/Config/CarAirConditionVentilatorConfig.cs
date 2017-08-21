using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Excel.Interface;

namespace Subway.TCMS.LanZhou.Config
{
    [ExcelLocation("Subway.TCMS.LanZhou空调通风机.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarAirConditionVentilatorConfig : ISetValueProvider
    {
        [ExcelField("CarIndex")]
        public int Index { get; private set; }

        [ExcelField("通风机运行1")]
        public string VentilatorRunning1 { get; private set; }

        [ExcelField("通风机未运行1")]
        public string VentilatorStop1 { get; private set; }

        [ExcelField("通风机运行2")]
        public string VentilatorRunning2 { get; private set; }

        [ExcelField("通风机未运行2")]
        public string VentilatorStop2 { get; private set; }

        [ExcelField("通风机运行3")]
        public string VentilatorRunning3 { get; private set; }

        [ExcelField("通风机未运行3")]
        public string VentilatorStop3 { get; private set; }

        [ExcelField("通风机运行4")]
        public string VentilatorRunning4 { get; private set; }

        [ExcelField("通风机未运行4")]
        public string VentilatorStop4 { get; private set; }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}
