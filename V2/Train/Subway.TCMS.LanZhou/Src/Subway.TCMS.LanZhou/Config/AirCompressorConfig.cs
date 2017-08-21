using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Excel.Interface;

namespace Subway.TCMS.LanZhou.Config
{
    [ExcelLocation("Subway.TCMS.LanZhou空压机状态配置.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class AirCompressorConfig : ISetValueProvider
    {
        [ExcelField("CarIndex")]
        public int Index { get; private set; }

        [ExcelField("空压机状态未知")]
        public string AirCompressorUnknow { get; private set; }

        [ExcelField("空压机工作")]
        public string AirCompressorRunning { get; private set; }

        [ExcelField("空压机未工作")]
        public string AirCompressorStop { get; private set; }

        [ExcelField("空压机故障")]
        public string AirCompressorFault { get; private set; }
        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}
