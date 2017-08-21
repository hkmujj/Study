using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using CommonUtil.Util;
using Excel.Interface;
using LightRail.HMI.GZYGDC.Config;
using LightRail.HMI.GZYGDC.Model.ConfigModel;
using LightRail.HMI.GZYGDC.Model.Units;
using LightRail.HMI.GZYGDC.Service;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace LightRail.HMI.GZYGDC.Model
{
    public class GlobalParam
    {
        public static readonly GlobalParam Instance = new GlobalParam();

        public SubsystemInitParam InitParam { private set; get; }

        public string DesignName { get { return typeof(GZYGDCSSubystem).Namespace; } }

        public IProjectIndexDescriptionConfig IndexDescription { private set; get; }

        ////中间按键
        //public ReadOnlyCollection<StateInterfaceItemCenter> CenterStateInterfaceCollection { get; private set; }

        ////底部按键
        //public ReadOnlyCollection<StateInterfaceItemBottom> BottomStateInterfaceCollection { get; private set; }

        //中间按键
        public List<StateInterfaceItem> CenterStateInterfaceCollection { get; private set; }

        //底部按键
        public List<StateInterfaceItem> BottomStateInterfaceCollection { get; private set; }


        /// <summary>
        /// 门集合
        /// </summary>
        public IEnumerable<DoorUnit> DoorUnits { get; private set; }


        /// <summary>
        /// 空调集合
        /// </summary>
        public IEnumerable<AirConditionUnit> AirConditionUnits { get; private set; }


        /// <summary>
        /// 辅助电源集合
        /// </summary>
        public IEnumerable<AssistUnit> AssistUnits { get; private set; }


        /// <summary>
        /// 紧急对讲单元集合
        /// </summary>
        public IEnumerable<EnmergencyTalkUnit> EnmergencyTalkUnits { get; private set; }


        /// <summary>
        /// 制动单元集合
        /// </summary>
        public IEnumerable<BrakeUnit> BrakeUnits { get; private set; }


        /// <summary>
        /// 牵引单元集合
        /// </summary>
        public IEnumerable<TractionUnit> TractionUnits { get; private set; }


        /// <summary>
        /// 受电弓单元集合
        /// </summary>
        public IEnumerable<PantographUnit> PantographUnits { get; private set; }


        /// <summary>
        /// HSCB高速断路器单元集合
        /// </summary>
        public IEnumerable<HSCBUnit> HSCBUnits { get; private set; }


        /// <summary>
        /// 电池单元集合
        /// </summary>
        public IEnumerable<BatteryUnit> BatteryUnits { get; private set; }

        /// <summary>
        /// 弹簧单元集合
        /// </summary>

        public IEnumerable<SpringUnit> SpringUnits { get; private set; }


        /// <summary>
        /// 紧急广播表
        /// </summary>

        public IEnumerable<EmergencyBroadcastItem> EmergencyBroadcastItems { get; private set; }


        /// <summary>
        /// 事件信息表
        /// </summary>

        public IEnumerable<EventInfo> EventInfoItems { get; private set; }





        public void Initalize(SubsystemInitParam initParam)
        {
            InitParam = initParam;

            IndexDescription =
                initParam.DataPackage.Config.IndexDescriptionConfig
                    .GetProjectIndexDescriptionConfig(new CommunicationDataKey(initParam.AppConfig));

            Initalize(initParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory,
                initParam.AppConfig.AppPaths.ConfigDirectory);

            var stations = ExcelParser.Parser<StationItem>(initParam.AppConfig.AppPaths.ConfigDirectory);

            InitParam.DataPackage.ServiceManager.RegistService<StationManagerService>(new StationManagerService(stations));

            var Priority = DataSerialization.DeserializeFromXmlFile<PriorityConfig>(Path.Combine(initParam.AppConfig.AppPaths.ConfigDirectory, PriorityConfig.File));

            InitParam.DataPackage.ServiceManager.RegistService<DoorPriorityService>(new DoorPriorityService(Priority.DoorPriorities));
            InitParam.DataPackage.ServiceManager.RegistService<AirConditionPriorityService>(new AirConditionPriorityService(Priority.AirConditionPriorities));
            InitParam.DataPackage.ServiceManager.RegistService<AssistPriorityService>(new AssistPriorityService(Priority.AssistPriorities));
            InitParam.DataPackage.ServiceManager.RegistService<EmergencyTalkPriorityService>(new EmergencyTalkPriorityService(Priority.EmergencyTalkPriorities));
            InitParam.DataPackage.ServiceManager.RegistService<BrakePriorityService>(new BrakePriorityService(Priority.BrakePriorities));
            InitParam.DataPackage.ServiceManager.RegistService<TractionPriorityService>(new TractionPriorityService(Priority.TractionPriorities));
            InitParam.DataPackage.ServiceManager.RegistService<PantographPriorityService>(new PantographPriorityService(Priority.PantographPriorities));
            InitParam.DataPackage.ServiceManager.RegistService<HSCBPriorityService>(new HSCBPriorityService(Priority.HSCBPriorities));
            InitParam.DataPackage.ServiceManager.RegistService<BatteryPriorityService>(new BatteryPriorityService(Priority.BatteryPriorities));
            InitParam.DataPackage.ServiceManager.RegistService<SpringPriorityService>(new SpringPriorityService(Priority.SpringPriorities));

            InitParam.DataPackage.ServiceManager.RegistService<EventManagerService>(new EventManagerService(EventInfoItems, 10));

        }

        public void Initalize(string rootConfigPath, string appConfigPath)
        {
            //CenterStateInterfaceCollection = ExcelParser.Parser<StateInterfaceItemCenter>(appConfigPath).ToList().AsReadOnly();
            //BottomStateInterfaceCollection = ExcelParser.Parser<StateInterfaceItemBottom>(appConfigPath).ToList().AsReadOnly();

            List<StateInterfaceItemCenter> CenterCollection = ExcelParser.Parser<StateInterfaceItemCenter>(appConfigPath).ToList();
            List<StateInterfaceItemBottom> BottomCollection = ExcelParser.Parser<StateInterfaceItemBottom>(appConfigPath).ToList();

          

            CenterStateInterfaceCollection = new List<StateInterfaceItem>();
            BottomStateInterfaceCollection = new List<StateInterfaceItem>();

            for (int i = 0; i < CenterCollection.Count; i++)
            {
               CenterStateInterfaceCollection.Add(new StateInterfaceItem(CenterCollection[i]));
            }

            for (int i = 0; i < BottomCollection.Count; i++)
            {
                BottomStateInterfaceCollection.Add(new StateInterfaceItem(BottomCollection[i]));
            }

            DoorUnits = ExcelParser.Parser<DoorUnit>(appConfigPath);
            AirConditionUnits = ExcelParser.Parser<AirConditionUnit>(appConfigPath);
            AssistUnits = ExcelParser.Parser<AssistUnit>(appConfigPath);
            EnmergencyTalkUnits = ExcelParser.Parser<EnmergencyTalkUnit>(appConfigPath);
            BrakeUnits = ExcelParser.Parser<BrakeUnit>(appConfigPath);
            TractionUnits = ExcelParser.Parser<TractionUnit>(appConfigPath);
            PantographUnits = ExcelParser.Parser<PantographUnit>(appConfigPath);
            HSCBUnits = ExcelParser.Parser<HSCBUnit>(appConfigPath);
            BatteryUnits = ExcelParser.Parser<BatteryUnit>(appConfigPath);
            SpringUnits = ExcelParser.Parser<SpringUnit>(appConfigPath);
            EmergencyBroadcastItems = ExcelParser.Parser<EmergencyBroadcastItem>(appConfigPath);
            EventInfoItems = ExcelParser.Parser<EventInfo>(appConfigPath);
        }
    }
}