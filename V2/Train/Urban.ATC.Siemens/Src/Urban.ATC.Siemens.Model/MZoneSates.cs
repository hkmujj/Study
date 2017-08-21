using Microsoft.Practices.Prism.ViewModel;
using Urban.ATC.Domain.Interface;
using Urban.ATC.Domain.Interface.ViewStates;

namespace Urban.ATC.Siemens.Model
{
    public class MZoneSates : NotificationObject, IMZoneSates
    {
        private DoorDetailModel m_DoorDetailModel;
        private EmergencyModel m_EmergencyModel;

        public DoorDetailModel DoorDetailModel
        {
            get { return m_DoorDetailModel; }
            set
            {
                if (value == m_DoorDetailModel)
                {
                    return;
                }
                m_DoorDetailModel = value;
                RaisePropertyChanged(() => DoorDetailModel);
            }
        }

        public EmergencyModel EmergencyModel
        {
            get { return m_EmergencyModel; }
            set
            {
                if (value == m_EmergencyModel)
                {
                    return;
                }
                m_EmergencyModel = value;
                RaisePropertyChanged(() => EmergencyModel);
            }
        }

        private ActualLevels m_ActualLevels;

        public ActualLevels ActualLevels
        {
            get { return m_ActualLevels; }
            set
            {
                if (m_ActualLevels == value)
                {
                    return;
                }
                m_ActualLevels = value;
                RaisePropertyChanged(() => ActualLevels);
            }
        }

        private DriveModel m_DriveModel;

        public DriveModel DriveModel
        {
            get { return m_DriveModel; }
            set
            {
                if (m_DriveModel == value)
                {
                    return;
                }
                m_DriveModel = value;
                RaisePropertyChanged(() => DriveModel);
            }
        }

        private ReverseModel m_ReverseModel;

        public ReverseModel ReverseModel
        {
            get { return m_ReverseModel; }
            set
            {
                if (m_ReverseModel == value)
                {
                    return;
                }
                m_ReverseModel = value;
                RaisePropertyChanged(() => ReverseModel);
            }
        }

        private StopModel m_StopModel;

        public StopModel StopModel
        {
            get { return m_StopModel; }
            set
            {
                if (m_StopModel == value)
                {
                    return;
                }
                m_StopModel = value;
                RaisePropertyChanged(() => StopModel);
            }
        }

        private DoorModel m_DoorModel;

        public DoorModel DoorModel
        {
            get { return m_DoorModel; }
            set
            {
                if (m_DoorModel == value)
                {
                    return;
                }
                m_DoorModel = value;
                RaisePropertyChanged(() => DoorModel);
            }
        }

        private DoorRelease m_DoorRelease;

        public DoorRelease DoorRelease
        {
            get { return m_DoorRelease; }
            set
            {
                if (m_DoorRelease == value)
                {
                    return;
                }
                m_DoorRelease = value;
                RaisePropertyChanged(() => DoorRelease);
            }
        }

        private DepartureType m_DepartureType;

        public DepartureType DepartureType
        {
            get { return m_DepartureType; }
            set
            {
                if (m_DepartureType == value)
                {
                    return;
                }
                m_DepartureType = value;
                RaisePropertyChanged(() => DepartureType);
            }
        }

        private SpecialModel m_SpecialModel;

        public SpecialModel SpecialModel
        {
            get { return m_SpecialModel; }
            set
            {
                if (m_SpecialModel == value)
                {
                    return;
                }
                m_SpecialModel = value;
                RaisePropertyChanged(() => SpecialModel);
            }
        }

        private float m_NumberT1;

        public float NumberT1
        {
            get { return m_NumberT1; }
            set
            {
                if (m_NumberT1 == value)
                {
                    return;
                }
                m_NumberT1 = value;
                RaisePropertyChanged(() => NumberT1);
            }
        }

        private float m_NumberT2;

        public float NumberT2
        {
            get { return m_NumberT2; }
            set
            {
                if (m_NumberT2 == value)
                {
                    return;
                }
                m_NumberT2 = value;
                RaisePropertyChanged(() => NumberT2);
            }
        }

        private float m_NumberT3;

        public float NumberT3
        {
            get { return m_NumberT3; }
            set
            {
                if (m_NumberT3 == value)
                {
                    return;
                }
                m_NumberT3 = value;
                RaisePropertyChanged(() => NumberT3);
            }
        }
    }
}