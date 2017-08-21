using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Excel.Interface;

namespace Subway.TCMS.LanZhou.Config.TrainStatus
{
    [ExcelLocation("Subway.TCMS.LanZhou车辆状态制动转向架切除.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class BogieResectionStatusConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int Index { get; private set; }

        [ExcelField("转向架1切除")]
        public string Bogie1Resection { get; private set; }

        [ExcelField("转向架1正常")]
        public string Bogie1ResectionNormal { get; private set; }
        [ExcelField("转向架2切除")]
        public string Bogie2Resection { get; private set; }

        [ExcelField("转向架2正常")]
        public string Bogie2ResectionNormal { get; private set; }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}
