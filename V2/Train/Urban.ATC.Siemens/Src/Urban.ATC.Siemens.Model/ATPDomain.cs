using System;
using System.Collections.Generic;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using Motor.ATP.Domain.Interface;
using Motor.ATP.Domain.Interface.Infomation;
using Motor.ATP.Domain.Interface.Service;
using Motor.ATP.Domain.Interface.SpeedMonitoringSection;
using Motor.ATP.Domain.Interface.UserAction;

namespace Motor.ATP.Domain.Model
{
    public class ATPDomain : NotificationObject, IATP, IDisposable
    {
        private IInterfaceAdapterService m_InterfaceAdapterService;
        private SendInterfaceProxy m_SendInterfaceProxy;
        private bool m_TrainControlTypeVisible;
        private bool m_Visible;
        private TrainControlType m_TrainControlType;
        private List<IPlanSectionCoordinate> m_PlanZoneCoordinates;
        private ATPHardwareButton m_ATPHardwareButton;
        private RegionFStateProvier m_RegionFStateProvier;
        public ATPType ATPType { get; set; }

        public TrainInfo TrainInfo { get; protected set; }

        public ControlModel ControlModel { get; protected set; }

        public CabSignal CabSignal { get; protected set; }

        public WarningIntervention WarningIntervention { get; protected set; }

        public SpeedMonitoringSection.SpeedMonitoringSection SpeedMonitoringSection { get; protected set; }

        public ForecastInformation ForecastInformation { get; protected set; }

        public SubsystemInitParam InitParam { protected set; get; }

        public ATPHardwareButton ATPHardwareButton
        {
            get { return m_ATPHardwareButton; }
            protected set
            {
                if (Equals(value, m_ATPHardwareButton))
                {
                    return;
                }
                m_ATPHardwareButton = value;
                RaisePropertyChanged(() => ATPHardwareButton);
            }
        }

        public IGradientInfomation GradientInfomation { get; protected set; }

        public RegionFStateProvier RegionFStateProvier
        {
            get { return m_RegionFStateProvier; }
            protected set
            {
                if (Equals(value, m_RegionFStateProvier))
                {
                    return;
                }

                m_RegionFStateProvier = value;
                RaisePropertyChanged(() => RegionFStateProvier);
            }
        }

        public TrainControlType TrainControlType
        {
            get { return m_TrainControlType; }
            set
            {
                if (value == m_TrainControlType)
                {
                    return;
                }
                m_TrainControlType = value;
                RaisePropertyChanged(() => TrainControlType);
            }
        }

        public Message.Message Message { get; protected set; }

        public CTCS CTCS { get; protected set; }

        public Other Other { get; protected set; }

        public ATPPower ATPPower { get; protected set; }

        public ScreenIdentity Identity { get; protected set; }

        public bool Visible
        {
            get { return m_Visible; }
            set
            {
                if (value == m_Visible)
                {
                    return;
                }
                m_Visible = value;
                RaisePropertyChanged(() => Visible);
            }
        }

        public IServiceManager ServiceManager { get; protected set; }
        public IInfomationService InfomationService { get; protected set; }
        public ICommunicationDataService DataService { get; protected set; }
        public IInputGuideService InputGuideService { get; protected set; }
        public IDriverInterfaceController DriverInterfaceController { get; protected set; }

        public ISendInterface SendInterface
        {
            get { return m_SendInterfaceProxy; }
            set
            {
                m_SendInterfaceProxy.RealInterface = value ?? new NoneSendInterface();
            }
        }

        public bool TrainControlTypeVisible
        {
            get { return m_TrainControlTypeVisible; }
            set
            {
                if (value == m_TrainControlTypeVisible)
                {
                    return;
                }
                m_TrainControlTypeVisible = value;
                RaisePropertyChanged(() => TrainControlTypeVisible);
            }
        }

        IATPHardwareButton IATP.ATPHardwareButton
        {
            get { return this.ATPHardwareButton; }
        }

        protected IEventAggregator EventAggregator { get; set; }

        object IIdentityProvider.Identity
        {
            get { return Identity; }
        }

        IRegionFStateProvier IATP.RegionFStateProvier
        {
            get { return RegionFStateProvier; }
        }

        ScreenIdentity IIdentityProvider<ScreenIdentity>.Identity
        {
            get { return Identity; }
        }

        ATPType IATP.ATPType
        {
            get { return ATPType; }
        }

        IATPPower IATP.ATPPower
        {
            get { return ATPPower; }
        }

        TrainControlType IATP.TrainControlType
        {
            get { return TrainControlType; }
        }

        IMessage IATP.Message
        {
            get { return Message; }
        }

        ITrainInfo IATP.TrainInfo
        {
            get { return TrainInfo; }
        }

        ICTCS IATP.CTCS
        {
            get { return CTCS; }
        }

        IControlModel IATP.ControlModel
        {
            get { return ControlModel; }
        }

        ICabSignal IATP.CabSignal
        {
            get { return CabSignal; }
        }

        IWarningIntervention IATP.WarningIntervention
        {
            get { return WarningIntervention; }
        }

        ISpeedMonitoringSection IATP.SpeedMonitoringSection
        {
            get { return SpeedMonitoringSection; }
        }

        IForecastInformation IATP.ForecastInformation
        {
            get { return ForecastInformation; }
        }

        IOther IATP.Other
        {
            get { return Other; }
        }

        IGradientInfomation IATP.GradientInfomation
        {
            get { return GradientInfomation; }
        }


        protected IInterfaceAdapterService InterfaceAdapterService
        {
            get
            {
                return m_InterfaceAdapterService ??
                       (m_InterfaceAdapterService = App.Current.ServiceManager.GetService<IInterfaceAdapterService>());
            }
        }


        public virtual void Initalize(ScreenIdentity identity)
        {
            m_SendInterfaceProxy = new SendInterfaceProxy() { RealInterface = new NoneSendInterface() };
            var course = App.Current.ServiceManager.GetService<ICourseService>();
            course.CourseStateChanged += CourseOnCourseStateChanged;
            Identity = identity;
            ATPRepository.Instance.Regist(this);
        }

        public List<IPlanSectionCoordinate> PlanZoneCoordinates { set; get; }


        private void CourseOnCourseStateChanged(object sender, CourseStateChangedArgs courseStateChangedArgs)
        {
            if (courseStateChangedArgs.CourseService.CurrentCourseState == CourseState.Started)
            {
            }
        }


        public virtual void OnFaultOccused(IInfomationItem faultItem)
        {
        }

        public virtual void OnFaultFixed(IInfomationItem faultItem)
        {
        }

        public virtual void Dispose()
        {

        }
    }
}