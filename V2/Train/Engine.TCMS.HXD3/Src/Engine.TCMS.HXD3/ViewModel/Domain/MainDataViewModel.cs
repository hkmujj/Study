using System.ComponentModel.Composition;
using Engine.TCMS.HXD3.Model.TCMS.Domain.Constant;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.HXD3.ViewModel.Domain
{
    [Export]
    public class MainDataViewModel : NotificationObject
    {
        private TowBrakeWorkState m_TowBrakeWorkState;
        private bool m_IsPackingBraking;
        private NobodyWarningLevel m_NobodyWarningLevel;
        private MainSwitchState m_MainSwitchState;
        private ConstantSpeedState m_ConstantSpeedState;
        private float m_CurrentSpeed;
        private float m_LevelValue;
        private string m_MainSwitchNotification;

        public MainDataViewModel()
        {
            ConstantSpeedState = ConstantSpeedState.Green;
            MainSwitchState = MainSwitchState.SwitchOff;
        }

        public float CurrentSpeed
        {
            set
            {
                if (value.Equals(m_CurrentSpeed))
                {
                    return;
                }

                m_CurrentSpeed = value;
                RaisePropertyChanged(() => CurrentSpeed);
            }
            get { return m_CurrentSpeed; }
        }

        public float LevelValue
        {
            set
            {
                if (value.Equals(m_LevelValue))
                {
                    return;
                }

                m_LevelValue = value;
                RaisePropertyChanged(() => LevelValue);
            }
            get { return m_LevelValue; }
        }


        public ConstantSpeedState ConstantSpeedState
        {
            set
            {
                if (value == m_ConstantSpeedState)
                {
                    return;
                }

                m_ConstantSpeedState = value;
                RaisePropertyChanged(() => ConstantSpeedState);
            }
            get { return m_ConstantSpeedState; }
        }

        public bool IsPackingBraking
        {
            set
            {
                if (value == m_IsPackingBraking)
                {
                    return;
                }

                m_IsPackingBraking = value;
                RaisePropertyChanged(() => IsPackingBraking);
            }
            get { return m_IsPackingBraking; }
        }



        public TowBrakeWorkState TowBrakeWorkState
        {
            set
            {
                if (value == m_TowBrakeWorkState)
                {
                    return;
                }

                m_TowBrakeWorkState = value;
                RaisePropertyChanged(() => TowBrakeWorkState);
            }
            get { return m_TowBrakeWorkState; }
        }

        public NobodyWarningLevel NobodyWarningLevel
        {
            set
            {
                if (value == m_NobodyWarningLevel)
                {
                    return;
                }

                m_NobodyWarningLevel = value;
                RaisePropertyChanged(() => NobodyWarningLevel);
            }
            get { return m_NobodyWarningLevel; }
        }

        public string MainSwitchNotification
        {
            set
            {
                if (value == m_MainSwitchNotification)
                {
                    return;
                }

                m_MainSwitchNotification = value;
                RaisePropertyChanged(() => MainSwitchNotification);
            }
            get { return m_MainSwitchNotification; }
        }

        public MainSwitchState MainSwitchState
        {
            set
            {
                if (value == m_MainSwitchState)
                {
                    return;
                }

                m_MainSwitchState = value;
                RaisePropertyChanged(() => MainSwitchState);
            }
            get { return m_MainSwitchState; }
        }
    }
}