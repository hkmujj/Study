using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Excel.Interface;

namespace Subway.TCMS.LanZhou.Config.TrainStatus
{
    [ExcelLocation("Subway.TCMS.LanZhou车辆状态制动空簧压力数值.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class AirSpringPreDataConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int Index { get; private set; }

        [ExcelField("空簧压力1数值")]
        public string Value1Index { get; private set; }

        [ExcelField("空簧压力1未知")]
        public string Unkown1Index { get; private set; }


        [ExcelField("空簧压力2数值")]
        public string Value2Index { get; private set; }

        [ExcelField("空簧压力2未知")]
        public string Unkown2Index { get; private set; }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}
