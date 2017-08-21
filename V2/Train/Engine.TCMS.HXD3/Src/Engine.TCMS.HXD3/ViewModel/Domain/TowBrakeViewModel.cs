using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.HXD3.ViewModel.Domain
{
    [Export]
    public class TowBrakeViewModel : NotificationObject
    {
        private float m_TowBrake5;
        private float m_TowBrake4;
        private float m_TowBrake3;
        private float m_TowBrake2;
        private float m_TowBrake1;
        private float m_ControlVoltage;
        private float m_RawSideCurrent;
        private float m_RawSideVoltage;
        private float m_TowBrake6;

        public float RawSideVoltage
        {
            set
            {
                if (value.Equals(m_RawSideVoltage))
                {
                    return;
                }

                m_RawSideVoltage = value;
                RaisePropertyChanged(() => RawSideVoltage);
            }
            get { return m_RawSideVoltage; }
        }

        public float RawSideCurrent
        {
            set
            {
                if (value.Equals(m_RawSideCurrent))
                {
                    return;
                }

                m_RawSideCurrent = value;
                RaisePropertyChanged(() => RawSideCurrent);
            }
            get { return m_RawSideCurrent; }
        }

        public float ControlVoltage
        {
            set
            {
                if (value.Equals(m_ControlVoltage))
                {
                    return;
                }

                m_ControlVoltage = value;
                RaisePropertyChanged(() => ControlVoltage);
            }
            get { return m_ControlVoltage; }
        }

        public float TowBrake1
        {
            set
            {
                if (value.Equals(m_TowBrake1))
                {
                    return;
                }

                m_TowBrake1 = value;
                RaisePropertyChanged(() => TowBrake1);
            }
            get { return m_TowBrake1; }
        }

        public float TowBrake2
        {
            set
            {
                if (value.Equals(m_TowBrake2))
                {
                    return;
                }

                m_TowBrake2 = value;
                RaisePropertyChanged(() => TowBrake2);
            }
            get { return m_TowBrake2; }
        }

        public float TowBrake3
        {
            set
            {
                if (value.Equals(m_TowBrake3))
                {
                    return;
                }

                m_TowBrake3 = value;
                RaisePropertyChanged(() => TowBrake3);
            }
            get { return m_TowBrake3; }
        }

        public float TowBrake4
        {
            set
            {
                if (value.Equals(m_TowBrake4))
                {
                    return;
                }

                m_TowBrake4 = value;
                RaisePropertyChanged(() => TowBrake4);
            }
            get { return m_TowBrake4; }
        }

        public float TowBrake5
        {
            set
            {
                if (value.Equals(m_TowBrake5))
                {
                    return;
                }

                m_TowBrake5 = value;
                RaisePropertyChanged(() => TowBrake5);
            }
            get { return m_TowBrake5; }
        }

        public float TowBrake6
        {
            set
            {
                if (value.Equals(m_TowBrake6))
                {
                    return;
                }

                m_TowBrake6 = value;
                RaisePropertyChanged(() => TowBrake6);
            }
            get { return m_TowBrake6; }
        }
    }
}