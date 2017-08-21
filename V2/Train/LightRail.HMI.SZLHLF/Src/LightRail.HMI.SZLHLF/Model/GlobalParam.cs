using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using CommonUtil.Util;
using LightRail.HMI.SZLHLF.Model.ConfigModel;
using Excel.Interface;
using LightRail.HMI.SZLHLF.Model.Units;
using LightRail.HMI.SZLHLF.Service;
using LightRail.HMI.SZLHLF.Config;
using LightRail.HMI.SZLHLF.Interface;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace LightRail.HMI.SZLHLF.Model
{
    public class GlobalParam
    {
        public static readonly GlobalParam Instance = new GlobalParam();

        public SubsystemInitParam InitParam { private set; get; }

        public string DesignName { get { return typeof(SZLHLFTCMSSubystem).Namespace; } }

        public IProjectIndexDescriptionConfig IndexDescription { private set; get; }

        public ReadOnlyCollection<StateInterfaceItem> StateInterfaceCollection { get; private set; }

        /// <summary>
        /// 车站集合
        /// </summary>
        public IEnumerable<StationItem> StationItem { get; private set; }

        /// <summary>
        /// 门集合
        /// </summary>
        public IEnumerable<DoorUnit> DoorUnits { get; private set; }

        /// <summary>
        /// 电池单元集合
        /// </summary>
        public IEnumerable<BatteryUnit> BatteryUnits { get; private set; }

        /// <summary>
        /// 制动单元集合
        /// </summary>
        public IEnumerable<BrakeUnit> BrakeUnits { get; private set; }

        /// <summary>
        /// 牵引单元集合
        /// </summary>
        public IEnumerable<TractionUnit> TractionUnits { get; private set; }

        /// <summary>
        /// 事件信息表
        /// </summary>
        public IEnumerable<EventInfo> EventInfoItems { get; private set; }

        /// <summary>
        /// 紧急对讲单元集合
        /// </summary>
        public IEnumerable<EnmergencyTalkUnit> EnmergencyTalkUnits { get; private set; }

        /// <summary>
        /// 紧急广播表
        /// </summary>
        public IEnumerable<EmergencyBroadcastItem> EmergencyBroadcastItems { get; private set; }

        /// <summary>
        /// 空调集合
        /// </summary>
        public IEnumerable<AirConditionUnit> AirConditionUnits { get; private set; }

        /// <summary>
        /// 空调模式设定集合
        /// </summary>
        public List<AirModelSetUnit> AirModelSetUnit { get; private set; }

        /// <summary>
        /// 风速设定集合
        /// </summary>
        public List<WindSpeedSetUnit> WindSpeedSetUnit { get; private set; }

        /// <summary>
        /// 新风量设定集合
        /// </summary>
        public List<NewAirValueSetUnit> NewAirValueSetUnit { get; private set; }

        /// <summary>
        /// 线路选择集合
        /// </summary>
        public List<RoadLineSetUnit> RoadLineSetUnit { get; private set; }

        /// <summary>
        /// 报站控制集合
        /// </summary>
        public List<StationBroadcasModeltSetUnit> StationBroadcasModeltSetUnit { get; private set; }

        /// <summary>
        /// 网络拓扑单元
        /// </summary>
        public List<NetWorkUnit> NetWorkUnits { get; private set; }

        public void Initalize(SubsystemInitParam initParam)
        {
            InitParam = initParam;
            IndexDescription =
                initParam.DataPackage.Config.IndexDescriptionConfig
                    .GetProjectIndexDescriptionConfig(new CommunicationDataKey(initParam.AppConfig));
            Initalize(initParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory,
                initParam.AppConfig.AppPaths.ConfigDirectory);

            InitParam.DataPackage.ServiceManager.RegistService<StationManagerService>(new StationManagerService(StationItem));

            var Priority = DataSerialization.DeserializeFromXmlFile<PriorityConfig>(Path.Combine(initParam.AppConfig.AppPaths.ConfigDirectory, PriorityConfig.File));
            InitParam.DataPackage.ServiceManager.RegistService<DoorPriorityService>(new DoorPriorityService(Priority.DoorPriorities));
            InitParam.DataPackage.ServiceManager.RegistService<BatteryPriorityService>(new BatteryPriorityService(Priority.BatteryPriorities));
            InitParam.DataPackage.ServiceManager.RegistService<BrakePriorityService>(new BrakePriorityService(Priority.BrakePriorities));
            InitParam.DataPackage.ServiceManager.RegistService<TractionPriorityService>(new TractionPriorityService(Priority.TractionPriorities));
            InitParam.DataPackage.ServiceManager.RegistService<EmergencyTalkPriorityService>(new EmergencyTalkPriorityService(Priority.EmergencyTalkPriorities));
            InitParam.DataPackage.ServiceManager.RegistService<AirConditionPriorityService>(new AirConditionPriorityService(Priority.AirConditionPriorities));

            InitParam.DataPackage.ServiceManager.RegistService<EventManagerService>(new EventManagerService(EventInfoItems, 10));

        }

        public void Initalize(string rootConfigPath, string appConfigPath)
        {
            StationItem = ExcelParser.Parser<StationItem>(appConfigPath);

            StateInterfaceCollection = ExcelParser.Parser<StateInterfaceItem>(appConfigPath).ToList().AsReadOnly();

            DoorUnits = ExcelParser.Parser<DoorUnit>(appConfigPath);
            BatteryUnits = ExcelParser.Parser<BatteryUnit>(appConfigPath);
            BrakeUnits = ExcelParser.Parser<BrakeUnit>(appConfigPath);
            TractionUnits = ExcelParser.Parser<TractionUnit>(appConfigPath);
            EnmergencyTalkUnits = ExcelParser.Parser<EnmergencyTalkUnit>(appConfigPath);
            AirConditionUnits = ExcelParser.Parser<AirConditionUnit>(appConfigPath);

            EmergencyBroadcastItems = ExcelParser.Parser<EmergencyBroadcastItem>(appConfigPath);

            EventInfoItems = ExcelParser.Parser<EventInfo>(appConfigPath);

            AirModelSetUnit = ExcelParser.Parser<AirModelSetUnit>(appConfigPath).ToList();
            WindSpeedSetUnit = ExcelParser.Parser<WindSpeedSetUnit>(appConfigPath).ToList();
            NewAirValueSetUnit = ExcelParser.Parser<NewAirValueSetUnit>(appConfigPath).ToList();
            RoadLineSetUnit = ExcelParser.Parser<RoadLineSetUnit>(appConfigPath).ToList();
            StationBroadcasModeltSetUnit = ExcelParser.Parser<StationBroadcasModeltSetUnit>(appConfigPath).ToList();
            NetWorkUnits = ExcelParser.Parser<NetWorkUnit>(appConfigPath).ToList();
        }
    }
}