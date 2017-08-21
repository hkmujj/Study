using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Excel.Interface;

namespace Subway.TCMS.LanZhou.Config
{
    [ExcelLocation("Subway.TCMS.LanZhou停放制动状态配置.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class ParkingBrakeConfig : ISetValueProvider
    {
        [ExcelField("CarIndex")]
        public int Index { get; private set; }

        [ExcelField("停放制动状态未知")]
        public string ParkingBrakeUnknow { get; private set; }

        [ExcelField("停放制动施加")]
        public string ParkingBrakeApplication { get; private set; }

        [ExcelField("停放制动缓解")]
        public string ParkingBrakeRelease { get; private set; }
        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}
