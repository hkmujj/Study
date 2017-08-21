using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using LightRail.HMI.SZLHLF.Model.BroadcastControlModel;

namespace LightRail.HMI.SZLHLF.ViewModel
{
    [Export]
    public class BroadcastControlModel : NotificationObject
    {
        private EmergencyCall m_Mc1EmergencyCall1;
        private EmergencyCall m_Mc1EmergencyCall2;
        private EmergencyCall m_TEmergencyCall1;
        private EmergencyCall m_TEmergencyCall2;
        private EmergencyCall m_MEmergencyCall1;
        private EmergencyCall m_MEmergencyCall2;
        private EmergencyCall m_Mc2EmergencyCall1;
        private EmergencyCall m_Mc2EmergencyCall2;

        public EmergencyCall Mc1EmergencyCall1
        {
            get { return m_Mc1EmergencyCall1; }
            set
            {
                if (value == m_Mc1EmergencyCall1)
                {
                    return;
                }

                m_Mc1EmergencyCall1 = value;
                RaisePropertyChanged(() => Mc1EmergencyCall1);
            }
        }

        public EmergencyCall Mc1EmergencyCall2
        {
            get { return m_Mc1EmergencyCall2; }
            set
            {
                if (value == m_Mc1EmergencyCall2)
                {
                    return;
                }

                m_Mc1EmergencyCall2 = value;
                RaisePropertyChanged(() => Mc1EmergencyCall2);
            }
        }

        public EmergencyCall TEmergencyCall1
        {
            get { return m_TEmergencyCall1; }
            set
            {
                if (value == m_TEmergencyCall1)
                {
                    return;
                }

                m_TEmergencyCall1 = value;
                RaisePropertyChanged(() => TEmergencyCall1);
            }
        }

        public EmergencyCall TEmergencyCall2
        {
            get { return m_TEmergencyCall2; }
            set
            {
                if (value == m_TEmergencyCall2)
                {
                    return;
                }

                m_TEmergencyCall2 = value;
                RaisePropertyChanged(() => TEmergencyCall2);
            }
        }

        public EmergencyCall MEmergencyCall1
        {
            get { return m_MEmergencyCall1; }
            set
            {
                if (value == m_MEmergencyCall1)
                {
                    return;
                }

                m_MEmergencyCall1 = value;
                RaisePropertyChanged(() => MEmergencyCall1);
            }
        }

        public EmergencyCall MEmergencyCall2
        {
            get { return m_MEmergencyCall2; }
            set
            {
                if (value == m_MEmergencyCall2)
                {
                    return;
                }

                m_MEmergencyCall2 = value;
                RaisePropertyChanged(() => MEmergencyCall2);
            }
        }

        public EmergencyCall Mc2EmergencyCall1
        {
            get { return m_Mc2EmergencyCall1; }
            set
            {
                if (value == m_Mc2EmergencyCall1)
                {
                    return;
                }

                m_Mc2EmergencyCall1 = value;
                RaisePropertyChanged(() => Mc2EmergencyCall1);
            }
        }

        public EmergencyCall Mc2EmergencyCall2
        {
            get { return m_Mc2EmergencyCall2; }
            set
            {
                if (value == m_Mc2EmergencyCall2)
                {
                    return;
                }

                m_Mc2EmergencyCall2 = value;
                RaisePropertyChanged(() => Mc2EmergencyCall2);
            }
        }
    }
}
