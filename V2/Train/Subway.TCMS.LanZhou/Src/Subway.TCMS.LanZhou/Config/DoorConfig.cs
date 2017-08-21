using System.Diagnostics.CodeAnalysis;
using Excel.Interface;

namespace Subway.TCMS.LanZhou.Config
{
    [ExcelLocation("Subway.TCMS.LanZhou门状态配置.xls", "Sheet1")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class DoorConfig : ISetValueProvider
    {
        [ExcelField("CarIndex")]
        public int CarIndex { get; private set; }

        [ExcelField("DoorIndex1切除")]
        public string Door1IndexResection { get; private set; }

        [ExcelField("DoorIndex1紧急释放")]
        public string Door1IndexEmergencyRelease { get; private set; }

        [ExcelField("DoorIndex1故障")]
        public string Door1IndexFault { get; private set; }

        [ExcelField("DoorIndex1障碍物")]
        public string Door1IndexDetectsObstacles { get; private set; }

        [ExcelField("DoorIndex1正在开关")]
        public string Door1IndexSwitch { get; private set; }

        [ExcelField("DoorIndex1开到位")]
        public string Door1IndexOpen { get; private set; }

        [ExcelField("DoorIndex1关并锁到位")]
        public string Door1IndexClosed { get; private set; }

        [ExcelField("DoorIndex1状态未知")]
        public string Door1IndexUnknow { get; private set; }


        [ExcelField("DoorIndex2切除")]
        public string Door2IndexResection { get; private set; }

        [ExcelField("DoorIndex2紧急释放")]
        public string Door2IndexEmergencyRelease { get; private set; }

        [ExcelField("DoorIndex2故障")]
        public string Door2IndexFault { get; private set; }

        [ExcelField("DoorIndex2障碍物")]
        public string Door2IndexDetectsObstacles { get; private set; }

        [ExcelField("DoorIndex2正在开关")]
        public string Door2IndexSwitch { get; private set; }

        [ExcelField("DoorIndex2开到位")]
        public string Door2IndexOpen { get; private set; }

        [ExcelField("DoorIndex2关并锁到位")]
        public string Door2IndexClosed { get; private set; }

        [ExcelField("DoorIndex2状态未知")]
        public string Door2IndexUnknow { get; private set; }


        [ExcelField("DoorIndex3切除")]
        public string Door3IndexResection { get; private set; }

        [ExcelField("DoorIndex3紧急释放")]
        public string Door3IndexEmergencyRelease { get; private set; }

        [ExcelField("DoorIndex3故障")]
        public string Door3IndexFault { get; private set; }

        [ExcelField("DoorIndex3障碍物")]
        public string Door3IndexDetectsObstacles { get; private set; }

        [ExcelField("DoorIndex3正在开关")]
        public string Door3IndexSwitch { get; private set; }

        [ExcelField("DoorIndex3开到位")]
        public string Door3IndexOpen { get; private set; }

        [ExcelField("DoorIndex3关并锁到位")]
        public string Door3IndexClosed { get; private set; }

        [ExcelField("DoorIndex3状态未知")]
        public string Door3IndexUnknow { get; private set; }


        [ExcelField("DoorIndex4切除")]
        public string Door4IndexResection { get; private set; }

        [ExcelField("DoorIndex4紧急释放")]
        public string Door4IndexEmergencyRelease { get; private set; }

        [ExcelField("DoorIndex4故障")]
        public string Door4IndexFault { get; private set; }

        [ExcelField("DoorIndex4障碍物")]
        public string Door4IndexDetectsObstacles { get; private set; }

        [ExcelField("DoorIndex4正在开关")]
        public string Door4IndexSwitch { get; private set; }

        [ExcelField("DoorIndex4开到位")]
        public string Door4IndexOpen { get; private set; }

        [ExcelField("DoorIndex4关并锁到位")]
        public string Door4IndexClosed { get; private set; }

        [ExcelField("DoorIndex4状态未知")]
        public string Door4IndexUnknow { get; private set; }


        [ExcelField("DoorIndex5切除")]
        public string Door5IndexResection { get; private set; }

        [ExcelField("DoorIndex5紧急释放")]
        public string Door5IndexEmergencyRelease { get; private set; }

        [ExcelField("DoorIndex5故障")]
        public string Door5IndexFault { get; private set; }

        [ExcelField("DoorIndex5障碍物")]
        public string Door5IndexDetectsObstacles { get; private set; }

        [ExcelField("DoorIndex5正在开关")]
        public string Door5IndexSwitch { get; private set; }

        [ExcelField("DoorIndex5开到位")]
        public string Door5IndexOpen { get; private set; }

        [ExcelField("DoorIndex5关并锁到位")]
        public string Door5IndexClosed { get; private set; }

        [ExcelField("DoorIndex5状态未知")]
        public string Door5IndexUnknow { get; private set; }


        [ExcelField("DoorIndex6切除")]
        public string Door6IndexResection { get; private set; }

        [ExcelField("DoorIndex6紧急释放")]
        public string Door6IndexEmergencyRelease { get; private set; }

        [ExcelField("DoorIndex6故障")]
        public string Door6IndexFault { get; private set; }

        [ExcelField("DoorIndex6障碍物")]
        public string Door6IndexDetectsObstacles { get; private set; }

        [ExcelField("DoorIndex6正在开关")]
        public string Door6IndexSwitch { get; private set; }

        [ExcelField("DoorIndex6开到位")]
        public string Door6IndexOpen { get; private set; }

        [ExcelField("DoorIndex6关并锁到位")]
        public string Door6IndexClosed { get; private set; }

        [ExcelField("DoorIndex6状态未知")]
        public string Door6IndexUnknow { get; private set; }


        [ExcelField("DoorIndex7切除")]
        public string Door7IndexResection { get; private set; }

        [ExcelField("DoorIndex7紧急释放")]
        public string Door7IndexEmergencyRelease { get; private set; }

        [ExcelField("DoorIndex7故障")]
        public string Door7IndexFault { get; private set; }

        [ExcelField("DoorIndex7障碍物")]
        public string Door7IndexDetectsObstacles { get; private set; }

        [ExcelField("DoorIndex7正在开关")]
        public string Door7IndexSwitch { get; private set; }

        [ExcelField("DoorIndex7开到位")]
        public string Door7IndexOpen { get; private set; }

        [ExcelField("DoorIndex7关并锁到位")]
        public string Door7IndexClosed { get; private set; }

        [ExcelField("DoorIndex7状态未知")]
        public string Door7IndexUnknow { get; private set; }


        [ExcelField("DoorIndex8切除")]
        public string Door8IndexResection { get; private set; }

        [ExcelField("DoorIndex8紧急释放")]
        public string Door8IndexEmergencyRelease { get; private set; }

        [ExcelField("DoorIndex8故障")]
        public string Door8IndexFault { get; private set; }

        [ExcelField("DoorIndex8障碍物")]
        public string Door8IndexDetectsObstacles { get; private set; }

        [ExcelField("DoorIndex8正在开关")]
        public string Door8IndexSwitch { get; private set; }

        [ExcelField("DoorIndex8开到位")]
        public string Door8IndexOpen { get; private set; }

        [ExcelField("DoorIndex8关并锁到位")]
        public string Door8IndexClosed { get; private set; }

        [ExcelField("DoorIndex8状态未知")]
        public string Door8IndexUnknow { get; private set; }


        [ExcelField("DoorIndex9切除")]
        public string Door9IndexResection { get; private set; }

        [ExcelField("DoorIndex9紧急释放")]
        public string Door9IndexEmergencyRelease { get; private set; }

        [ExcelField("DoorIndex9故障")]
        public string Door9IndexFault { get; private set; }

        [ExcelField("DoorIndex9障碍物")]
        public string Door9IndexDetectsObstacles { get; private set; }

        [ExcelField("DoorIndex9正在开关")]
        public string Door9IndexSwitch { get; private set; }

        [ExcelField("DoorIndex9开到位")]
        public string Door9IndexOpen { get; private set; }

        [ExcelField("DoorIndex9关并锁到位")]
        public string Door9IndexClosed { get; private set; }

        [ExcelField("DoorIndex9状态未知")]
        public string Door9IndexUnknow { get; private set; }


        [ExcelField("DoorIndex10切除")]
        public string Door10IndexResection { get; private set; }

        [ExcelField("DoorIndex10紧急释放")]
        public string Door10IndexEmergencyRelease { get; private set; }

        [ExcelField("DoorIndex10故障")]
        public string Door10IndexFault { get; private set; }

        [ExcelField("DoorIndex10障碍物")]
        public string Door10IndexDetectsObstacles { get; private set; }

        [ExcelField("DoorIndex10正在开关")]
        public string Door10IndexSwitch { get; private set; }

        [ExcelField("DoorIndex10开到位")]
        public string Door10IndexOpen { get; private set; }

        [ExcelField("DoorIndex10关并锁到位")]
        public string Door10IndexClosed { get; private set; }

        [ExcelField("DoorIndex10状态未知")]
        public string Door10IndexUnknow { get; private set; }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {
        }
    }
}