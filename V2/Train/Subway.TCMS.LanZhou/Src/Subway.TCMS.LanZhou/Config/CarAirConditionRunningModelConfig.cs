
using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Subway.TCMS.LanZhou.Config
{
    [ExcelLocation("Subway.TCMS.LanZhou空调运行模式.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarAirConditionRunningModelConfig : ISetValueProvider
    {
        [ExcelField("CarIndex")]
        public int Index { get; private set; }

        [ExcelField("空调本地控制")]
        public string AirConditionLocalControl { get; private set; }

        [ExcelField("空调自动冷")]
        public string AirConditionAutoCold { get; private set; }

        [ExcelField("空调通风")]
        public string AirConditionAirout { get; private set; }

        [ExcelField("空调强风")]
        public string AirConditionStrongwind { get; private set; }

        [ExcelField("空调紧急停机")]
        public string AirConditionShutdown { get; private set; }

        [ExcelField("空调状态未知")]
        public string AirConditionUnknow { get; private set; }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}
