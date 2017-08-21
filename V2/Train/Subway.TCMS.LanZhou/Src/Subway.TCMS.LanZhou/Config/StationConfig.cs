using System.Diagnostics;
using Excel.Interface;

namespace Subway.TCMS.LanZhou.Config
{
    [ExcelLocation("兰州地铁1号线车站配置.xls", "Sheet1")]
    [DebuggerDisplay("StationKey={StationKey}, StationCH={StationCH}, StationEN={StationEN}")]
    public class StationConfig : ISetValueProvider
    {
        [DebuggerStepThrough]
        public StationConfig(ulong index, string stationCH, string stationEN)
        {
            StationKey = index;
            StationCH = stationCH;
            StationEN = stationEN;
        }

        public StationConfig()
        {
        }

        [ExcelField("StationKey", false)]
        public ulong StationKey { private set; get; }

        [ExcelField("StationCH")]
        public string StationCH { private set; get; }

        [ExcelField("StationEN")]
        public string StationEN { private set; get; }

        /// <summary>
        /// 车站， TODO 增加中英文判断
        /// </summary>
        public string Staion
        {
            get { return StationCH; }
        }

        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {
        }
    }
}