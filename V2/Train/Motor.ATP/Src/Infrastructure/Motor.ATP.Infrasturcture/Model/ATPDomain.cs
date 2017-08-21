using System;
using System.Collections.Generic;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Infomation;
using Motor.ATP.Infrasturcture.Interface.Service;
using Motor.ATP.Infrasturcture.Interface.SpeedMonitoringSection;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP.Infrasturcture.Model
{
    public class ATPDomain : NotificationObject, IATP, IDisposable
    {
        private IInterfaceAdapterService m_InterfaceAdapterService;
        private SendInterfaceProxy m_SendInterfaceProxy;
        private bool m_TrainControlTypeVisible;
        private bool m_Visible;
        private TrainControlType m_TrainControlType;
        private ATPHardwareButton m_ATPHardwareButton;
        private RegionFStateProvier m_RegionFStateProvier;
        // ReSharper disable once InconsistentNaming
        protected DisplayType m_DisplayType;
        private bool m_IsUsed;
        private ATPPower m_ATPPower;

        public ATPDomain(ATPType atpType)
        {
            ATPType = atpType;
        }

        private IClearDataService ClearDataService
        {
            get { return App.Current.ServiceManager.GetService<IClearDataService>(ATPType.ToString()); }
        }

        public GlobalParamBase GlobalParam { get; protected set; }

        public ATPType ATPType { get; private set; }

        /// <summary>
        /// MMI位置
        /// </summary>
        public MMILocation MMILocation { get; protected set; }

        /// <summary>
        /// 显示类型
        /// </summary>
        public virtual DisplayType DisplayType
        {
            get { return m_DisplayType; }
            set
            {
                if (value == m_DisplayType)
                {
                    return;
                }

                m_DisplayType = value;
                RaisePropertyChanged(() => DisplayType);
            }
        }

        /// <summary>
        /// 辅助显示信息， LED显示内容
        /// </summary>
        public AssistDisplayInfo AssistDisplayInfo { get; protected set; }

        /// <summary>
        /// 辅助显示信息， LED显示内容
        /// </summary>
        IAssistDisplayInfo IATP.AssistDisplayInfo
        {
            get { return AssistDisplayInfo; }
        }

        protected Lazy<VersionManager> Version { set; get; }

        /// <summary>
        /// 版本信息
        /// </summary>
        IVersionManager IATP.VersionManager
        {
            get { return Version.Value; }
        }

        public TrainInfo TrainInfo { get; protected set; }

        public ControlModel ControlModel { get; protected set; }

        public CabSignal CabSignal { get; protected set; }

        public EmergencyInfo EmergencyInfo { get; protected set; }

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

        public ATPPower ATPPower
        {
            get { return m_ATPPower; }
            protected set
            {
                if (Equals(value, m_ATPPower))
                    return;

                m_ATPPower = value;
                m_ATPPower.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName ==
                        CommonUtil.Util.PropertySupport.ExtractPropertyName<ATPPower, ATPPowerState>(
                            a => a.ATPPowerState))
                    {
                        if (m_ATPPower.ATPPowerState == ATPPowerState.Stopped && ClearDataService != null)
                        {
                            ClearDataService.NotifyClearData(this);
                        }
                    }
                };
                RaisePropertyChanged(() => ATPPower);
            }
        }

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

        /// <summary>
        /// 是否使用 ， 不使用：整个界面消失
        /// </summary>
        public bool IsUsing
        {
            get { return m_IsUsed; }
            set
            {
                if (value == m_IsUsed)
                {
                    return;
                }

                m_IsUsed = value;
                RaisePropertyChanged(() => IsUsing);
            }
        }

        public IServiceManager ServiceManager { get; protected set; }
        public IInfomationService InfomationService { get; protected set; }
        public ICommunicationDataService DataService { get; protected set; }
        public IDriverInterfaceController DriverInterfaceController { get; protected set; }

        public DriverInterfaceController DriverInterfaceControllerImp
        {
            get { return DriverInterfaceController as DriverInterfaceController; }
        }

        public SendInterfaceCounterableProxy SendInterfaceCounterableProxy
        {
            get { return m_SendInterfaceProxy as SendInterfaceCounterableProxy; }
        }

        public ISendInterface SendInterface
        {
            get { return m_SendInterfaceProxy; }
            set { m_SendInterfaceProxy.RealInterface = value ?? new NoneSendInterface(); }
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

        IEmergencyInfo IATP.EmergencyInfo
        {
            get { return EmergencyInfo; }
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
                       (m_InterfaceAdapterService =
                           App.Current.ServiceManager.GetService<IInterfaceAdapterService>(ATPType.ToString()));
            }
        }


        public virtual void Initalize(ScreenIdentity identity)
        {
            if (InitParam == null || InitParam.DataPackage.Config.SystemConfig.IsDebugModel)
            {
                m_SendInterfaceProxy = new SendInterfaceCounterableProxy() {RealInterface = new NoneSendInterface()};
            }
            else
            {
                m_SendInterfaceProxy = new SendInterfaceProxy() {RealInterface = new NoneSendInterface()};
            }

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