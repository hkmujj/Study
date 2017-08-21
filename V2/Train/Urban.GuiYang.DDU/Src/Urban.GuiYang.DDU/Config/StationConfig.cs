using System.Diagnostics;
using Excel.Interface;

namespace Urban.GuiYang.DDU.Config
{
    [ExcelLocation("贵阳地铁车站配置.xls", "Sheet1")]
    [DebuggerDisplay("StationKey={StationKey}, LineId={LineId}, StationCH={StationCH}, StationEN={StationEN}")]
    public class StationConfig : ISetValueProvider
    {
        [DebuggerStepThrough]
        public StationConfig(ulong index, string stationCH, string stationEN, string lineId)
        {
            StationKey = index;
            StationCH = stationCH;
            StationEN = stationEN;
            LineId = lineId;
        }

        public StationConfig()
        {
        }

        [ExcelField("StationKey", false)]
        public ulong StationKey { private set; get; }

        [ExcelField("线路号")]
        public string LineId { get; private set; }

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