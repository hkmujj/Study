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
using Motor.ATP._200C.Subsys.Events;
using Motor.ATP._200C.Subsys.Model.Forecast;
using Motor.ATP._200C.Subsys.Model.Message;
using Motor.ATP._200C.Subsys.ViewModel;

namespace Motor.ATP._200C.Subsys.Model
{
    public class ATP200CDomainModel : ATPDomain
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

        public ATP200CDomainModel(SubsystemInitParam initParam):base(ATPType.ATP200C)
        {
            if (initParam != null)
            {
                InitParam = initParam;
                DataService = initParam.CommunicationDataService;
            }
            ATPHardwareButton = new ATPHardwareButton(this) { HardwareButtonViewModel = new HardwareButtonViewModel() };
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

            AssistDisplayInfo = new ATP200CAssistDisplayInfo(this);

            ServiceManager = App.Current.ServiceManager;

            DriverInterfaceController = ServiceManager.GetService<IDriverInterfaceController>(ATPType.ATP200C.ToString());

            InfomationService = ServiceManager.GetService<IInfomationService>(ATPType.ATP200C.ToString());

            EventAggregator = ServiceManager.GetService<IEventAggregatorProvider>().EventAggregator;

            PlanZoneCoordinates = new List<IPlanSectionCoordinate>()
            {
                new ATP200CPlanSectionCoordinate8K(),
                new ATP200CPlanSectionCoordinate16K(),
                new ATP200CPlanSectionCoordinate32K(),
            };

            CabSignal = new CabSignal(this);
            ForecastInformation = new ForecastInformation200C(this);
            SpeedMonitoringSection = new SpeedMonitoringSection(this)
            {
                PlanSectionCoordinate = PlanZoneCoordinates[0],
                SpeedCurve = new SpeedCurve()
            };
            SpeedMonitoringSection.SpeedCurve.CurvePointCollection.Add(new DistanceSpeedPoint(200, 100));
            SpeedMonitoringSection.SpeedCurve.CurvePointCollection.Add(new DistanceSpeedPoint(2000, 100));

            GradientInfomation = new GradientInfomation(this);
            Message = new ATP200CMessage(this);

            TrainInfo = new TrainInfo(this)
            {
                KilometerPost = new KilometerPost(TrainInfo),
                ConnectState = new ConnectState(TrainInfo),
                Station = new Station(TrainInfo, new StationInterpreter(string.Empty)),
            };
            var brake = new Brake(TrainInfo);
            var speed = new Speed(TrainInfo) { SpeedDialPlate = new ATP200CSpeedDialPlate() };
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