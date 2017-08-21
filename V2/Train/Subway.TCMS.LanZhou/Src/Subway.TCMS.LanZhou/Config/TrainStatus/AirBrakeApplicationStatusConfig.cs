using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Excel.Interface;

namespace Subway.TCMS.LanZhou.Config.TrainStatus
{
    [ExcelLocation("Subway.TCMS.LanZhou车辆状态制动空气制动施加.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
   public class AirBrakeApplicationStatusConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int Index { get; private set; }

        [ExcelField("空气制动1施加")]
        public string AirBrake1ApplicationExert { get; private set; }

        [ExcelField("空气制动1未施加")]
        public string AirBrake1ApplicationNotapplied { get; private set; }
        [ExcelField("空气制动2施加")]
        public string AirBrake2ApplicationExert { get; private set; }

        [ExcelField("空气制动2未施加")]
        public string AirBrake2ApplicationNotapplied { get; private set; }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}
