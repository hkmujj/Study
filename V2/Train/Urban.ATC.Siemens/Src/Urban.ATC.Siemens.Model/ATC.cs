using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Service;
using Motor.ATP.Domain.Interface;
using Motor.ATP.Domain.Model;
using Urban.ATC.Domain.Interface;
using Urban.ATC.Domain.Interface.ViewStates;

namespace Urban.ATC.Siemens.Model
{
    public class ATC : NotificationObject, IATC
    {
        private double m_TargetDistance;
        private Speed m_Speed;
        private TargetBarType m_TargetDistanceBarColor;
        private MZoneSates m_MZoneSates;
        private CZoneStatus m_CZoneStatus;
        private FZoneStatus m_FZoneStatus;

        public ICommunicationDataService DataService { set; get; }
        private BrakeDetailsType m_BrakeDetailsType;
        private bool m_MMIBack;
        private CourseState m_CourseState;
        private double m_TargetSpeed;

        public double TargetSpeed
        {
            get { return m_TargetSpeed; }
            set
            {
                if (value.Equals(m_TargetSpeed))
                {
                    return;
                }
                m_TargetSpeed = value;
                RaisePropertyChanged(() => TargetSpeed);
            }
        }

        public BrakeDetailsType BrakeDetailsType
        {
            set
            {
                if (value.Equals(m_BrakeDetailsType))
                {
                    return;
                }
                m_BrakeDetailsType = value;
                RaisePropertyChanged(() => BrakeDetailsType);
            }
            get { return m_BrakeDetailsType; }
        }

        public CourseState CourseState
        {
            get { return m_CourseState; }
            set
            {
                if (value == m_CourseState) return;
                m_CourseState = value;
                RaisePropertyChanged(() => CourseState);
            }
        }

        /// <summary>
        /// 目标距离
        /// </summary>
        public double TargetDistance
        {
            set
            {
                if (value.Equals(m_TargetDistance))
                {
                    return;
                }
                m_TargetDistance = value;
                RaisePropertyChanged(() => TargetDistance);
            }
            get { return m_TargetDistance; }
        }

        public TargetBarType TargetDistanceBarColor
        {
            set
            {
                if (value.Equals(m_TargetDistanceBarColor))
                {
                    return;
                }
                m_TargetDistanceBarColor = value;
                RaisePropertyChanged(() => TargetDistanceBarColor);
            }
            get { return m_TargetDistanceBarColor; }
        }

        ISpeed IATC.Speed
        {
            get { return this.Speed; }
        }

        public CZoneStatus CZoneStatus
        {
            get { return m_CZoneStatus; }
            set
            {
                if (Equals(value, m_CZoneStatus))
                {
                    return;
                }
                m_CZoneStatus = value;
                RaisePropertyChanged(() => CZoneStatus);
            }
        }

        public bool MMIBack
        {
            get { return m_MMIBack; }
            set
            {
                if (value == m_MMIBack) return;
                m_MMIBack = value;
                RaisePropertyChanged(() => MMIBack);
            }
        }

        ICZoneStatus IATC.CZoneStatus
        {
            get { return this.CZoneStatus; }
        }

        public MZoneSates MZoneSates
        {
            get { return m_MZoneSates; }
            set
            {
                if (Equals(value, m_MZoneSates))
                {
                    return;
                }
                m_MZoneSates = value;
                RaisePropertyChanged(() => MZoneSates);
            }
        }

        public FZoneStatus FZoneStatus
        {
            get { return m_FZoneStatus; }
            set
            {
                if (Equals(value, m_FZoneStatus))
                {
                    return;
                }
                m_FZoneStatus = value;
                RaisePropertyChanged(() => FZoneStatus);
            }
        }

        IFZoneStatus IATC.FZoneStatus
        {
            get { return this.FZoneStatus; }
        }

        IMZoneSates IATC.MZoneSates
        {
            get { return this.MZoneSates; }
        }

        public Speed Speed
        {
            private set
            {
                if (Equals(value, m_Speed))
                {
                    return;
                }
                m_Speed = value;
                RaisePropertyChanged(() => Speed);
            }
            get { return m_Speed; }
        }

        public ATC()
        {
            Speed = new Speed(null);
            MZoneSates = new MZoneSates();
            CZoneStatus = new CZoneStatus();
            FZoneStatus = new FZoneStatus();
            FZoneStatus.MessgeInfo = new MessgeInfo();
        }
    }
}