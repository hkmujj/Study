using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Project;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Events;
using Motor.ATP.Infrasturcture.Interface.Infomation;
using Motor.ATP.Infrasturcture.Interface.Service;
using Motor.ATP.Infrasturcture.Interface.SpeedMonitoringSection;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model;
using Motor.ATP.Infrasturcture.Model.Extension;
using Motor.ATP.Infrasturcture.Model.SpeedMonitoringSection;
using Motor.ATP._200H.Subsys.Controller;
using Motor.ATP._200H.Subsys.Events;
using Motor.ATP._200H.Subsys.Model.Forecast;
using Motor.ATP._200H.Subsys.Model.Message;
using Motor.ATP._200H.Subsys.ViewModel;

namespace Motor.ATP._200H.Subsys.Model
{
    public class ATP200HDomainModel : ATPDomain
    {
        /// <summary>
        /// 显示类型
        /// </summary>
        public override DisplayType DisplayType
        {
            get { return m_DisplayType; }
            set
            {
                if (value == m_DisplayType)
                {
                    return;
                }
                m_DisplayType = value;
                AssistDisplayInfo.Visible = DisplayType == DisplayType.Assist;
                RaisePropertyChanged(() => DisplayType);
            }
        }

        public ATP200HDomainModel(SubsystemInitParam initParam) : base(ATPType.ATP200H)
        {
            if (initParam != null)
            {
                InitParam = initParam;
                DataService = initParam.CommunicationDataService;
            }
            ATPHardwareButton = new ATPHardwareButton(this) { HardwareButtonViewModel = new HardwareButtonViewModel() };
            //初始化控制类
            DoMainController.Instance.Initalization(this);
        }

        public override void Initalize(ScreenIdentity identity)
        {
            GlobalParam = Model.GlobalParam.Instance;

            Version = new Lazy<VersionManager>(() =>
            {
                var rc = GlobalParam.RunningConfig.Value;

                var vm = new VersionManager
                {
                    MainVersion = rc.GetCurrentVersion(),
                    KnownVersions = new Lazy<ReadOnlyCollection<Version>>(() => rc.GetKnownVersions().AsReadOnly())
                };
                return vm;
            });

            AssistDisplayInfo = new ATP200HAssistDisplayInfo(this);

            ServiceManager = App.Current.ServiceManager;

            DriverInterfaceController = ServiceManager.GetService<IDriverInterfaceController>(ATPType.ATP200H.ToString());

            InfomationService = ServiceManager.GetService<IInfomationService>(ATPType.ATP200H.ToString());

            EventAggregator = ServiceManager.GetService<IEventAggregatorProvider>().EventAggregator;

            PlanZoneCoordinates = new List<IPlanSectionCoordinate>()
            {
                new ATP200HPlanSectionCoordinate8K(),
                new ATP200HPlanSectionCoordinate16K(),
                new ATP200HPlanSectionCoordinate32K(),
            };

            CabSignal = new CabSignal(this);
            ForecastInformation = new ForecastInformation200H(this);
            SpeedMonitoringSection = new SpeedMonitoringSection(this)
            {
                PlanSectionCoordinate = PlanZoneCoordinates[0],
                SpeedCurve = new SpeedCurve()
            };
            SpeedMonitoringSection.SpeedCurve.CurvePointCollection.Add(new DistanceSpeedPoint(200, 100));
            SpeedMonitoringSection.SpeedCurve.CurvePointCollection.Add(new DistanceSpeedPoint(2000, 100));

            GradientInfomation = new GradientInfomation(this);
            Message = new ATP200HMessage(this);

            TrainInfo = new TrainInfo(this)
            {
                KilometerPost = new KilometerPost(TrainInfo),
                ConnectState = new ConnectState(TrainInfo),
                Station = new Station(TrainInfo, new StationInterpreter(string.Empty)),
            };
            var brake = new Brake(TrainInfo);
            var speed = new Speed(TrainInfo) { SpeedDialPlate = new ATP200HSpeedDialPlate() };
            TrainInfo.Brake = brake;
            TrainInfo.Speed = speed;
            TrainInfo.Driver = new Driver(TrainInfo);

            ControlModel = new ControlModel(this);
            WarningIntervention = new WarningIntervention(this);
            CTCS = new CTCS(this);
            Other = new Other(this);

            EventAggregator.GetEvent<ChangePlanScaleEvent>()
                .Subscribe(UpdatePlanSectionCoordinate, ThreadOption.PublisherThread, true,
                    args => args.ATPType == ATPType);

            EventAggregator.GetEvent<EnsureMessageEvent>()
                .Subscribe(EnsureMessageIfNeed, ThreadOption.PublisherThread, true, args => args.ATPType == ATPType);

            ATPPower = new ATPPower(this);
            ATPPower.PropertyChanged += ATPPowerOnPropertyChanged;

            RegionFStateProvier = new RegionFStateProvier(this);

            UpdatePowerState();

            base.Initalize(identity);
        }

        private void EnsureMessageIfNeed(EnsureMessageEvent.Args args)
        {
            var msg = Message.MessageCollection.LastOrDefault();
            if (msg == null ||
                msg.InfomationItem.Content.ResponseType != args.ResponseType)
            {
                return;
            }

            if (InfomationService.CurrentEnsureingInfomation != null)
            {
                SendInterface.EnsureMessage(
                    new SendModel<IInfomationItem>(InfomationService.EnsureCurrentInfomation()));
            }

        }

        private void ATPPowerOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            UpdatePowerState();
        }

        private void UpdatePowerState()
        {
            switch (ATPPower.ATPPowerState)
            {
                case ATPPowerState.Started:
                    if (RegionFStateProvier != null)
                    {
                        RegionFStateProvier.Visible = true;
                    }
                    break;
                case ATPPowerState.Starting:
                    break;
                case ATPPowerState.Stopped:
                    if (RegionFStateProvier != null)
                    {
                        RegionFStateProvier.Visible = false;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UpdatePlanSectionCoordinate(ChangePlanScaleEvent.Args args)
        {
            switch (args.ScaleType)
            {
                case ChangePlanScaleType.Increase:
                    SpeedMonitoringSection.PlanSectionCoordinate =
                        PlanZoneCoordinates[PlanZoneCoordinates.IndexOf(SpeedMonitoringSection.PlanSectionCoordinate) - 1];
                    break;
                case ChangePlanScaleType.Reduce:
                    SpeedMonitoringSection.PlanSectionCoordinate =
                       PlanZoneCoordinates[PlanZoneCoordinates.IndexOf(SpeedMonitoringSection.PlanSectionCoordinate) + 1];
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }
}