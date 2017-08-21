using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Excel.Interface;

namespace Subway.TCMS.LanZhou.Config.TrainStatus
{
    [ExcelLocation("Subway.TCMS.LanZhou车辆状态制动保持制动施加.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class HoldBrakeApplyStatusConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int Index { get; private set; }

        [ExcelField("保持制动1施加")]
        public string HoldBrake1ApplyExert { get; private set; }

        [ExcelField("保持制动1未施加")]
        public string HoldBrake1ApplyNotavailable
        { get; private set; }
        [ExcelField("保持制动2施加")]
        public string HoldBrake2ApplyExert { get; private set; }

        [ExcelField("保持制动2未施加")]
        public string HoldBrake2ApplyNotavailable
        { get; private set; }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}
