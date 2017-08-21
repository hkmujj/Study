
using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Subway.TCMS.LanZhou.Config
{
    [ExcelLocation("Subway.TCMS.LanZhou辅助系统状态配置.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class AuxiliarySystemStatusConfig : ISetValueProvider
    {
        [ExcelField("CarIndex")]
        public int Index { get; private set; }

        [ExcelField("系统状态未知")]
        public string AuxiliarySystemStatusUnknow { get; private set; }

        [ExcelField("系统状态切除")]
        public string AuxiliarySystemStatusResection { get; private set; }
        [ExcelField("系统状态故障")]
        public string AuxiliarySystemStatusFailure { get; private set; }

        [ExcelField("系统状态运行")]
        public string AuxiliarySystemStatusRunning { get; private set; }

        [ExcelField("系统状态正常")]
        public string AuxiliarySystemStatusNormal { get; private set; }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}
