using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Excel.Interface;

namespace Subway.TCMS.LanZhou.Config.TrainStatus
{
    [ExcelLocation("Subway.TCMS.LanZhou车辆状态制动空簧压力.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class AirSpringPreStatusConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int Index { get; private set; }
     
        [ExcelField("空簧压力1有效")]
        public string AirSpringPre1Effective { get; private set; }

        [ExcelField("空簧压力1无效")]
        public string AirSpringPre1Invalid { get; private set; }

        
        [ExcelField("空簧压力2有效")]
        public string AirSpringPre2Effective { get; private set; }

        [ExcelField("空簧压力2无效")]
        public string AirSpringPre2Invalid { get; private set; }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}
