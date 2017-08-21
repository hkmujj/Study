using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Excel.Interface;

namespace Subway.TCMS.LanZhou.Config
{
    [ExcelLocation("Subway.TCMS.LanZhou空调冷凝机.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarAirConditionCondensingMachineConfig : ISetValueProvider
    {
        [ExcelField("CarIndex")]
        public int Index { get; private set; }

        [ExcelField("冷凝机运行1")]
        public string CondensingRunning1 { get; private set; }

        [ExcelField("冷凝机未运行1")]
        public string CondensingStop1 { get; private set; }

        [ExcelField("冷凝机运行2")]
        public string CondensingRunning2 { get; private set; }

        [ExcelField("冷凝机未运行2")]
        public string CondensingStop2 { get; private set; }

        [ExcelField("冷凝机运行3")]
        public string CondensingRunning3 { get; private set; }

        [ExcelField("冷凝机未运行3")]
        public string CondensingStop3 { get; private set; }

        [ExcelField("冷凝机运行4")]
        public string CondensingRunning4 { get; private set; }

        [ExcelField("冷凝机未运行4")]
        public string CondensingStop4 { get; private set; }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}
