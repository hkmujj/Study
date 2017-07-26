using System;
using System.Collections.Generic;
using System.ComponentModel;
using CommonUtil.Util;
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
using Motor.ATP.Infrasturcture.Model.Message;
using Motor.ATP.Infrasturcture.Model.SpeedMonitoringSection;
using Motor.ATP._300H.Subsys.ViewModel;

namespace Motor.ATP._300H.Subsys.Model
{
    public class ATP300HDomainModel : ATPDomain
    {
        public ATP300HDomainModel(SubsystemInitParam initParam) : base(ATPType.ATP300H)
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
                Version v;

                try
                {
                     v = System.Version.Parse(GlobalParam.RunningConfig.Value.Version);
                }
                catch (Exception)
                {
                    AppLog.Warn("Can not parser version, let version = 0.0");
                    v = new Version();
                }

                AppLog.Info("Parse version sucess, the version = {0}", v);

                var vm = new VersionManager {MainVersion = v};
                return vm;
            });

            ServiceManager = App.Current.ServiceManager;

            DriverInterfaceController = ServiceManager.GetService<IDriverInterfaceController>(ATPType.ATP300H.ToString());

            InfomationService = ServiceManager.GetService<IInfomationService>(ATPType.ATP300H.ToString());

            EventAggregator = ServiceManager.GetService<IEventAggregatorProvider>().EventAggregator;

            PlanZoneCoordinates = new List<IPlanSectionCoordinate>()
            {
                new ATP300HPlanSectionCoordinate8K(),
                new ATP300HPlanSectionCoordinate16K(),
                new ATP300HPlanSectionCoordinate32K(),
            };

            CabSignal = new CabSignal(this);
            ForecastInformation = new ForecastInformation(this);
            SpeedMonitoringSection = new SpeedMonitoringSection(this)
            {
                PlanSectionCoordinate = PlanZoneCoordinates[0],
                SpeedCurve = new SpeedCurve()
            };
            SpeedMonitoringSection.SpeedCurve.CurvePointCollection.Add(new DistanceSpeedPoint(200, 100));
            SpeedMonitoringSection.SpeedCurve.CurvePointCollection.Add(new DistanceSpeedPoint(2000, 100));

            GradientInfomation = new GradientInfomation(this);
            Message = new Message(this);

            TrainInfo = new TrainInfo(this)
            {
                KilometerPost = new KilometerPost(TrainInfo),
                ConnectState = new ConnectState(TrainInfo),
                Station = new Station(TrainInfo, new StationInterpreter(string.Empty)),
            };
            var brake = new Brake(TrainInfo);
            var speed = new Speed(TrainInfo) { SpeedDialPlate = new ATP300HSpeedDialPlate() };
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

            ATPPower = new ATPPower(this);
            ATPPower.PropertyChanged += ATPPowerOnPropertyChanged;

            RegionFStateProvier = new RegionFStateProvier(this);

            UpdatePowerState();

            base.Initalize(identity);
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