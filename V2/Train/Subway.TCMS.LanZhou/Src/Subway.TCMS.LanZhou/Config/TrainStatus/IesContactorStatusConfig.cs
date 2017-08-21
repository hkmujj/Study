using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Excel.Interface;

namespace Subway.TCMS.LanZhou.Config.TrainStatus
{
    [ExcelLocation("Subway.TCMS.LanZhou车辆状态牵引IES接触器.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class IesContactorStatusConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int Index { get; private set; }

        [ExcelField("状态未知")]
        public string IesContactorUnknow { get; private set; }

        [ExcelField("运行")]
        public string IesContactorRunning { get; private set; }

        [ExcelField("车间电源")]
        public string IesContactorWorkshoppower { get; private set; }

        [ExcelField("接地")]
        public string IesContactorEarthcircuit { get; private set; }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}
