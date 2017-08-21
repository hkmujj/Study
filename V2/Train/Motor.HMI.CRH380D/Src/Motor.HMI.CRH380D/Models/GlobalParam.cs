using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommonUtil.Util;
using Excel.Interface;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using Motor.HMI.CRH380D.Models.ConfigModel;
using Motor.HMI.CRH380D.Models.Units;
using Motor.HMI.CRH380D.Service;

namespace Motor.HMI.CRH380D.Models
{
    public class GlobalParam
    {
        public static readonly GlobalParam Instance = new GlobalParam();

        public SubsystemInitParam InitParam { private set; get; }

        public string DesignName { get { return typeof(Subystem).Namespace; } }

        public IProjectIndexDescriptionConfig IndexDescription { private set; get; }
        
        //按键集合
        public List<StateInterfaceItem> StateInterfaceCollection { get; private set; }
        
        /// <summary>
        /// 外门集合
        /// </summary>
        public List<DoorUnit> DoorUnits { get; private set; }

        /// <summary>
        /// 门集合
        /// </summary>
        public List<StationUnit> StationUnits { get; private set; }

        /// <summary>
        /// 受电弓集合
        /// </summary>
        public List<PantographUnit> PantographUnits { get; private set; }

        /// <summary>
        /// LCB集合
        /// </summary>
        public List<LCBUnit> LCBUnits{ get; private set; }

        /// <summary>
        /// 高速断路器集合
        /// </summary>
        public List<QuickBreakUnit> QuickBreakUnits{ get; private set; }

        /// <summary>
        /// 接地开关集合
        /// </summary>
        public List<GroundingUnit> GroundingUnits { get; private set; }

        /// <summary>
        /// LCM集合
        /// </summary>
        public List<LCMUnit> LCMUnits { get; private set; }

        /// <summary>
        /// ACM集合
        /// </summary>
        public List<ACMUnit> ACMUnits { get; private set; }

        /// <summary>
        /// MCM集合
        /// </summary>
        public List<MCMUnit> MCMUnits { get; private set; }

        /// <summary>
        /// 停放制动集合
        /// </summary>
        public List<ParkBreakUnit> ParkBreakUnits { get; private set; }

        /// <summary>
        /// 紧急制动安全制动集合
        /// </summary>
        public List<EmergencyBreakUnit> EmergencyBreakUnits { get; private set; }

        /// <summary>
        /// BCU集合
        /// </summary>
        public List<BCUUnit> BCUUnits { get; private set; }

        /// <summary>
        /// 司机室舒适度集合
        /// </summary>
        public List<CarComfortUnit> CarComfortUnits { get; private set; }

        /// <summary>
        /// 主压缩机集合
        /// </summary>
        public List<PrimaryCompressorUnit> PrimaryCompressorUnits { get; private set; }

        /// <summary>
        /// 辅助压缩机集合
        /// </summary>
        public List<SubCompressorUnit> SubCompressorUnits { get; private set; }

        /// <summary>
        /// 压力传感器集合
        /// </summary>
        public List<PressureSensorUnit> PressureSensorUnits { get; private set; }

        /// <summary>
        /// 制动试验提示文本集合
        /// </summary>
        public List<BreakTestInfo> BreakTestInfos { get; private set; }

        /// <summary>
        /// 手柄测试提示文本集合
        /// </summary>
        public List<HandleTestInfo> HandleTestInfos { get; private set; }

        /// <summary>
        /// 直流供电设备1集合
        /// </summary>
        public List<DCDevice1Unit> DCDevice1Units { get; private set; }

        /// <summary>
        /// 直流供电设备2集合
        /// </summary>
        public List<DCDevice2Unit> DCDevice2Units { get; private set; }

        /// <summary>
        /// 直流供电设备3集合
        /// </summary>
        public List<DCDevice3Unit> DCDevice3Units { get; private set; }

        public void Initalize(SubsystemInitParam initParam)
        {
            InitParam = initParam;
            IndexDescription =
                initParam.DataPackage.Config.IndexDescriptionConfig
                    .GetProjectIndexDescriptionConfig(new CommunicationDataKey(initParam.AppConfig));

            var Priority = DataSerialization.DeserializeFromXmlFile<PriorityConfig>(Path.Combine(initParam.AppConfig.AppPaths.ConfigDirectory, PriorityConfig.File));
            InitParam.DataPackage.ServiceManager.RegistService<DoorPriorityService>(new DoorPriorityService(Priority.DoorPriorities));
            InitParam.DataPackage.ServiceManager.RegistService<PantographPriorityService>(new PantographPriorityService(Priority.PantographPriorities));
            InitParam.DataPackage.ServiceManager.RegistService<LCBPriorityService>(new LCBPriorityService(Priority.LCBPriorities));
            InitParam.DataPackage.ServiceManager.RegistService<QuickBreakPriorityService>(new QuickBreakPriorityService(Priority.QuickBreakPriorities));
            InitParam.DataPackage.ServiceManager.RegistService<GroundingPriorityService>(new GroundingPriorityService(Priority.GroundingPriorities));
            InitParam.DataPackage.ServiceManager.RegistService<LCMPriorityService>(new LCMPriorityService(Priority.LCMPriorities));
            InitParam.DataPackage.ServiceManager.RegistService<ACMPriorityService>(new ACMPriorityService(Priority.ACMPriorities));
            InitParam.DataPackage.ServiceManager.RegistService<MCMPriorityService>(new MCMPriorityService(Priority.MCMPriorities));
            InitParam.DataPackage.ServiceManager.RegistService<BCUPriorityService>(new BCUPriorityService(Priority.BCUPriorities));
            InitParam.DataPackage.ServiceManager.RegistService<EmergencyBreakPriorityService>(new EmergencyBreakPriorityService(Priority.EmergencyBreakPriorities));
            InitParam.DataPackage.ServiceManager.RegistService<ParkBreakPriorityService>(new ParkBreakPriorityService(Priority.ParkBreakPriorities));
            InitParam.DataPackage.ServiceManager.RegistService<CarComfortPriorityService>(new CarComfortPriorityService(Priority.CarComfortPriorities));
            InitParam.DataPackage.ServiceManager.RegistService<PrimaryCompressorPriorityService>(new PrimaryCompressorPriorityService(Priority.PrimaryCompressorPriorities));
            InitParam.DataPackage.ServiceManager.RegistService<SubCompressorPriorityService>(new SubCompressorPriorityService(Priority.SubCompressorPriorities));
            InitParam.DataPackage.ServiceManager.RegistService<PressureSensorPriorityService>(new PressureSensorPriorityService(Priority.PressureSensorPriorities));
            InitParam.DataPackage.ServiceManager.RegistService<StationPriorityService>(new StationPriorityService(Priority.StationPriorities));
            InitParam.DataPackage.ServiceManager.RegistService<DCDevice1PriorityService>(new DCDevice1PriorityService(Priority.DCDevice1Priorities));
            InitParam.DataPackage.ServiceManager.RegistService<DCDevice2PriorityService>(new DCDevice2PriorityService(Priority.DCDevice2Priorities));
            InitParam.DataPackage.ServiceManager.RegistService<DCDevice3PriorityService>(new DCDevice3PriorityService(Priority.DCDevice3Priorities));

            Initalize(initParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory,
                initParam.AppConfig.AppPaths.ConfigDirectory);
        }

        public void Initalize(string rootConfigPath, string appConfigPath)
        {
            StateInterfaceCollection = ExcelParser.Parser<StateInterfaceItem>(appConfigPath).ToList();
            BreakTestInfos = ExcelParser.Parser<BreakTestInfo>(appConfigPath).ToList();
            HandleTestInfos = ExcelParser.Parser<HandleTestInfo>(appConfigPath).ToList();
            
            DoorUnits = ExcelParser.Parser<DoorUnit>(appConfigPath).ToList();
            PantographUnits = ExcelParser.Parser<PantographUnit>(appConfigPath).ToList();
            LCBUnits = ExcelParser.Parser<LCBUnit>(appConfigPath).ToList();
            QuickBreakUnits = ExcelParser.Parser<QuickBreakUnit>(appConfigPath).ToList();
            GroundingUnits = ExcelParser.Parser<GroundingUnit>(appConfigPath).ToList();
            LCMUnits = ExcelParser.Parser<LCMUnit>(appConfigPath).ToList();
            ACMUnits = ExcelParser.Parser<ACMUnit>(appConfigPath).ToList();
            MCMUnits = ExcelParser.Parser<MCMUnit>(appConfigPath).ToList();
            BCUUnits = ExcelParser.Parser<BCUUnit>(appConfigPath).ToList();
            EmergencyBreakUnits = ExcelParser.Parser<EmergencyBreakUnit>(appConfigPath).ToList();
            ParkBreakUnits = ExcelParser.Parser<ParkBreakUnit>(appConfigPath).ToList();
            CarComfortUnits = ExcelParser.Parser<CarComfortUnit>(appConfigPath).ToList();
            PrimaryCompressorUnits = ExcelParser.Parser<PrimaryCompressorUnit>(appConfigPath).ToList();
            SubCompressorUnits = ExcelParser.Parser<SubCompressorUnit>(appConfigPath).ToList();
            PressureSensorUnits = ExcelParser.Parser<PressureSensorUnit>(appConfigPath).ToList();
            StationUnits = ExcelParser.Parser<StationUnit>(appConfigPath).ToList();
            DCDevice1Units = ExcelParser.Parser<DCDevice1Unit>(appConfigPath).ToList();
            DCDevice2Units = ExcelParser.Parser<DCDevice2Unit>(appConfigPath).ToList();
            DCDevice3Units = ExcelParser.Parser<DCDevice3Unit>(appConfigPath).ToList();
        }
    }
}