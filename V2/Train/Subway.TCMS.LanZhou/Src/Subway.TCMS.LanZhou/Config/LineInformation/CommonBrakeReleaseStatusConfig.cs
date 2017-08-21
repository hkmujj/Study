using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Excel.Interface;

namespace Subway.TCMS.LanZhou.Config.LineInformation
{
    [ExcelLocation("Subway.TCMS.LanZhou旁路信息常用制动缓解旁路.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CommonBrakeReleaseStatusConfig : ISetValueProvider
    {
        [ExcelField("CarIndex")]
        public int Index { get; private set; }

        [ExcelField("旁路")]
        public string CommonBrakeReleaseLineInf { get; private set; }

        [ExcelField("正常")]
        public string CommonBrakeReleaseNormal { get; private set; }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}
