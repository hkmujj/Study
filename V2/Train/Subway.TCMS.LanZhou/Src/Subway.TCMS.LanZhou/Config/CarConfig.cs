using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Subway.TCMS.LanZhou.Config
{
    [ExcelLocation("Subway.TCMS.LanZhou列车配置.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarConfig : ISetValueProvider
    {
        [ExcelField("GroupId")]
        public int GroupId { get; private set; }

        [ExcelField("CarId")]
        public int CarId { get; private set; }

        [ExcelField("ShowingName")]
        public string ShowingName { get; private set; }

        [ExcelField("是否为司机室")]
        public bool IsDriverRoom { get; private set; }

        [ExcelField("司机室状态索引")]
        public string DriverRoomState { get; private set; }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}