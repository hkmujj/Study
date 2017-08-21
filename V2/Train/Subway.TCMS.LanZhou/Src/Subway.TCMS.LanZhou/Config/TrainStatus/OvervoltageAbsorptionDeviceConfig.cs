using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Excel.Interface;

namespace Subway.TCMS.LanZhou.Config.TrainStatus
{
    [ExcelLocation("Subway.TCMS.LanZhou车辆状态牵引过压吸收装置.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class OvervoltageAbsorptionDeviceConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int Index { get; private set; }

        [ExcelField("工作")]
        public string OvervoltageRunning { get; private set; }

        [ExcelField("未工作")]
        public string OvervoltageStop { get; private set; }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}
