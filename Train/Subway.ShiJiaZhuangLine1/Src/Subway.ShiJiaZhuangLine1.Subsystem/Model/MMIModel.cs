
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows;
using Excel.Interface;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;
using Subway.ShiJiaZhuangLine1.Interface;
using Subway.ShiJiaZhuangLine1.Interface.Model;
using Subway.ShiJiaZhuangLine1.Interface.Resouce;
using Subway.ShiJiaZhuangLine1.Subsystem.Constant;
using Subway.ShiJiaZhuangLine1.Subsystem.Extension;
using Subway.ShiJiaZhuangLine1.Subsystem.View.Shell;

namespace Subway.ShiJiaZhuangLine1.Subsystem.Model
{
    [Export]
    public class MMIModel : NotificationObject, IMMI, IDisposable
    {
        private ITraction m_TractionModel;
        private TitleModel m_TitleModel;
        private MainModel m_MainModel;
        private FrsmHighSpeedModel m_FrsmHighSpeedModel;
        private DoorModel m_DoorModel;
        private IButton m_ButtonModel;
        private BrakeModel m_BrakeModel;
        private IAssistModel m_AssistModel;
        private IAirPunp m_AirPunp;
        private IAirCondition m_AirCondition;
        private EmergencyTalkModel m_EmergencyTalk;
        private Visibility m_MMIBack;
        private ISmoke m_SmokeModel;
        private IStationsMgr m_StationsMgr;
        private IEnmergencyBorader m_EnmergencyBorader;
        private IEventPageModel m_EventPageModel;

        private IStationSettingModel m_StationSettingModel;

        protected IRegionManager m_RegionManager;
        private BypassModel m_BypassModel;
        private ILightSettingViewModel m_LightSetting;
        private IResetModel m_ResetModel;

        [ImportingConstructor]
        public MMIModel(IRegionManager regionManager)
        {
            m_RegionManager = regionManager;
            MMIBack = Visibility.Hidden;
            AirCondition = new AirConditionModel();
            AirPunp = new AirPumpModel();
            AssistModel = new AssistModel();
            BrakeModel = new BrakeModel();
            ButtonModel = new MainRunningBtnViewModel(this);
            DoorModel = new DoorModel();
            FrsmHighSpeedModel = new FrsmHighSpeedModel();
            MainModel = new MainModel();
            TitleModel = new TitleModel();
            TractionModel = new TractionModel();
            EmergencyTalk = new EmergencyTalkModel();
            SmokeModel = new SmokeModel();
            BoradercastMgr = new BoradcastMgr(10);
            StationsMgr = new StationsMgr();
            EnmergencyBorader = new EnmergencyBorader(this);
            EventMgr = new EventMgr(6);
            EventPageModel = new EventPageModel(this);
            StationSettingModel = new StationSettingModel(this);
            Maintence = new MaintenceViewModel();
            LightSetting = new LightSettingViewModel();
            ResetModel = new ResetModel();
        }

        public void Init()
        {
            BoradercastMgr.Load("Bordercast.txt");
            StationsMgr.Load(Path.Combine(SubsysParams.Instance.SubsystemInitParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory, "车站信息.txt"));

            //EventMgr.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config//EnventInfo.txt"));

            var evts = ExcelParser.Parser<EventInfo>(SubsysParams.Instance.SubsystemInitParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory);
            foreach (var evt in evts)
            {
                EventMgr.AllData.Add(evt.LogicId, evt);
            }

            var appConfigDirectory = SubsysParams.Instance.SubsystemInitParam.AppConfig.AppPaths.ConfigDirectory;

            var bps =
                ExcelParser.Parser<BypassUnit>(appConfigDirectory);
            BypassModel = ServiceLocator.Current.GetInstance<BypassModel>();
            BypassModel.BypassUnitCollecion = new ObservableCollection<BypassUnit>(bps);

            var bu =
                ExcelParser.Parser<BrakeUnit>(appConfigDirectory);
            BrakeModel.BrakeItemCollection = new ObservableCollection<BrakeUnit>(bu);

            var drUnit = ExcelParser.Parser<DoorUnit>(appConfigDirectory);
            DoorModel.DoorUnitCollection = new ObservableCollection<DoorUnit>(drUnit);

            var etu = ExcelParser.Parser<EmergencyTalkUnit>(appConfigDirectory);
            EmergencyTalk.EmergencyTalkUnitCollection = new ObservableCollection<EmergencyTalkUnit>(etu);

            StationSettingModel.Init();
        }


        public void ChangeStatus(object sender, CommunicationDataChangedArgs e)
        {

            AirCondition.ChangeBools(e.ChangedBools);
            AirCondition.ChangeFloats(e.ChangedFloats);
            AirPunp.ChangeBools(e.ChangedBools);
            AirPunp.ChangeFloats(e.ChangedFloats);
            AssistModel.ChangeBools(e.ChangedBools);
            AssistModel.ChangeFloats(e.ChangedFloats);
            ButtonModel.ChangeBools(e.ChangedBools);
            ButtonModel.ChangeFloats(e.ChangedFloats);
            FrsmHighSpeedModel.ChangeBools(e.ChangedBools);
            FrsmHighSpeedModel.ChangeFloats(e.ChangedFloats);
            MainModel.ChangeBools(e.ChangedBools);
            MainModel.ChangeFloats(e.ChangedFloats);
            TractionModel.ChangeBools(e.ChangedBools);
            TractionModel.ChangeFloats(e.ChangedFloats);
            ChangeBool(e.ChangedBools);
            ChangeFloat(e.ChangedFloats);
            SmokeModel.ChangeBools(e.ChangedBools);
            EventPageModel.ChangeBools(e.ChangedBools);
        }

        private string GetStationName(int value)
        {
            if (StationsMgr.AllData.ContainsKey(value))
            {
                return StationsMgr.GetStationName(value);
            }
            return string.Empty;
        }

        private void ChangeFloat(CommunicationDataChangedArgs<float> args)
        {
            args.UpdateIfContains(InFloatKeys.当前站, f => TitleModel.CurrenStation = GetStationName((int)f));
            args.UpdateIfContains(InFloatKeys.下一站, f => TitleModel.NextStatuin = GetStationName((int)f));
            args.UpdateIfContains(InFloatKeys.终点站, f => TitleModel.EndStation = GetStationName((int)f));
        }

        public IStationsMgr StationsMgr
        {
            get { return m_StationsMgr; }
            set
            {
                if (Equals(value, m_StationsMgr))
                {
                    return;
                }
                m_StationsMgr = value;
                RaisePropertyChanged(() => StationsMgr);
            }
        }

        public IMaintence Maintence { get; set; }

        private void ChangeBool(CommunicationDataChangedArgs<bool> args)
        {

            ServiceLocator.Current.GetInstance<Shell>().Dispatcher.Invoke(new Action(() =>
            {
                var value1 = Dataserver.ReadService.ReadOnlyBoolDictionary.GetValutAt(InBoolKeys.Inb车辆屏启动界面);
                var value2 = Dataserver.ReadService.ReadOnlyBoolDictionary.GetValutAt(InBoolKeys.Inb车辆屏黑屏标志0黑1亮);
                if (value2)
                {

                    if (value1)
                    {
                        m_RegionManager.RequestNavigate(RegionNames.RootRegion, ViewNames.StartView);
                    }
                    else
                    {
                        m_RegionManager.RequestNavigate(RegionNames.RootRegion, ViewNames.DoMainView);
                        //m_RegionManager.RequestNavigate(RegionNames.ShellContentRegion, ViewNames.ShellContentMainContentView);
                        //m_RegionManager.RequestNavigate(RegionNames.MainContentContentRegion, ViewNames.MainRunningView);
                        ////m_RegionManager.RequestNavigate(RegionNames.MainRunningChildrenTrainRegion, ViewNames.MainRunningDoorPage);
                        //m_RegionManager.RequestNavigate(RegionNames.MainRunningChildrenBreakerRegion, ViewNames.HightBreakerView);
                    }

                }
                else
                {
                    m_RegionManager.RequestNavigate(RegionNames.RootRegion, ViewNames.BlackView);
                }
            }));



            // args.UpdateIfContains(InBoolKeys.Inb车辆屏黑屏标志0黑1亮, b => MMIBack = b ? Visibility.Visible : Visibility.Hidden);

        }
        public IAirCondition AirCondition
        {
            get { return m_AirCondition; }
            private set
            {
                if (Equals(value, m_AirCondition))
                {
                    return;
                }
                m_AirCondition = value;
                RaisePropertyChanged(() => AirCondition);
            }
        }

        public IAirPunp AirPunp
        {
            get { return m_AirPunp; }
            private set
            {
                if (Equals(value, m_AirPunp))
                {
                    return;
                }
                m_AirPunp = value;
                RaisePropertyChanged(() => AirPunp);
            }
        }

        public IAssistModel AssistModel
        {
            get { return m_AssistModel; }
            private set
            {
                if (Equals(value, m_AssistModel))
                {
                    return;
                }
                m_AssistModel = value;
                RaisePropertyChanged(() => AssistModel);
            }
        }

        public BrakeModel BrakeModel
        {
            get { return m_BrakeModel; }
            private set
            {
                if (Equals(value, m_BrakeModel))
                {
                    return;
                }
                m_BrakeModel = value;
                RaisePropertyChanged(() => BrakeModel);
            }
        }

        public IButton ButtonModel
        {
            get { return m_ButtonModel; }
            private set
            {
                if (Equals(value, m_ButtonModel))
                {
                    return;
                }
                m_ButtonModel = value;
                RaisePropertyChanged(() => ButtonModel);
            }
        }

        public DoorModel DoorModel
        {
            get { return m_DoorModel; }
            private set
            {
                if (Equals(value, m_DoorModel))
                {
                    return;
                }
                m_DoorModel = value;
                RaisePropertyChanged(() => DoorModel);
            }
        }

        public FrsmHighSpeedModel FrsmHighSpeedModel
        {
            get { return m_FrsmHighSpeedModel; }
            private set
            {
                if (Equals(value, m_FrsmHighSpeedModel))
                {
                    return;
                }
                m_FrsmHighSpeedModel = value;
                RaisePropertyChanged(() => FrsmHighSpeedModel);
            }
        }

        public MainModel MainModel
        {
            get { return m_MainModel; }
            private set
            {
                if (Equals(value, m_MainModel))
                {
                    return;
                }
                m_MainModel = value;
                RaisePropertyChanged(() => MainModel);
            }
        }

        public ISmoke SmokeModel
        {
            get { return m_SmokeModel; }
            private set
            {
                if (Equals(value, m_SmokeModel))
                {
                    return;
                }
                m_SmokeModel = value;
                RaisePropertyChanged(() => SmokeModel);
            }
        }

        public TitleModel TitleModel
        {
            get { return m_TitleModel; }
            private set
            {
                if (Equals(value, m_TitleModel))
                {
                    return;
                }
                m_TitleModel = value;
                RaisePropertyChanged(() => TitleModel);
            }
        }

        public ITraction TractionModel
        {
            get { return m_TractionModel; }
            private set
            {
                if (Equals(value, m_TractionModel))
                {
                    return;
                }
                m_TractionModel = value;
                RaisePropertyChanged(() => TractionModel);
            }
        }

        public EmergencyTalkModel EmergencyTalk
        {
            get { return m_EmergencyTalk; }
            set
            {
                if (Equals(value, m_EmergencyTalk))
                {
                    return;
                }
                m_EmergencyTalk = value;
                RaisePropertyChanged(() => EmergencyTalk);
            }
        }

        public Visibility MMIBack
        {
            get { return m_MMIBack; }
            private set
            {
                if (value == m_MMIBack)
                {
                    return;
                }
                m_MMIBack = value;
                RaisePropertyChanged(() => MMIBack);
            }
        }

        public IBoradercastMgr BoradercastMgr { get; set; }

        public IEventMgr EventMgr { get; set; }

        public IEnmergencyBorader EnmergencyBorader
        {
            get { return m_EnmergencyBorader; }
            set
            {
                if (Equals(value, m_EnmergencyBorader))
                {
                    return;
                }
                m_EnmergencyBorader = value;
                RaisePropertyChanged(() => EnmergencyBorader);
            }
        }

        public IStationSettingModel StationSettingModel
        {
            get { return m_StationSettingModel; }
            private set
            {
                if (Equals(value, m_StationSettingModel))
                {
                    return;
                }
                m_StationSettingModel = value;
                RaisePropertyChanged(() => StationSettingModel);
            }
        }

        public IResetModel ResetModel
        {
            get { return m_ResetModel; }
            private set
            {
                if (Equals(value, m_ResetModel))
                {
                    return;
                }
                m_ResetModel = value;
                RaisePropertyChanged(() => ResetModel);
            }
        }

        public BypassModel BypassModel
        {
            set
            {
                if (Equals(value, m_BypassModel))
                {
                    return;
                }
                m_BypassModel = value;
                RaisePropertyChanged(() => BypassModel);
            }
            get { return m_BypassModel; }
        }

        public IEventPageModel EventPageModel
        {
            get { return m_EventPageModel; }
            private set
            {
                if (Equals(value, m_EventPageModel))
                {
                    return;
                }
                m_EventPageModel = value;
                RaisePropertyChanged(() => EventPageModel);
            }
        }

        public ILightSettingViewModel LightSetting
        {
            get { return m_LightSetting; }
            set
            {
                if (Equals(value, m_LightSetting))
                {
                    return;
                }
                m_LightSetting = value;
                RaisePropertyChanged(() => LightSetting);
            }
        }

        public ICommunicationDataService Dataserver { get; set; }

        public void Dispose()
        {
            TitleModel.Dispose();
        }
    }
}