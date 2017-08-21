using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Motor.TCMS.CRH400BF.Config
{
    [ExcelLocation("Urban.GuiYang.DDU列车Car车门配置.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarDoorConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int CarIndex { get; private set; }

        [ExcelField("DoorIndex1打开")]
        public string DoorIndex1Open { get; private set; }

        [ExcelField("DoorIndex1锁闭")]
        public string DoorIndex1Close { get; private set; }

        [ExcelField("DoorIndex1故障")]
        public string DoorIndex1Fault { get; private set; }

        [ExcelField("DoorIndex1隔离")]
        public string DoorIndex1Isolation { get; private set; }

        [ExcelField("DoorIndex2打开")]
        public string DoorIndex2Open { get; private set; }

        [ExcelField("DoorIndex2锁闭")]
        public string DoorIndex2Close { get; private set; }

        [ExcelField("DoorIndex2故障")]
        public string DoorIndex2Fault { get; private set; }

        [ExcelField("DoorIndex2隔离")]
        public string DoorIndex2Isolation { get; private set; }

        [ExcelField("DoorIndex3打开")]
        public string DoorIndex3Open { get; private set; }

        [ExcelField("DoorIndex3锁闭")]
        public string DoorIndex3Close { get; private set; }

        [ExcelField("DoorIndex3故障")]
        public string DoorIndex3Fault { get; private set; }

        [ExcelField("DoorIndex3隔离")]
        public string DoorIndex3Isolation { get; private set; }

        [ExcelField("DoorIndex4打开")]
        public string DoorIndex4Open { get; private set; }

        [ExcelField("DoorIndex4锁闭")]
        public string DoorIndex4Close { get; private set; }

        [ExcelField("DoorIndex4故障")]
        public string DoorIndex4Fault { get; private set; }

        [ExcelField("DoorIndex4隔离")]
        public string DoorIndex4Isolation { get; private set; }

        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {
        }
    }
}

