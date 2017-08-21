using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Subway.TCMS.LanZhou.Config
{
    [ExcelLocation("Subway.TCMS.LanZhou牵引系统状态配置.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class TowSystemConfig :ISetValueProvider
    {
        [ExcelField("CarIndex")]
        public int Index { get; private set; }

        [ExcelField("系统状态未知")]
        public string TractionSystemUnknow { get; private set; }

        [ExcelField("系统切除")]
        public string TractionSystemResection { get; private set; }

        [ExcelField("系统故障")]
        public string TractionSystemFualt { get; private set; }

        [ExcelField("系统运行中")]
        public string TractionSystemRunning { get; private set; }

        [ExcelField("系统状态正常")]
        public string TractionSystemNormal { get; private set; }
        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}