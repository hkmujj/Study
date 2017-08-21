
using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Subway.TCMS.LanZhou.Config
{
    [ExcelLocation("Subway.TCMS.LanZhou常用制动状态配置.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class BrakeStatusConfig : ISetValueProvider
    {
        [ExcelField("CarIndex")]
        public int Index { get; private set; }

        [ExcelField("常用制动状态未知1")]
        public string CommonBrakeUnknow1 { get; private set; }

        [ExcelField("常用制动施加1")]
        public string CommonBrakeApplication1 { get; private set; }

        [ExcelField("常用制动缓解1")]
        public string CommonBrakeRelease1 { get; private set; }

        [ExcelField("常用制动故障1")]
        public string CommonBrakeFault1 { get; private set; }

        [ExcelField("常用制动本地隔离1")]
        public string CommonBrakeIsolation1 { get; private set; }

        [ExcelField("常用制动状态未知2")]
        public string CommonBrakeUnknow2 { get; private set; }

        [ExcelField("常用制动施加2")]
        public string CommonBrakeApplication2 { get; private set; }

        [ExcelField("常用制动缓解2")]
        public string CommonBrakeRelease2 { get; private set; }

        [ExcelField("常用制动故障2")]
        public string CommonBrakeFault2 { get; private set; }

        [ExcelField("常用制动本地隔离2")]
        public string CommonBrakeIsolation2 { get; private set; }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}
