using System.ComponentModel.Composition;
using Engine._6A.Interface.ViewModel.SystemSeting.TrainInfo;

namespace Engine._6A.ViewModel.Common
{
    [Export(typeof(IElectropsychometerTestViewModel))]
    public class ElectropsychometerTestViewModel : ViewModelBase, IElectropsychometerTestViewModel
    {
        private string m_ReactivePower;
        private string m_ActivePower;
        private string m_PowerFactor;
        private string m_VoltageFrequency;
        private string m_Current;
        private string m_Voltage;
        private string m_ReverseReactivePower;
        private string m_PositiveReactivePower;
        private string m_ReverseActivePower;
        private string m_PositiveActivePower;
        private string m_ErrorFlag;
        private string m_CirculationNum;

        public string CirculationNum
        {
            get { return m_CirculationNum; }
            set
            {
                if (value == m_CirculationNum)
                {
                    return;
                }
                m_CirculationNum = value;
                RaisePropertyChanged(() => CirculationNum);
            }
        }

        public string ErrorFlag
        {
            get { return m_ErrorFlag; }
            set
            {
                if (value == m_ErrorFlag)
                {
                    return;
                }
                m_ErrorFlag = value;
                RaisePropertyChanged(() => ErrorFlag);
            }
        }

        public string PositiveActivePower
        {
            get { return m_PositiveActivePower; }
            set
            {
                if (value == m_PositiveActivePower)
                {
                    return;
                }
                m_PositiveActivePower = value;
                RaisePropertyChanged(() => PositiveActivePower);
            }
        }

        public string ReverseActivePower
        {
            get { return m_ReverseActivePower; }
            set
            {
                if (value == m_ReverseActivePower)
                {
                    return;
                }
                m_ReverseActivePower = value;
                RaisePropertyChanged(() => ReverseActivePower);
            }
        }

        public string PositiveReactivePower
        {
            get { return m_PositiveReactivePower; }
            set
            {
                if (value == m_PositiveReactivePower)
                {
                    return;
                }
                m_PositiveReactivePower = value;
                RaisePropertyChanged(() => PositiveReactivePower);
            }
        }

        public string ReverseReactivePower
        {
            get { return m_ReverseReactivePower; }
            set
            {
                if (value == m_ReverseReactivePower)
                {
                    return;
                }
                m_ReverseReactivePower = value;
                RaisePropertyChanged(() => ReverseReactivePower);
            }
        }

        public string Voltage
        {
            get { return m_Voltage; }
            set
            {
                if (value == m_Voltage)
                {
                    return;
                }
                m_Voltage = value;
                RaisePropertyChanged(() => Voltage);
            }
        }

        public string Current
        {
            get { return m_Current; }
            set
            {
                if (value == m_Current)
                {
                    return;
                }
                m_Current = value;
                RaisePropertyChanged(() => Current);
            }
        }

        public string VoltageFrequency
        {
            get { return m_VoltageFrequency; }
            set
            {
                if (value == m_VoltageFrequency)
                {
                    return;
                }
                m_VoltageFrequency = value;
                RaisePropertyChanged(() => VoltageFrequency);
            }
        }

        public string PowerFactor
        {
            get { return m_PowerFactor; }
            set
            {
                if (value == m_PowerFactor)
                {
                    return;
                }
                m_PowerFactor = value;
                RaisePropertyChanged(() => PowerFactor);
            }
        }

        public string ActivePower
        {
            get { return m_ActivePower; }
            set
            {
                if (value == m_ActivePower)
                {
                    return;
                }
                m_ActivePower = value;
                RaisePropertyChanged(() => ActivePower);
            }
        }

        public string ReactivePower
        {
            get { return m_ReactivePower; }
            set
            {
                if (value == m_ReactivePower)
                {
                    return;
                }
                m_ReactivePower = value;
                RaisePropertyChanged(() => ReactivePower);
            }
        }
    }
}