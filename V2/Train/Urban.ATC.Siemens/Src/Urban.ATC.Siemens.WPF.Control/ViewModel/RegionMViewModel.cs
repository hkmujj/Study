using System.Threading;
using System.Windows;
using Urban.ATC.Siemens.WPF.Interface.ViewStates;

namespace Urban.ATC.Siemens.WPF.Control.ViewModel
{
    public class RegionMViewModel : ViewModelBase
    {
        private SpecialModel m_SpecialModel;
        private ErrorModel m_ErrorModel;
        private EmergencyModel m_EmergencyModel;
        private DoorModel m_DoorModel;
        private DepartureType m_DepartureType;
        private DoorRelease m_DoorRelease;
        private StopModel m_StopModel;
        private ReverseModel m_ReverseModel;
        private ActualLevels m_ActualLevels;
        private DriveModel m_DriveModel;
        private ButtonStatus m_ButtonStatus;
        private Timer m_Timer;
        private Visibility m_ReverseModelVisibility;

        public RegionMViewModel()
        {
            DriveModel = DriveModel.None;
            ActualLevels = ActualLevels.Initial;
            ReverseModel = ReverseModel.Initial;
            StopModel = StopModel.Initial;
            DoorRelease = DoorRelease.Initial;
            DepartureType = DepartureType.None;
            DoorModel = DoorModel.None;
            EmergencyModel = EmergencyModel.None;
            ErrorModel = ErrorModel.None;
            SpecialModel = SpecialModel.Initial;
            ButtonStatus = ButtonStatus.Initial;
            m_Timer = new Timer((state =>
            {
                ReverseModelVisibility = ReverseModelVisibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;

            }));
        }

        public Visibility ReverseModelVisibility
        {
            get { return m_ReverseModelVisibility; }
            set
            {
                if (value == m_ReverseModelVisibility)
                {
                    return;
                }
                m_ReverseModelVisibility = value;
                RaisePropertyChanged(() => ReverseModelVisibility);
            }
        }

        public ButtonStatus ButtonStatus
        {
            get { return m_ButtonStatus; }
            set
            {
                if (value == m_ButtonStatus)
                {
                    return;
                }
                m_ButtonStatus = value;
                RaisePropertyChanged(() => ButtonStatus);
            }
        }

        public DriveModel DriveModel
        {
            get { return m_DriveModel; }
            set
            {
                if (value == m_DriveModel)
                {
                    return;
                }
                m_DriveModel = value;
                RaisePropertyChanged(() => DriveModel);
            }
        }

        public ActualLevels ActualLevels
        {
            get { return m_ActualLevels; }
            set
            {
                if (value == m_ActualLevels)
                {
                    return;
                }
                m_ActualLevels = value;
                RaisePropertyChanged(() => ActualLevels);
            }
        }

        public ReverseModel ReverseModel
        {
            get { return m_ReverseModel; }
            set
            {
                if (value == m_ReverseModel)
                {
                    return;
                }
                m_ReverseModel = value;
                RaisePropertyChanged(() => ReverseModel);
                //  ReverModelChanged();
            }
        }

        private void ReverModelChanged()
        {
            if (ReverseModel == ReverseModel.AROffered || ReverseModel == ReverseModel.DTRO)
            {
                m_Timer.Change(1000, 1000);
            }
            else
            {
                m_Timer.Change(int.MaxValue, int.MaxValue);
                ReverseModelVisibility = Visibility.Visible;
            }
        }
        public StopModel StopModel
        {
            get { return m_StopModel; }
            set
            {
                if (value == m_StopModel)
                {
                    return;
                }
                m_StopModel = value;
                RaisePropertyChanged(() => StopModel);
            }
        }

        public DoorRelease DoorRelease
        {
            get { return m_DoorRelease; }
            set
            {
                if (value == m_DoorRelease)
                {
                    return;
                }
                m_DoorRelease = value;
                RaisePropertyChanged(() => DoorRelease);
            }
        }

        public DepartureType DepartureType
        {
            get { return m_DepartureType; }
            set
            {
                if (value == m_DepartureType)
                {
                    return;
                }
                m_DepartureType = value;
                RaisePropertyChanged(() => DepartureType);
            }
        }

        public DoorModel DoorModel
        {
            get { return m_DoorModel; }
            set
            {
                if (value == m_DoorModel)
                {
                    return;
                }
                m_DoorModel = value;
                RaisePropertyChanged(() => DoorModel);
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

        public ErrorModel ErrorModel
        {
            get { return m_ErrorModel; }
            set
            {
                if (value == m_ErrorModel)
                {
                    return;
                }
                m_ErrorModel = value;
                RaisePropertyChanged(() => ErrorModel);
            }
        }

        public SpecialModel SpecialModel
        {
            get { return m_SpecialModel; }
            set
            {
                if (value == m_SpecialModel)
                {
                    return;
                }
                m_SpecialModel = value;
                RaisePropertyChanged(() => SpecialModel);
            }
        }
    }
}