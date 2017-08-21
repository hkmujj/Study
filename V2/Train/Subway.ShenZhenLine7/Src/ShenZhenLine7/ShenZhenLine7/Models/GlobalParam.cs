using System.Collections.Generic;
using System.IO;
using CommonUtil.Util;
using Excel.Interface;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using Subway.ShenZhenLine7.Config;
using Subway.ShenZhenLine7.Controller.BtnResponse;
using Subway.ShenZhenLine7.Models.Units;
using Subway.ShenZhenLine7.Service;
using Subway.ShenZhenLine7.Views.Car;
using Subway.ShenZhenLine7.Views.MainContent;
using Subway.ShenZhenLine7.Views.Root;
using Subway.ShenZhenLine7.Views.SubSystem;

namespace Subway.ShenZhenLine7.Models
{
    /// <summary>
    /// 全局变量
    /// </summary>
    public class GlobalParam
    {
        static GlobalParam()
        {
            Instance = new GlobalParam();
        }
        /// <summary>
        /// 单例
        /// </summary>
        public static GlobalParam Instance { get; private set; }
        /// <summary>
        ///     子系统配置
        /// </summary>
        public SubsystemInitParam InitParam { get; private set; }
        /// <summary>
        ///     接口索引
        /// </summary>
        public IProjectIndexDescriptionConfig IndexConfig { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public BtnConfig StateConfigs { get; private set; }
        /// <summary>
        /// 门状态集合
        /// </summary>
        public IEnumerable<DoorUnit> DoorUnits { get; private set; }
        /// <summary>
        /// 空调
        /// </summary>
        public IEnumerable<AirConditionUnit> AirConditionUnits { get; private set; }
        /// <summary>
        /// 辅助电源
        /// </summary>
        public IEnumerable<AssistUnit> AssistUnits { get; private set; }
        /// <summary>
        /// 紧急对讲
        /// </summary>
        public IEnumerable<EnmergencyTalkUnit> EnmergencyTalkUnits { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<BrakeUnit> BrakeUnits { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public EnmergencyBorderCastConfig BorderCastConfig { get; private set; }
        /// <summary>
        /// 所有旁路单元
        /// </summary>
        public IEnumerable<BypassUnit> AllBypassUnits { get; private set; }
        /// <summary>
        ///     初始化
        /// </summary>
        /// <param name="rootConfig">系统配置目录</param>
        /// <param name="appConfig">应用程序目录</param>
        public void Initialize(string rootConfig, string appConfig)
        {
            RegistPriorityService(appConfig);
            BorderCastConfig = DataSerialization.DeserializeFromXmlFile<EnmergencyBorderCastConfig>(Path.Combine(appConfig, EnmergencyBorderCastConfig.File));
            StateConfigs = DataSerialization.DeserializeFromXmlFile<BtnConfig>(Path.Combine(rootConfig, BtnConfig.File));
            DoorUnits = ExcelParser.Parser<DoorUnit>(appConfig);
            AirConditionUnits = ExcelParser.Parser<AirConditionUnit>(appConfig);
            AssistUnits = ExcelParser.Parser<AssistUnit>(appConfig);
            EnmergencyTalkUnits = ExcelParser.Parser<EnmergencyTalkUnit>(appConfig);
            BrakeUnits = ExcelParser.Parser<BrakeUnit>(appConfig);
            AllBypassUnits = ExcelParser.Parser<BypassUnit>(appConfig);

        }

        private void RegistPriorityService(string app)
        {
            var tmp = DataSerialization.DeserializeFromXmlFile<PriorityConfig>(Path.Combine(app, PriorityConfig.File));
            if (InitParam != null)
            {
                var serviceManager = InitParam.DataPackage.ServiceManager;
                serviceManager.RegistService<DoorPriorityService>(new DoorPriorityService(tmp.DoorPriorities));
                serviceManager.RegistService<AirConditionPriorityService>(new AirConditionPriorityService(tmp.AirConditionPriorities));
                serviceManager.RegistService<AssistPriorityService>(new AssistPriorityService(tmp.AssistPriorities));
                serviceManager.RegistService<EmergencyTalkPriorityService>(new EmergencyTalkPriorityService(tmp.EmergencyTalkPriorities));
                serviceManager.RegistService<BrakePriorityService>(new BrakePriorityService(tmp.BrakePriorities));
                var eventinfo = ExcelParser.Parser<EventInfo>(app);
                var service = new EventManagerService(eventinfo, 10);
                serviceManager.RegistService<EventManagerService>(service);
                var station = ExcelParser.Parser<Station>(app);
                serviceManager.RegistService<StationManagerService>(new StationManagerService(station));
            }
        }

        private void BorderSericalize(string app)
        {
            var tmp = new EnmergencyBorderCastConfig
            {
                BorderCastItems = new List<BorderCastItem>
                {
                    new BorderCastItem() {Index = 1, Name = "临时停车"},
                    new BorderCastItem() {Index = 2, Name = "下一站退出服务"},
                    new BorderCastItem() {Index = 3, Name = "本站退出服务"},
                    new BorderCastItem() {Index = 4, Name = "列车重启"},
                    new BorderCastItem() {Index = 5, Name = "前端疏散"},
                    new BorderCastItem() {Index = 6, Name = "后端疏散"},
                    new BorderCastItem() {Index = 7, Name = "限速运行"},
                    new BorderCastItem() {Index = 8, Name = "两端疏散"},
                    new BorderCastItem() {Index = 9, Name = "反方向运行"},
                    new BorderCastItem() {Index = 10, Name = "列车火灾"},
                    new BorderCastItem() {Index = 11, Name = "临时停车"}
                }
            };
            DataSerialization.SerializeToXmlFile(tmp, Path.Combine(app, EnmergencyBorderCastConfig.File));
        }
        /// <summary>
        ///     初始化
        /// </summary>
        /// <param name="initParam">子系统配置参数</param>
        public void Initialize(SubsystemInitParam initParam)
        {
            InitParam = initParam;
            //获取当前应用程序的逻辑描述
            IndexConfig = InitParam.DataPackage.Config.IndexDescriptionConfig.GetProjectIndexDescriptionConfig(new CommunicationDataKey(initParam.AppConfig));
            //初始化
            Initialize(initParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory, initParam.AppConfig.AppPaths.ConfigDirectory);
        }
    }
}