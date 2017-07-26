using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.SpeedMonitoringSection;
using Motor.ATP.Infrasturcture.Model.SpeedMonitoringSection;

namespace Motor.ATP.Infrasturcture.Model
{
    public class Speed : TrainInfoPartialBase, ISpeed
    {
        private SpeedModel m_CurrentSpeed;
        private SpeedModel m_TargetSpeed;
        private SpeedModel m_ServiceBrakeInterventionSpeed;
        private SpeedModel m_EmergencyBrakeInterventionSpeed;
        private SpeedModel m_PermittedLimitSpeed;
        private SpeedModel m_WarningLimitSpeed;
        private SpeedModel m_IntervertionSpeed;
        private bool m_Visible;
        private ISpeedDialPlate m_SpeedDialPlate;

        /// <summary>
        /// 钩子的速度值
        /// </summary>
        public static readonly float HookSpeedInterval = 5;

        public Speed(ITrainInfo parent)
            : base(parent)
        {
            CurrentSpeed = new SpeedModel();
            TargetSpeed = new SpeedModel();
            ServiceBrakeInterventionSpeed = new SpeedModel();
            EmergencyBrakeInterventionSpeed = new SpeedModel();
            PermittedLimitSpeed = new SpeedModel();
            WarningLimitSpeed = new SpeedModel();
            IntervertionSpeed = new SpeedModel();
            SpeedDialPlate = new DefaultSpeedDialPlate();
            Visible = true;
        }


        public ISpeedDialPlate SpeedDialPlate
        {
            get { return m_SpeedDialPlate; }
            set
            {
                if (Equals(value, m_SpeedDialPlate))
                {
                    return;
                }
                m_SpeedDialPlate = value;
                RaisePropertyChanged(() => SpeedDialPlate);
            }
        }

        public SpeedModel CurrentSpeed
        {
            get { return m_CurrentSpeed; }
            private set
            {
                if (Equals(value, m_CurrentSpeed))
                {
                    return;
                }
                m_CurrentSpeed = (SpeedModel)value;
                RaisePropertyChanged(() => CurrentSpeed);
            }
        }
        public SpeedModel TargetSpeed
        {
            get { return m_TargetSpeed; }
            private set
            {
                if (Equals(value, m_TargetSpeed))
                {
                    return;
                }
                m_TargetSpeed = value;
                RaisePropertyChanged(() => TargetSpeed);
            }
        }

        public SpeedModel ServiceBrakeInterventionSpeed
        {
            get { return m_ServiceBrakeInterventionSpeed; }
            private set
            {
                if (Equals(value, m_ServiceBrakeInterventionSpeed))
                {
                    return;
                }
                m_ServiceBrakeInterventionSpeed = (SpeedModel)value;
                RaisePropertyChanged(() => ServiceBrakeInterventionSpeed);
            }
        }

        public SpeedModel EmergencyBrakeInterventionSpeed
        {
            get { return m_EmergencyBrakeInterventionSpeed; }
            private set
            {
                if (Equals(value, m_EmergencyBrakeInterventionSpeed))
                {
                    return;
                }
                m_EmergencyBrakeInterventionSpeed = (SpeedModel)value;
                RaisePropertyChanged(() => EmergencyBrakeInterventionSpeed);
            }
        }

        public SpeedModel PermittedLimitSpeed
        {
            get { return m_PermittedLimitSpeed; }
            private set
            {
                if (Equals(value, m_PermittedLimitSpeed))
                {
                    return;
                }
                m_PermittedLimitSpeed = (SpeedModel)value;
                RaisePropertyChanged(() => PermittedLimitSpeed);
            }
        }

        public SpeedModel WarningLimitSpeed
        {
            get { return m_WarningLimitSpeed; }
            private set
            {
                if (Equals(value, m_WarningLimitSpeed))
                {
                    return;
                }
                m_WarningLimitSpeed = (SpeedModel)value;
                RaisePropertyChanged(() => WarningLimitSpeed);
            }
        }

        public SpeedModel IntervertionSpeed
        {
            get { return m_IntervertionSpeed; }
            private set
            {
                if (Equals(value, m_IntervertionSpeed))
                {
                    return;
                }
                m_IntervertionSpeed = (SpeedModel)value;
                RaisePropertyChanged(() => IntervertionSpeed);
            }
        }


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

        ISpeedModel ISpeed.TargetSpeed
        {
            get { return TargetSpeed; }
        }


        ISpeedModel ISpeed.ServiceBrakeInterventionSpeed
        {
            get { return ServiceBrakeInterventionSpeed; }
        }


        ISpeedModel ISpeed.EmergencyBrakeInterventionSpeed
        {
            get { return EmergencyBrakeInterventionSpeed; }
        }


        ISpeedModel ISpeed.PermittedLimitSpeed
        {
            get { return PermittedLimitSpeed; }
        }


        ISpeedModel ISpeed.WarningLimitSpeed
        {
            get { return WarningLimitSpeed; }
        }


        ISpeedModel ISpeed.IntervertionSpeed
        {
            get { return IntervertionSpeed; }
        }

        ISpeedModel ISpeed.CurrentSpeed
        {
            get { return CurrentSpeed; }
        }
    }
}
