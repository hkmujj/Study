using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Urban.GuiYang.DDU.Config
{
    [ExcelLocation("Urban.GuiYang.DDU列车Car车门配置.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CarDoorConfig : ISetValueProvider
    {
        [ExcelField("Car索引")]
        public int CarIndex { get; private set; }

        [ExcelField("Door1Index打开")]
        public string Door1IndexOpen { get; private set; }

        [ExcelField("Door1Index关闭")]
        public string Door1IndexClose { get; private set; }

        [ExcelField("Door1Index检测到障碍物")]
        public string Door1IndexCheckedObstruction { get; private set; }

        [ExcelField("Door1Index紧急解锁")]
        public string Door1IndexEmergLock { get; private set; }

        [ExcelField("Door1Index故障关")]
        public string Door1IndexFaultClose { get; private set; }

        [ExcelField("Door1Index故障开")]
        public string Door1IndexFaultOpen { get; private set; }

        [ExcelField("Door1Index隔离")]
        public string Door1IndexIsolation { get; private set; }


        [ExcelField("Door2Index打开")]
        public string Door2IndexOpen { get; private set; }

        [ExcelField("Door2Index关闭")]
        public string Door2IndexClose { get; private set; }

        [ExcelField("Door2Index检测到障碍物")]
        public string Door2IndexCheckedObstruction { get; private set; }

        [ExcelField("Door2Index紧急解锁")]
        public string Door2IndexEmergLock { get; private set; }

        [ExcelField("Door2Index故障关")]
        public string Door2IndexFaultClose { get; private set; }

        [ExcelField("Door2Index故障开")]
        public string Door2IndexFaultOpen { get; private set; }

        [ExcelField("Door2Index隔离")]
        public string Door2IndexIsolation { get; private set; }



        [ExcelField("Door3Index打开")]
        public string Door3IndexOpen { get; private set; }

        [ExcelField("Door3Index关闭")]
        public string Door3IndexClose { get; private set; }

        [ExcelField("Door3Index检测到障碍物")]
        public string Door3IndexCheckedObstruction { get; private set; }

        [ExcelField("Door3Index紧急解锁")]
        public string Door3IndexEmergLock { get; private set; }

        [ExcelField("Door3Index故障关")]
        public string Door3IndexFaultClose { get; private set; }

        [ExcelField("Door3Index故障开")]
        public string Door3IndexFaultOpen { get; private set; }

        [ExcelField("Door3Index隔离")]
        public string Door3IndexIsolation { get; private set; }


        [ExcelField("Door4Index打开")]
        public string Door4IndexOpen { get; private set; }

        [ExcelField("Door4Index关闭")]
        public string Door4IndexClose { get; private set; }

        [ExcelField("Door4Index检测到障碍物")]
        public string Door4IndexCheckedObstruction { get; private set; }

        [ExcelField("Door4Index紧急解锁")]
        public string Door4IndexEmergLock { get; private set; }

        [ExcelField("Door4Index故障关")]
        public string Door4IndexFaultClose { get; private set; }

        [ExcelField("Door4Index故障开")]
        public string Door4IndexFaultOpen { get; private set; }

        [ExcelField("Door4Index隔离")]
        public string Door4IndexIsolation { get; private set; }


        [ExcelField("Door5Index打开")]
        public string Door5IndexOpen { get; private set; }

        [ExcelField("Door5Index关闭")]
        public string Door5IndexClose { get; private set; }

        [ExcelField("Door5Index检测到障碍物")]
        public string Door5IndexCheckedObstruction { get; private set; }

        [ExcelField("Door5Index紧急解锁")]
        public string Door5IndexEmergLock { get; private set; }

        [ExcelField("Door5Index故障关")]
        public string Door5IndexFaultClose { get; private set; }

        [ExcelField("Door5Index故障开")]
        public string Door5IndexFaultOpen { get; private set; }

        [ExcelField("Door5Index隔离")]
        public string Door5IndexIsolation { get; private set; }



        [ExcelField("Door6Index打开")]
        public string Door6IndexOpen { get; private set; }

        [ExcelField("Door6Index关闭")]
        public string Door6IndexClose { get; private set; }

        [ExcelField("Door6Index检测到障碍物")]
        public string Door6IndexCheckedObstruction { get; private set; }

        [ExcelField("Door6Index紧急解锁")]
        public string Door6IndexEmergLock { get; private set; }

        [ExcelField("Door6Index故障关")]
        public string Door6IndexFaultClose { get; private set; }

        [ExcelField("Door6Index故障开")]
        public string Door6IndexFaultOpen { get; private set; }

        [ExcelField("Door6Index隔离")]
        public string Door6IndexIsolation { get; private set; }


        [ExcelField("Door7Index打开")]
        public string Door7IndexOpen { get; private set; }

        [ExcelField("Door7Index关闭")]
        public string Door7IndexClose { get; private set; }

        [ExcelField("Door7Index检测到障碍物")]
        public string Door7IndexCheckedObstruction { get; private set; }

        [ExcelField("Door7Index紧急解锁")]
        public string Door7IndexEmergLock { get; private set; }

        [ExcelField("Door7Index故障关")]
        public string Door7IndexFaultClose { get; private set; }

        [ExcelField("Door7Index故障开")]
        public string Door7IndexFaultOpen { get; private set; }

        [ExcelField("Door7Index隔离")]
        public string Door7IndexIsolation { get; private set; }


        [ExcelField("Door8Index打开")]
        public string Door8IndexOpen { get; private set; }

        [ExcelField("Door8Index关闭")]
        public string Door8IndexClose { get; private set; }

        [ExcelField("Door8Index检测到障碍物")]
        public string Door8IndexCheckedObstruction { get; private set; }

        [ExcelField("Door8Index紧急解锁")]
        public string Door8IndexEmergLock { get; private set; }

        [ExcelField("Door8Index故障关")]
        public string Door8IndexFaultClose { get; private set; }

        [ExcelField("Door8Index故障开")]
        public string Door8IndexFaultOpen { get; private set; }

        [ExcelField("Door8Index隔离")]
        public string Door8IndexIsolation { get; private set; }

        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {
        }
    }
}