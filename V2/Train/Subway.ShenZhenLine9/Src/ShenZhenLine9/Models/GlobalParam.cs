using System.Collections.Generic;
using System.IO;
using CommonUtil.Util;
using Excel.Interface;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using Subway.ShenZhenLine9.Config;
using Subway.ShenZhenLine9.Controller.BtnResponse;
using Subway.ShenZhenLine9.Models.Units;
using Subway.ShenZhenLine9.Service;
using Subway.ShenZhenLine9.Views.Car;
using Subway.ShenZhenLine9.Views.MainContent;
using Subway.ShenZhenLine9.Views.Root;
using Subway.ShenZhenLine9.Views.SubSystem;

namespace Subway.ShenZhenLine9.Models
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
        public IEnumerable<LCUUnit> LCUUnit { get; private set; }
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
            //  Serialize(rootConfig, appConfig);
            RegistPriorityService(appConfig);
            BorderCastConfig = DataSerialization.DeserializeFromXmlFile<EnmergencyBorderCastConfig>(Path.Combine(appConfig, EnmergencyBorderCastConfig.File));
            StateConfigs = DataSerialization.DeserializeFromXmlFile<BtnConfig>(Path.Combine(rootConfig, BtnConfig.File));
            DoorUnits = ExcelParser.Parser<DoorUnit>(appConfig);
            AirConditionUnits = ExcelParser.Parser<AirConditionUnit>(appConfig);
            AssistUnits = ExcelParser.Parser<AssistUnit>(appConfig);
            EnmergencyTalkUnits = ExcelParser.Parser<EnmergencyTalkUnit>(appConfig);
            BrakeUnits = ExcelParser.Parser<BrakeUnit>(appConfig);
            AllBypassUnits = ExcelParser.Parser<BypassUnit>(appConfig);
            LCUUnit = ExcelParser.Parser<LCUUnit>(appConfig);
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
        private void Serialize(string root, string app)
        {
            BtnSerialize(root);
            PrioritySerialize(app);
            BorderSericalize(app);

        }

        private void BorderSericalize(string app)
        {
            var tmp = new EnmergencyBorderCastConfig();
            tmp.BorderCastItems = new List<BorderCastItem>();
            tmp.BorderCastItems.Add(new BorderCastItem() { Index = 1, Name = "临时停车" });
            tmp.BorderCastItems.Add(new BorderCastItem() { Index = 2, Name = "下一站退出服务" });
            tmp.BorderCastItems.Add(new BorderCastItem() { Index = 3, Name = "本站退出服务" });
            tmp.BorderCastItems.Add(new BorderCastItem() { Index = 4, Name = "列车重启" });
            tmp.BorderCastItems.Add(new BorderCastItem() { Index = 5, Name = "前端疏散" });
            tmp.BorderCastItems.Add(new BorderCastItem() { Index = 6, Name = "后端疏散" });
            tmp.BorderCastItems.Add(new BorderCastItem() { Index = 7, Name = "限速运行" });
            tmp.BorderCastItems.Add(new BorderCastItem() { Index = 8, Name = "两端疏散" });
            tmp.BorderCastItems.Add(new BorderCastItem() { Index = 9, Name = "反方向运行" });
            tmp.BorderCastItems.Add(new BorderCastItem() { Index = 10, Name = "列车火灾" });
            tmp.BorderCastItems.Add(new BorderCastItem() { Index = 11, Name = "临时停车" });
            DataSerialization.SerializeToXmlFile(tmp, Path.Combine(app, EnmergencyBorderCastConfig.File));
        }

        private static void PrioritySerialize(string app)
        {
            var confi = new PriorityConfig
            {
                DoorPriorities = new List<Priority<DoorState, int>>
                {
                    new Priority<DoorState,int>() {Key = DoorState.EmergencyLock, Prioritys = 1},
                    new Priority<DoorState,int>() {Key = DoorState.Cut, Prioritys = 2},
                    new Priority<DoorState,int>() {Key = DoorState.Fault, Prioritys = 3},
                    new Priority<DoorState,int>() {Key = DoorState.Check, Prioritys = 4},
                    new Priority<DoorState,int>() {Key = DoorState.Flicker, Prioritys = 5},
                    new Priority<DoorState,int>() {Key = DoorState.Closed, Prioritys = 6},
                },
                AirConditionPriorities = new List<Priority<AirConditionState, int>>
                {
                    new Priority<AirConditionState, int>() {Key = AirConditionState.Fault,Prioritys = 1},
                    new Priority<AirConditionState, int>() {Key = AirConditionState.Run,Prioritys = 2},
                    new Priority<AirConditionState, int>() {Key = AirConditionState.Off,Prioritys = 3},
                },
                AssistPriorities = new List<Priority<AssistState, int>>()
                {
                    new Priority<AssistState, int>() {Key = AssistState.ACFault,Prioritys = 1},
                    new Priority<AssistState, int>() {Key = AssistState.DCFault,Prioritys = 1},
                    new Priority<AssistState, int>() {Key = AssistState.ACRun,Prioritys = 2},
                    new Priority<AssistState, int>() {Key = AssistState.DCRun,Prioritys = 2},
                    new Priority<AssistState, int>() {Key = AssistState.ACOff,Prioritys = 3},
                    new Priority<AssistState, int>() {Key = AssistState.DCOff,Prioritys = 3},
                },
                EmergencyTalkPriorities = new List<Priority<EmergencyTalkState, int>>()
                {
                    new Priority<EmergencyTalkState, int>() {Key = EmergencyTalkState.Fault,Prioritys = 1},
                    new Priority<EmergencyTalkState, int>() {Key = EmergencyTalkState.Active,Prioritys = 2},
                    new Priority<EmergencyTalkState, int>() {Key = EmergencyTalkState.Talk,Prioritys = 3},
                    new Priority<EmergencyTalkState, int>() {Key = EmergencyTalkState.UnActive,Prioritys = 4},
                },
                BrakePriorities = new List<Priority<BrakeState, int>>()
                {
                    new Priority<BrakeState, int>() {Key = BrakeState.BrakeCut,Prioritys = 1},
                    new Priority<BrakeState, int>() {Key = BrakeState.BrakeFault,Prioritys = 2},
                    new Priority<BrakeState, int>() {Key = BrakeState.BrakeUnRemit,Prioritys = 3},
                    new Priority<BrakeState, int>() {Key = BrakeState.BrakeRemit,Prioritys = 4},
                    new Priority<BrakeState, int>() {Key = BrakeState.ParkingUnRemit,Prioritys = 5},
                    new Priority<BrakeState, int>() {Key = BrakeState.ParkingRemit,Prioritys = 6},
                    new Priority<BrakeState, int>() {Key = BrakeState.Noaml,Prioritys = 7}
                }

            };

            DataSerialization.SerializeToXmlFile(confi, Path.Combine(app, PriorityConfig.File));
        }

        private static void BtnSerialize(string root)
        {
            var s = new BtnConfig();
            s.AllSatteConfig = new List<StateConfig>();
            var s0 = new StateConfig
            {
                Key = "启动界面",
                Btn01 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn02 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn03 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn04 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn05 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn06 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn07 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn08 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn09 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn10 = new BtnItemConfig { ResponseName = "", Text = "" },
                View = typeof(StartView).FullName
            };
            var s1 = new StateConfig
            {
                Key = "主页面",
                Btn01 = new BtnItemConfig { ResponseName = nameof(EventInfoResponse), Text = "事件信息" },
                Btn02 = new BtnItemConfig { ResponseName = nameof(SettingResponse), Text = "设置" },
                Btn03 = new BtnItemConfig { ResponseName = nameof(MaininstanceResponse), Text = "维护" },
                Btn04 = new BtnItemConfig { ResponseName = nameof(BypassResponse), Text = "旁路信息" },
                Btn05 = new BtnItemConfig { ResponseName = nameof(TractionLockResponse), Text = "牵引封锁" },
                Btn06 = new BtnItemConfig { ResponseName = nameof(BorderCastResponse), Text = "广播" },
                Btn07 = new BtnItemConfig { ResponseName = nameof(HelpResponse), Text = "帮助" },
                Btn08 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn09 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn10 = new BtnItemConfig { ResponseName = nameof(MasterResponse), Text = "主页" },
                View = typeof(DoorViewCar).FullName,
                TitleName = "门状态"
            };
            var s2 = new StateConfig
            {
                Key = "黑屏",
                Btn01 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn02 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn03 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn04 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn05 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn06 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn07 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn08 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn09 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn10 = new BtnItemConfig { ResponseName = "", Text = "" },
                View = typeof(BlackView).FullName,
                TitleName = ""
            };
            var s3 = new StateConfig
            {
                Key = "空调子页面",
                Btn01 = new BtnItemConfig { ResponseName = nameof(EventInfoResponse), Text = "事件信息" },
                Btn02 = new BtnItemConfig { ResponseName = nameof(SettingResponse), Text = "设置" },
                Btn03 = new BtnItemConfig { ResponseName = nameof(MaininstanceResponse), Text = "维护" },
                Btn04 = new BtnItemConfig { ResponseName = nameof(BypassResponse), Text = "旁路信息" },
                Btn05 = new BtnItemConfig { ResponseName = nameof(TractionLockResponse), Text = "牵引封锁" },
                Btn06 = new BtnItemConfig { ResponseName = nameof(BorderCastResponse), Text = "广播" },
                Btn07 = new BtnItemConfig { ResponseName = nameof(HelpResponse), Text = "帮助" },
                Btn08 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn09 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn10 = new BtnItemConfig { ResponseName = nameof(MasterResponse), Text = "主页" },
                View = typeof(AirConditionView).FullName,
                TitleName = "通风/空调"
            };
            var s4 = new StateConfig
            {
                Key = "辅助电源页面",
                Btn01 = new BtnItemConfig { ResponseName = nameof(EventInfoResponse), Text = "事件信息" },
                Btn02 = new BtnItemConfig { ResponseName = nameof(SettingResponse), Text = "设置" },
                Btn03 = new BtnItemConfig { ResponseName = nameof(MaininstanceResponse), Text = "维护" },
                Btn04 = new BtnItemConfig { ResponseName = nameof(BypassResponse), Text = "旁路信息" },
                Btn05 = new BtnItemConfig { ResponseName = nameof(TractionLockResponse), Text = "牵引封锁" },
                Btn06 = new BtnItemConfig { ResponseName = nameof(BorderCastResponse), Text = "广播" },
                Btn07 = new BtnItemConfig { ResponseName = nameof(HelpResponse), Text = "帮助" },
                Btn08 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn09 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn10 = new BtnItemConfig { ResponseName = nameof(MasterResponse), Text = "主页" },
                View = typeof(AssistView).FullName,
                TitleName = "辅助电源"
            };
            var s5 = new StateConfig
            {
                Key = "紧急对讲页面",
                Btn01 = new BtnItemConfig { ResponseName = nameof(EventInfoResponse), Text = "事件信息" },
                Btn02 = new BtnItemConfig { ResponseName = nameof(SettingResponse), Text = "设置" },
                Btn03 = new BtnItemConfig { ResponseName = nameof(MaininstanceResponse), Text = "维护" },
                Btn04 = new BtnItemConfig { ResponseName = nameof(BypassResponse), Text = "旁路信息" },
                Btn05 = new BtnItemConfig { ResponseName = nameof(TractionLockResponse), Text = "牵引封锁" },
                Btn06 = new BtnItemConfig { ResponseName = nameof(BorderCastResponse), Text = "广播" },
                Btn07 = new BtnItemConfig { ResponseName = nameof(HelpResponse), Text = "帮助" },
                Btn08 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn09 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn10 = new BtnItemConfig { ResponseName = nameof(MasterResponse), Text = "主页" },
                View = typeof(EmergencyTalkView).FullName,
                TitleName = "紧急对讲"
            };
            var s6 = new StateConfig
            {
                Key = "制动页面",
                Btn01 = new BtnItemConfig { ResponseName = nameof(EventInfoResponse), Text = "事件信息" },
                Btn02 = new BtnItemConfig { ResponseName = nameof(SettingResponse), Text = "设置" },
                Btn03 = new BtnItemConfig { ResponseName = nameof(MaininstanceResponse), Text = "维护" },
                Btn04 = new BtnItemConfig { ResponseName = nameof(BypassResponse), Text = "旁路信息" },
                Btn05 = new BtnItemConfig { ResponseName = nameof(TractionLockResponse), Text = "牵引封锁" },
                Btn06 = new BtnItemConfig { ResponseName = nameof(BorderCastResponse), Text = "广播" },
                Btn07 = new BtnItemConfig { ResponseName = nameof(HelpResponse), Text = "帮助" },
                Btn08 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn09 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn10 = new BtnItemConfig { ResponseName = nameof(MasterResponse), Text = "主页" },
                View = typeof(BrakeView).FullName,
                TitleName = "制动状态"
            };
            var s7 = new StateConfig
            {
                Key = "牵引页面",
                Btn01 = new BtnItemConfig { ResponseName = nameof(EventInfoResponse), Text = "事件信息" },
                Btn02 = new BtnItemConfig { ResponseName = nameof(SettingResponse), Text = "设置" },
                Btn03 = new BtnItemConfig { ResponseName = nameof(MaininstanceResponse), Text = "维护" },
                Btn04 = new BtnItemConfig { ResponseName = nameof(BypassResponse), Text = "旁路信息" },
                Btn05 = new BtnItemConfig { ResponseName = nameof(TractionLockResponse), Text = "牵引封锁" },
                Btn06 = new BtnItemConfig { ResponseName = nameof(BorderCastResponse), Text = "广播" },
                Btn07 = new BtnItemConfig { ResponseName = nameof(HelpResponse), Text = "帮助" },
                Btn08 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn09 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn10 = new BtnItemConfig { ResponseName = nameof(MasterResponse), Text = "主页" },
                View = typeof(TractionVIew).FullName,
                TitleName = "牵引状态"
            };

            var s8 = new StateConfig
            {
                Key = "烟火检测",
                Btn01 = new BtnItemConfig { ResponseName = nameof(EventInfoResponse), Text = "事件信息" },
                Btn02 = new BtnItemConfig { ResponseName = nameof(SettingResponse), Text = "设置" },
                Btn03 = new BtnItemConfig { ResponseName = nameof(MaininstanceResponse), Text = "维护" },
                Btn04 = new BtnItemConfig { ResponseName = nameof(BypassResponse), Text = "旁路信息" },
                Btn05 = new BtnItemConfig { ResponseName = nameof(TractionLockResponse), Text = "牵引封锁" },
                Btn06 = new BtnItemConfig { ResponseName = nameof(BorderCastResponse), Text = "广播" },
                Btn07 = new BtnItemConfig { ResponseName = nameof(HelpResponse), Text = "帮助" },
                Btn08 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn09 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn10 = new BtnItemConfig { ResponseName = nameof(MasterResponse), Text = "主页" },
                View = typeof(SmokeView).FullName,
                TitleName = "火/烟检测"
            };

            var s9 = new StateConfig
            {
                Key = "空压机",
                Btn01 = new BtnItemConfig { ResponseName = nameof(EventInfoResponse), Text = "事件信息" },
                Btn02 = new BtnItemConfig { ResponseName = nameof(SettingResponse), Text = "设置" },
                Btn03 = new BtnItemConfig { ResponseName = nameof(MaininstanceResponse), Text = "维护" },
                Btn04 = new BtnItemConfig { ResponseName = nameof(BypassResponse), Text = "旁路信息" },
                Btn05 = new BtnItemConfig { ResponseName = nameof(TractionLockResponse), Text = "牵引封锁" },
                Btn06 = new BtnItemConfig { ResponseName = nameof(BorderCastResponse), Text = "广播" },
                Btn07 = new BtnItemConfig { ResponseName = nameof(HelpResponse), Text = "帮助" },
                Btn08 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn09 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn10 = new BtnItemConfig { ResponseName = nameof(MasterResponse), Text = "主页" },
                View = typeof(AirPumpView).FullName,
                TitleName = "空气压缩机"
            };
            var s10 = new StateConfig
            {
                Key = "HSCB页面",
                Btn01 = new BtnItemConfig { ResponseName = nameof(EventInfoResponse), Text = "事件信息" },
                Btn02 = new BtnItemConfig { ResponseName = nameof(SettingResponse), Text = "设置" },
                Btn03 = new BtnItemConfig { ResponseName = nameof(MaininstanceResponse), Text = "维护" },
                Btn04 = new BtnItemConfig { ResponseName = nameof(BypassResponse), Text = "旁路信息" },
                Btn05 = new BtnItemConfig { ResponseName = nameof(TractionLockResponse), Text = "牵引封锁" },
                Btn06 = new BtnItemConfig { ResponseName = nameof(BorderCastResponse), Text = "广播" },
                Btn07 = new BtnItemConfig { ResponseName = nameof(HelpResponse), Text = "帮助" },
                Btn08 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn09 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn10 = new BtnItemConfig { ResponseName = nameof(MasterResponse), Text = "主页" },
                View = typeof(HSCBView).FullName,
                TitleName = "受电弓/高速断路器"
            };
            var s11 = new StateConfig
            {
                Key = "紧急广播界面",
                Btn01 = new BtnItemConfig { ResponseName = nameof(EventInfoResponse), Text = "事件信息" },
                Btn02 = new BtnItemConfig { ResponseName = nameof(SettingResponse), Text = "设置" },
                Btn03 = new BtnItemConfig { ResponseName = nameof(MaininstanceResponse), Text = "维护" },
                Btn04 = new BtnItemConfig { ResponseName = nameof(BypassResponse), Text = "旁路信息" },
                Btn05 = new BtnItemConfig { ResponseName = nameof(TractionLockResponse), Text = "牵引封锁" },
                Btn06 = new BtnItemConfig { ResponseName = nameof(BorderCastResponse), Text = "广播" },
                Btn07 = new BtnItemConfig { ResponseName = nameof(HelpResponse), Text = "帮助" },
                Btn08 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn09 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn10 = new BtnItemConfig { ResponseName = nameof(MasterResponse), Text = "主页" },
                View = typeof(EmengencyBorderCastView).FullName,
                TitleName = "紧急广播"
            };
            var s12 = new StateConfig
            {
                Key = "事件信息",
                Btn01 = new BtnItemConfig { ResponseName = nameof(EventInfoResponse), Text = "事件信息" },
                Btn02 = new BtnItemConfig { ResponseName = nameof(SettingResponse), Text = "设置" },
                Btn03 = new BtnItemConfig { ResponseName = nameof(MaininstanceResponse), Text = "维护" },
                Btn04 = new BtnItemConfig { ResponseName = nameof(BypassResponse), Text = "旁路信息" },
                Btn05 = new BtnItemConfig { ResponseName = nameof(TractionLockResponse), Text = "牵引封锁" },
                Btn06 = new BtnItemConfig { ResponseName = nameof(BorderCastResponse), Text = "广播" },
                Btn07 = new BtnItemConfig { ResponseName = nameof(HelpResponse), Text = "帮助" },
                Btn08 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn09 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn10 = new BtnItemConfig { ResponseName = nameof(MasterResponse), Text = "主页" },
                View = typeof(EventInfoView).FullName,
                TitleName = "事件信息"
            };
            var s13 = new StateConfig
            {
                Key = "设置",
                Btn01 = new BtnItemConfig { ResponseName = nameof(EventInfoResponse), Text = "事件信息" },
                Btn02 = new BtnItemConfig { ResponseName = nameof(SettingResponse), Text = "设置" },
                Btn03 = new BtnItemConfig { ResponseName = nameof(MaininstanceResponse), Text = "维护" },
                Btn04 = new BtnItemConfig { ResponseName = nameof(BypassResponse), Text = "旁路信息" },
                Btn05 = new BtnItemConfig { ResponseName = nameof(TractionLockResponse), Text = "牵引封锁" },
                Btn06 = new BtnItemConfig { ResponseName = nameof(BorderCastResponse), Text = "广播" },
                Btn07 = new BtnItemConfig { ResponseName = nameof(HelpResponse), Text = "帮助" },
                Btn08 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn09 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn10 = new BtnItemConfig { ResponseName = nameof(MasterResponse), Text = "主页" },
                View = typeof(SettingView).FullName,
                TitleName = "设置"
            };
            var s14 = new StateConfig
            {
                Key = "维护",
                Btn01 = new BtnItemConfig { ResponseName = nameof(EventInfoResponse), Text = "事件信息" },
                Btn02 = new BtnItemConfig { ResponseName = nameof(SettingResponse), Text = "设置" },
                Btn03 = new BtnItemConfig { ResponseName = nameof(MaininstanceResponse), Text = "维护" },
                Btn04 = new BtnItemConfig { ResponseName = nameof(BypassResponse), Text = "旁路信息" },
                Btn05 = new BtnItemConfig { ResponseName = nameof(TractionLockResponse), Text = "牵引封锁" },
                Btn06 = new BtnItemConfig { ResponseName = nameof(BorderCastResponse), Text = "广播" },
                Btn07 = new BtnItemConfig { ResponseName = nameof(HelpResponse), Text = "帮助" },
                Btn08 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn09 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn10 = new BtnItemConfig { ResponseName = nameof(MasterResponse), Text = "主页" },
                View = typeof(MaininstanceView).FullName,
                TitleName = "登录"
            };
            var s15 = new StateConfig
            {
                Key = "旁路信息",
                Btn01 = new BtnItemConfig { ResponseName = nameof(EventInfoResponse), Text = "事件信息" },
                Btn02 = new BtnItemConfig { ResponseName = nameof(SettingResponse), Text = "设置" },
                Btn03 = new BtnItemConfig { ResponseName = nameof(MaininstanceResponse), Text = "维护" },
                Btn04 = new BtnItemConfig { ResponseName = nameof(BypassResponse), Text = "旁路信息" },
                Btn05 = new BtnItemConfig { ResponseName = nameof(TractionLockResponse), Text = "牵引封锁" },
                Btn06 = new BtnItemConfig { ResponseName = nameof(BorderCastResponse), Text = "广播" },
                Btn07 = new BtnItemConfig { ResponseName = nameof(HelpResponse), Text = "帮助" },
                Btn08 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn09 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn10 = new BtnItemConfig { ResponseName = nameof(MasterResponse), Text = "主页" },
                View = typeof(BypassView).FullName,
                TitleName = "旁路信息"
            };
            var s16 = new StateConfig
            {
                Key = "牵引封锁",
                Btn01 = new BtnItemConfig { ResponseName = nameof(EventInfoResponse), Text = "事件信息" },
                Btn02 = new BtnItemConfig { ResponseName = nameof(SettingResponse), Text = "设置" },
                Btn03 = new BtnItemConfig { ResponseName = nameof(MaininstanceResponse), Text = "维护" },
                Btn04 = new BtnItemConfig { ResponseName = nameof(BypassResponse), Text = "旁路信息" },
                Btn05 = new BtnItemConfig { ResponseName = nameof(TractionLockResponse), Text = "牵引封锁" },
                Btn06 = new BtnItemConfig { ResponseName = nameof(BorderCastResponse), Text = "广播" },
                Btn07 = new BtnItemConfig { ResponseName = nameof(HelpResponse), Text = "帮助" },
                Btn08 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn09 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn10 = new BtnItemConfig { ResponseName = nameof(MasterResponse), Text = "主页" },
                View = typeof(TractionLockView).FullName,
                TitleName = "牵引封锁"
            };
            var s17 = new StateConfig
            {
                Key = "广播",
                Btn01 = new BtnItemConfig { ResponseName = nameof(EventInfoResponse), Text = "事件信息" },
                Btn02 = new BtnItemConfig { ResponseName = nameof(SettingResponse), Text = "设置" },
                Btn03 = new BtnItemConfig { ResponseName = nameof(MaininstanceResponse), Text = "维护" },
                Btn04 = new BtnItemConfig { ResponseName = nameof(BypassResponse), Text = "旁路信息" },
                Btn05 = new BtnItemConfig { ResponseName = nameof(TractionLockResponse), Text = "牵引封锁" },
                Btn06 = new BtnItemConfig { ResponseName = nameof(BorderCastResponse), Text = "广播" },
                Btn07 = new BtnItemConfig { ResponseName = nameof(HelpResponse), Text = "帮助" },
                Btn08 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn09 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn10 = new BtnItemConfig { ResponseName = nameof(MasterResponse), Text = "主页" },
                View = typeof(BorderCastView).FullName,
                TitleName = "广播"
            };
            var s18 = new StateConfig
            {
                Key = "帮助",
                Btn01 = new BtnItemConfig { ResponseName = nameof(EventInfoResponse), Text = "事件信息" },
                Btn02 = new BtnItemConfig { ResponseName = nameof(SettingResponse), Text = "设置" },
                Btn03 = new BtnItemConfig { ResponseName = nameof(MaininstanceResponse), Text = "维护" },
                Btn04 = new BtnItemConfig { ResponseName = nameof(BypassResponse), Text = "旁路信息" },
                Btn05 = new BtnItemConfig { ResponseName = nameof(TractionLockResponse), Text = "牵引封锁" },
                Btn06 = new BtnItemConfig { ResponseName = nameof(BorderCastResponse), Text = "广播" },
                Btn07 = new BtnItemConfig { ResponseName = nameof(HelpResponse), Text = "帮助" },
                Btn08 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn09 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn10 = new BtnItemConfig { ResponseName = nameof(MasterResponse), Text = "主页" },
                View = typeof(HelpView).FullName,
                TitleName = "帮助"
            };
            var s19 = new StateConfig
            {
                Key = "事件帮助",
                Btn01 = new BtnItemConfig { ResponseName = nameof(EventInfoResponse), Text = "事件信息" },
                Btn02 = new BtnItemConfig { ResponseName = nameof(SettingResponse), Text = "设置" },
                Btn03 = new BtnItemConfig { ResponseName = nameof(MaininstanceResponse), Text = "维护" },
                Btn04 = new BtnItemConfig { ResponseName = nameof(BypassResponse), Text = "旁路信息" },
                Btn05 = new BtnItemConfig { ResponseName = nameof(TractionLockResponse), Text = "牵引封锁" },
                Btn06 = new BtnItemConfig { ResponseName = nameof(BorderCastResponse), Text = "广播" },
                Btn07 = new BtnItemConfig { ResponseName = nameof(HelpResponse), Text = "帮助" },
                Btn08 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn09 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn10 = new BtnItemConfig { ResponseName = nameof(MasterResponse), Text = "主页" },
                View = typeof(EventOperationView).FullName,
                TitleName = "事件信息"
            };
            var s20 = new StateConfig
            {
                Key = "站点设置",
                Btn01 = new BtnItemConfig { ResponseName = nameof(EventInfoResponse), Text = "事件信息" },
                Btn02 = new BtnItemConfig { ResponseName = nameof(SettingResponse), Text = "设置" },
                Btn03 = new BtnItemConfig { ResponseName = nameof(MaininstanceResponse), Text = "维护" },
                Btn04 = new BtnItemConfig { ResponseName = nameof(BypassResponse), Text = "旁路信息" },
                Btn05 = new BtnItemConfig { ResponseName = nameof(TractionLockResponse), Text = "牵引封锁" },
                Btn06 = new BtnItemConfig { ResponseName = nameof(BorderCastResponse), Text = "广播" },
                Btn07 = new BtnItemConfig { ResponseName = nameof(HelpResponse), Text = "帮助" },
                Btn08 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn09 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn10 = new BtnItemConfig { ResponseName = nameof(MasterResponse), Text = "主页" },
                View = typeof(StationSelectView).FullName,
                TitleName = "站点设置"
            };
            var s21 = new StateConfig
            {
                Key = "走行部状态",
                Btn01 = new BtnItemConfig { ResponseName = nameof(EventInfoResponse), Text = "事件信息" },
                Btn02 = new BtnItemConfig { ResponseName = nameof(SettingResponse), Text = "设置" },
                Btn03 = new BtnItemConfig { ResponseName = nameof(MaininstanceResponse), Text = "维护" },
                Btn04 = new BtnItemConfig { ResponseName = nameof(BypassResponse), Text = "旁路信息" },
                Btn05 = new BtnItemConfig { ResponseName = nameof(TractionLockResponse), Text = "牵引封锁" },
                Btn06 = new BtnItemConfig { ResponseName = nameof(BorderCastResponse), Text = "广播" },
                Btn07 = new BtnItemConfig { ResponseName = nameof(HelpResponse), Text = "帮助" },
                Btn08 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn09 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn10 = new BtnItemConfig { ResponseName = nameof(MasterResponse), Text = "主页" },
                View = typeof(LeeFeedView).FullName,
                TitleName = "走行部状态"
            };
            var s22 = new StateConfig
            {
                Key = "LCU状态",
                Btn01 = new BtnItemConfig { ResponseName = nameof(EventInfoResponse), Text = "事件信息" },
                Btn02 = new BtnItemConfig { ResponseName = nameof(SettingResponse), Text = "设置" },
                Btn03 = new BtnItemConfig { ResponseName = nameof(MaininstanceResponse), Text = "维护" },
                Btn04 = new BtnItemConfig { ResponseName = nameof(BypassResponse), Text = "旁路信息" },
                Btn05 = new BtnItemConfig { ResponseName = nameof(TractionLockResponse), Text = "牵引封锁" },
                Btn06 = new BtnItemConfig { ResponseName = nameof(BorderCastResponse), Text = "广播" },
                Btn07 = new BtnItemConfig { ResponseName = nameof(HelpResponse), Text = "帮助" },
                Btn08 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn09 = new BtnItemConfig { ResponseName = "", Text = "" },
                Btn10 = new BtnItemConfig { ResponseName = nameof(MasterResponse), Text = "主页" },
                View = typeof(LCUStatusView).FullName,
                TitleName = "LCU状态"
            };
            s.AllSatteConfig.Add(s0);
            s.AllSatteConfig.Add(s1);
            s.AllSatteConfig.Add(s2);
            s.AllSatteConfig.Add(s3);
            s.AllSatteConfig.Add(s4);
            s.AllSatteConfig.Add(s5);
            s.AllSatteConfig.Add(s6);
            s.AllSatteConfig.Add(s7);
            s.AllSatteConfig.Add(s8);
            s.AllSatteConfig.Add(s9);
            s.AllSatteConfig.Add(s10);
            s.AllSatteConfig.Add(s11);
            s.AllSatteConfig.Add(s12);
            s.AllSatteConfig.Add(s13);
            s.AllSatteConfig.Add(s14);
            s.AllSatteConfig.Add(s15);
            s.AllSatteConfig.Add(s16);
            s.AllSatteConfig.Add(s17);
            s.AllSatteConfig.Add(s18);
            s.AllSatteConfig.Add(s19);
            s.AllSatteConfig.Add(s20);
            s.AllSatteConfig.Add(s21);
            s.AllSatteConfig.Add(s22);
            DataSerialization.SerializeToXmlFile(s, Path.Combine(root, BtnConfig.File));
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