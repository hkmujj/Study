using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Excel.Interface;

namespace Subway.TCMS.LanZhou.Config.TrainStatus
{
    [ExcelLocation("Subway.TCMS.LanZhou车辆状态制动紧急制动可用.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class EmergencyBrakeAvailableStatusConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int Index { get; private set; }

        [ExcelField("紧急制动1正常")]
        public string EmergencyBrake1AvailableNormal { get; private set; }

        [ExcelField("紧急制动1不可用")]
        public string EmergencyBrake1AvailableNotavailable { get; private set; }
        [ExcelField("紧急制动2正常")]
        public string EmergencyBrake2AvailableNormal { get; private set; }

        [ExcelField("紧急制动2不可用")]
        public string EmergencyBrake2AvailableNotavailable { get; private set; }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}
