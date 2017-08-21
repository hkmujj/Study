using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Motor.TCMS.CRH400BF.Config
{
   
    [ExcelLocation("Motor.TCMS.CRH400BF列车Car充电机状态配置.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarBatteryChargerConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int Index { get; private set; }

        [ExcelField("充电机1未知")]
        public string BatteryCharger1UnKnow { get; private set; }

        [ExcelField("充电机1工作")]
        public string BatteryCharger1Run { get; private set; }

        [ExcelField("充电机1未工作")]
        public string BatteryCharger1NotRun { get; private set; }

       [ExcelField("充电机1故障")]
        public string BatteryCharger1Fault { get; private set; }

        [ExcelField("充电机1切除")]
        public string BatteryCharger1CutOff { get; private set; }

        [ExcelField("充电机2未知")]
        public string BatteryCharger2UnKnow { get; private set; }

        [ExcelField("充电机2工作")]
        public string BatteryCharger2Run { get; private set; }

        [ExcelField("充电机2未工作")]
        public string BatteryCharger2NotRun { get; private set; }

        [ExcelField("充电机2故障")]
        public string BatteryCharger2Fault { get; private set; }

        [ExcelField("充电机2切除")]
        public string BatteryCharger2CutOff { get; private set; }

        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {
        }
    }
}
