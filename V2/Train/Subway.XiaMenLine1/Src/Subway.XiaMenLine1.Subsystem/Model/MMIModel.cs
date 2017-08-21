using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Excel.Interface;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;
using Subway.XiaMenLine1.Interface;
using Subway.XiaMenLine1.Interface.Model;
using Subway.XiaMenLine1.Subsystem.Constant;
using Subway.XiaMenLine1.Subsystem.Events;
using Subway.XiaMenLine1.Subsystem.ViewModels;
using Subway.XiaMenLine1.Interface.Resouce;
using Subway.XiaMenLine1.Subsystem.Extension;

namespace Subway.XiaMenLine1.Subsystem.Model
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
        public ICommand MMIBackCommand;
        private IStationSettingModel m_StationSettingModel;

        protected IRegionManager m_RegionManager;
        private BypassModel m_BypassModel;
        private EmergencyCauseViewModel m_EmergencyCause;

        [ImportingConstructor]
        public MMIModel(IRegionManager regionManager, MainInstanceViewModel mainInstanceViewModel)
        {
            m_RegionManager = regionManager;
            MainInstanceViewModel = mainInstanceViewModel;
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
            BoradercastMgr = new BoradcastMgr(6);
            StationsMgr = new StationsMgr();
            EnmergencyBorader = new EnmergencyBorader(this);
            EventMgr = new EventMgr(6);
            EventPageModel = new EventPageModel(this);
            StationSettingModel = new StationSettingModel(this);
            EmergencyCause = new EmergencyCauseViewModel();
        }

        public void Init()
        {
            BoradercastMgr.Load(SubsysParams.Instance.SubsystemInitParam.AppConfig.AppPaths.ConfigDirectory);
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

        private void ChangeBool(CommunicationDataChangedArgs<bool> args)
        {
            args.UpdateIfContains(InBoolKeys.Inb车辆屏黑屏标志0黑1亮, b =>
            {
                if (b)
                {
                    MMIBack = Visibility.Visible;
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                     {
                         MMIBackCommand.Execute(new MainRunningViewNavigateParam()
                         {
                             BreakerRegionViewName = ViewNames.HightBreakerView,
                             TrainRegionViewName = ViewNames.MainRunningDoorPage
                         });
                     }));
                }
                else
                {
                    MMIBack = Visibility.Hidden;
                }

            });

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

        public MainInstanceViewModel MainInstanceViewModel { get; private set; }
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

        public EmergencyCauseViewModel EmergencyCause
        {
            get { return m_EmergencyCause; }
            private set
            {
                if (Equals(value, m_EmergencyCause))
                {
                    return;
                }
                m_EmergencyCause = value;
                RaisePropertyChanged(() => EmergencyCause);
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

        public ICommunicationDataService Dataserver { get; set; }

        public void Dispose()
        {
            TitleModel.Dispose();
        }
    }
}