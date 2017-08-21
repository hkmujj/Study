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
using Motor.ATP.Infrasturcture.Model.Extension;
using Motor.ATP.Infrasturcture.Model.Message;
using Motor.ATP.Infrasturcture.Model.SpeedMonitoringSection;
using Motor.ATP._300S.Subsys.ViewModel;

namespace Motor.ATP._300S.Subsys.Model
{
    public class ATP300SDomainModel : ATPDomain
    {
        public ATP300SDomainModel(SubsystemInitParam initParam):base(ATPType.ATP300S)
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
            Version = new Lazy<VersionManager>(() => new VersionManager());

            ServiceManager = App.Current.ServiceManager;

            DriverInterfaceController = ServiceManager.GetService<IDriverInterfaceController>(ATPType.ATP300S.ToString());

            InfomationService = ServiceManager.GetService<IInfomationService>(ATPType.ATP300S.ToString());

            EventAggregator = ServiceManager.GetService<IEventAggregatorProvider>().EventAggregator;

            PlanZoneCoordinates = new List<IPlanSectionCoordinate>()
            {
                new ATP300SPlanSectionCoordinate8K(),
                new ATP300SPlanSectionCoordinate16K(),
                new ATP300SPlanSectionCoordinate32K(),
            };

            CabSignal = new CabSignal(this);
            ForecastInformation = new ForecastInformation(this);
            SpeedMonitoringSection = new SpeedMonitoringSection(this)
            {
                PlanSectionCoordinate = PlanZoneCoordinates[2],
                SpeedCurve = new SpeedCurve()
            };
            SpeedMonitoringSection.SpeedCurve.CurvePointCollection.Add(new DistanceSpeedPoint(200, 100));
            SpeedMonitoringSection.SpeedCurve.CurvePointCollection.Add(new DistanceSpeedPoint(2000, 100));
            SpeedMonitoringSection.PropertyChanged += SpeedMonitoringSectionOnPropertyChanged;

            GradientInfomation = new GradientInfomation(this);
            Message = new Message(this);

            TrainInfo = new TrainInfo(this)
            {
                KilometerPost = new KilometerPost(TrainInfo),
                ConnectState = new ConnectState(TrainInfo),
                Station = new Station(TrainInfo, new StationInterpreter(string.Empty)),
            };
            var brake = new Brake(TrainInfo);
            var speed = new Speed(TrainInfo) { SpeedDialPlate = new ATP300SSpeedDialPlate() };
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

            this.SetAllVisible(true);
        }

        private void SpeedMonitoringSectionOnPropertyChanged(object sender,
            PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName ==
                PropertySupport.ExtractPropertyName<ISpeedMonitoringSection, bool>(p => p.ZoomVisible))
            {
                if (!SpeedMonitoringSection.ZoomVisible)
                {
                    SpeedMonitoringSection.PlanSectionCoordinate =
                        PlanZoneCoordinates.Find(f => f.GetType() == typeof(ATP300SPlanSectionCoordinate32K));
                }
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
            if (!SpeedMonitoringSection.ZoomVisible)
            {
                return;
            }

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