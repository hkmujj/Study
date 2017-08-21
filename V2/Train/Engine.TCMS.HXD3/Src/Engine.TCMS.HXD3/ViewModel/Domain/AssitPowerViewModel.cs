using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Engine.TCMS.HXD3.Model.TCMS.Domain;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.HXD3.ViewModel.Domain
{
    [Export]
    public class AssitPowerViewModel : NotificationObject
    {
        private ObservableCollection<APU> m_Apus;
        private bool m_Psu1;
        private bool m_Psu2;
        private bool m_TouchMachineRight;
        private bool m_TouchMachineMiddle;
        private bool m_TouchMachineLeft;

        public ObservableCollection<APU> Apus
        {
            private set
            {
                if (Equals(value, m_Apus))
                {
                    return;
                }

                m_Apus = value;
                RaisePropertyChanged(() => Apus);
            }
            get { return m_Apus; }
        }

        public bool Psu1
        {
            set
            {
                if (value == m_Psu1)
                {
                    return;
                }

                m_Psu1 = value;
                RaisePropertyChanged(() => Psu1);
            }
            get { return m_Psu1; }
        }

        public bool Psu2
        {
            set
            {
                if (value == m_Psu2)
                {
                    return;
                }

                m_Psu2 = value;
                RaisePropertyChanged(() => Psu2);
            }
            get { return m_Psu2; }
        }

        public bool TouchMachineLeft
        {
            set
            {
                if (value == m_TouchMachineLeft)
                {
                    return;
                }

                m_TouchMachineLeft = value;
                RaisePropertyChanged(() => TouchMachineLeft);
            }
            get { return m_TouchMachineLeft; }
        }

        public bool TouchMachineMiddle
        {
            set
            {
                if (value == m_TouchMachineMiddle)
                {
                    return;
                }

                m_TouchMachineMiddle = value;
                RaisePropertyChanged(() => TouchMachineMiddle);
            }
            get { return m_TouchMachineMiddle; }
        }

        public bool TouchMachineRight
        {
            set
            {
                if (value == m_TouchMachineRight)
                {
                    return;
                }

                m_TouchMachineRight = value;
                RaisePropertyChanged(() => TouchMachineRight);
            }
            get { return m_TouchMachineRight; }
        }

        public AssitPowerViewModel()
        {
            Apus = new ObservableCollection<APU>() {new APU(), new APU()};
        }
    }
}