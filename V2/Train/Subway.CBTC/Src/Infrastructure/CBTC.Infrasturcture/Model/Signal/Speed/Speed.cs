using CBTC.Infrasturcture.Model.Constant;
using Microsoft.Practices.Prism.ViewModel;

namespace CBTC.Infrasturcture.Model.Signal.Speed
{
    public class Speed: NotificationObject
    {
        private ISpeedDialPlate m_SpeedDialPlate;
        private BrakeDetailsType m_BrakeDetailsType;
        private bool m_IsSpeeding;
        private bool m_IsZeroSpeed;

        public Speed()
        {
            CurrentSpeed = new SpeedModel();
            TargetSpeed = new SpeedModel();
            PermittedSpeed = new SpeedModel();
            EmergencyBrakeInterventionSpeed  = new SpeedModel();
            WarningLimitSpeed = new SpeedModel();
        }

        public ISpeedDialPlate SpeedDialPlate
        {
            get { return m_SpeedDialPlate; }
            set
            {
                if (Equals(value, m_SpeedDialPlate))
                    return;

                m_SpeedDialPlate = value;
                RaisePropertyChanged(() => SpeedDialPlate);
            }
        }

        /// <summary>
        /// 超速报警及输出紧急制动
        /// </summary>
        public BrakeDetailsType BrakeDetailsType
        {
            get { return m_BrakeDetailsType; }
            set
            {
                if (Equals(value, m_BrakeDetailsType))
                    return;

                m_BrakeDetailsType = value;
                RaisePropertyChanged(() => BrakeDetailsType);
            }
        }

        /// <summary>
        /// 是否0速
        /// </summary>
        public bool IsZeroSpeed
        {
            get { return m_IsZeroSpeed; }
            set
            {
                if (value == m_IsZeroSpeed)
                    return;

                m_IsZeroSpeed = value;
                RaisePropertyChanged(() => IsZeroSpeed);
            }
        }

        /// <summary>
        /// 是否超速
        /// </summary>
        public bool IsSpeeding
        {
            get { return m_IsSpeeding; }
            set
            {
                if (value == m_IsSpeeding)
                    return;

                m_IsSpeeding = value;
                RaisePropertyChanged(() => IsSpeeding);
            }
        }
        
        public SpeedModel CurrentSpeed { get; protected set; }

        public SpeedModel TargetSpeed { get; protected set; }

        public SpeedModel PermittedSpeed { get; protected set; }

        public SpeedModel EmergencyBrakeInterventionSpeed { get; protected set; }

        public SpeedModel WarningLimitSpeed { get; protected set; }
    }
}