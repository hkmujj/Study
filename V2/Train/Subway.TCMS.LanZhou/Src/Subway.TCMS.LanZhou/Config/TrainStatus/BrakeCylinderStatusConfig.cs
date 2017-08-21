using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Excel.Interface;

namespace Subway.TCMS.LanZhou.Config.TrainStatus
{
    [ExcelLocation("Subway.TCMS.LanZhou车辆状态制动制动风缸压力.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
   public class BrakeCylinderStatusConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int Index { get; private set; }

        [ExcelField("制动风缸压力1有效")]
        public string BrakeCylinder1Effective { get; private set; }

        [ExcelField("制动风缸压力1无效")]
        public string BrakeCylinder1Invalied { get; private set; }
        [ExcelField("制动风缸压力2有效")]
        public string BrakeCylinder2Effective { get; private set; }

        [ExcelField("制动风缸压力2无效")]
        public string BrakeCylinder2Invalied { get; private set; }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}
