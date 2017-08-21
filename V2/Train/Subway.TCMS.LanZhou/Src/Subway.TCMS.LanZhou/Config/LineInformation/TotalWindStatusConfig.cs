using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Excel.Interface;

namespace Subway.TCMS.LanZhou.Config.LineInformation
{
    [ExcelLocation("Subway.TCMS.LanZhou旁路信息总风地压旁路.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class TotalWindStatusConfig : ISetValueProvider
    {
        [ExcelField("CarIndex")]
        public int Index { get; private set; }

        [ExcelField("旁路")]
        public string TotalWindLineInf { get; private set; }

        [ExcelField("正常")]
        public string TotalWindNormal { get; private set; }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}
