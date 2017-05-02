using System.Collections.Generic;
using System.IO;
using CommonUtil.Util;
using Excel.Interface;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using Subway.WuHanLine6.Configs;
using Subway.WuHanLine6.FaultInfos;
using Subway.WuHanLine6.Models.Units;

namespace Subway.WuHanLine6.Models
{
    /// <summary>
    ///     全局变量
    /// </summary>
    public class GlobalParams
    {
        static GlobalParams()
        {
            Instance = new GlobalParams();
        }

        /// <summary>
        ///     GlobalParams 静态访问类
        /// </summary>
        public static GlobalParams Instance { get; private set; }

        /// <summary>
        ///     子系统配置
        /// </summary>
        public SubsystemInitParam InitParam { get; private set; }

        /// <summary>
        /// </summary>
        public IEnumerable<StateInterfaceUnit> StateInterfaceUnits { get; private set; }

        /// <summary>
        ///     接口索引
        /// </summary>
        public IProjectIndexDescriptionConfig IndexConfig { get; private set; }

        /// <summary>
        ///     制动单元
        /// </summary>
        public IEnumerable<BrakeUnit> BrakeUnits { get; private set; }

        /// <summary>
        ///     空调单元
        /// </summary>
        public IEnumerable<AirConditionUnit> AirConditionUnits { get; private set; }

        /// <summary>
        ///     门单元
        /// </summary>
        public IEnumerable<DoorUnit> DoorUnits { get; private set; }

        /// <summary>
        ///     打开状态集合
        ///     打开状态集合
        /// </summary>
        public IEnumerable<ChanceUnit> ChanceUnits { get; private set; }

        /// <summary>
        /// 运行模式
        /// </summary>
        public IEnumerable<RunModelUnit> RunModelUnits { get; private set; }

        /// <summary>
        /// 牵引封锁单元
        /// </summary>
        public IEnumerable<TractionLockUnit> TractionLockUnits { get; private set; }

        /// <summary>
        /// 紧急广播单元
        /// </summary>
        public IEnumerable<EmergencyBordercastUnit> EmergencyBordercastUnits { get; private set; }

        /// <summary>
        /// 网络拓扑单元
        /// </summary>
        public IEnumerable<NetWorkUnit> NetWorkUnits { get; private set; }

        /// <summary>
        /// 故障信息
        /// </summary>
        public IEnumerable<FaultInfo> FaultInfos { get; private set; }

        /// <summary>
        /// 站点名称
        /// </summary>
        public IEnumerable<StationInfo> StationInfos { get; private set; }

        /// <summary>
        /// 系统配置目录
        /// </summary>
        public string AppConfigPath { get; private set; }
        /// <summary>
        /// 门优先级配置
        /// </summary>
        public DoorPriorityConfig DoorPriorityConfig { get; set; }
      
        /// <summary>
        ///     初始化
        /// </summary>
        /// <param name="rootConfig">系统配置目录</param>
        /// <param name="appConfig">应用程序目录</param>
        public void Initialize(string rootConfig, string appConfig)
        {
           

            DoorPriorityConfig = DataSerialization.DeserializeFromXmlFile<DoorPriorityConfig>(Path.Combine(appConfig, DoorPriorityConfig.File));
            AppConfigPath = appConfig;
            StateInterfaceUnits = ExcelParser.Parser<StateInterfaceUnit>(appConfig);
            BrakeUnits = ExcelParser.Parser<BrakeUnit>(appConfig);
            AirConditionUnits = ExcelParser.Parser<AirConditionUnit>(appConfig);
            DoorUnits = ExcelParser.Parser<DoorUnit>(appConfig);
            ChanceUnits = ExcelParser.Parser<ChanceUnit>(appConfig);
            RunModelUnits = ExcelParser.Parser<RunModelUnit>(appConfig);
            TractionLockUnits = ExcelParser.Parser<TractionLockUnit>(appConfig);
            EmergencyBordercastUnits = ExcelParser.Parser<EmergencyBordercastUnit>(appConfig);
            NetWorkUnits = ExcelParser.Parser<NetWorkUnit>(appConfig);
            FaultInfos = ExcelParser.Parser<FaultInfo>(appConfig);
            StationInfos = ExcelParser.Parser<StationInfo>(appConfig);
            ServiceLocator.Current.GetInstance<FaultInfoManager>().Initialize(FaultInfos);
            ServiceLocator.Current.GetInstance<StationManager>().Initialize(StationInfos);
        }

        /// <summary>
        ///     初始化
        /// </summary>
        /// <param name="initParam">子系统配置参数</param>
        public void Initialize(SubsystemInitParam initParam)
        {
            InitParam = initParam;
            //获取当前应用程序的逻辑描述
            IndexConfig =
                InitParam.DataPackage.Config.IndexDescriptionConfig.GetProjectIndexDescriptionConfig(
                    new CommunicationDataKey(initParam.AppConfig));
            //初始化
            Initialize(initParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory,
                initParam.AppConfig.AppPaths.ConfigDirectory);
        }
    }
}