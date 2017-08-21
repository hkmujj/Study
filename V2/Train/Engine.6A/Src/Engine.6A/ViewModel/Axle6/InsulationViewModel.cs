using System.ComponentModel.Composition;
using Engine._6A.Interface.ViewModel.DataMonitor;
using Engine._6A.ViewModel.Common;

namespace Engine._6A.ViewModel.Axle6
{
    [Export(typeof(IInsulationViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class InsulationViewModel : ViewModelBase, IInsulationViewModel
    {
        private string m_AlarmThreshold;
        private string m_PowerModule;
        private string m_ElectricNetwork;
        private string m_ElectricKey;
        private string m_TestResult;
        private string m_TestType;
        private double m_TestVoltage;

        public InsulationViewModel()
        {

        }

        public double TestVoltage
        {
            get { return m_TestVoltage; }
            set
            {
                if (value.Equals(m_TestVoltage))
                {
                    return;
                }
                m_TestVoltage = value;
                RaisePropertyChanged(() => TestVoltage);
            }
        }

        public string TestType
        {
            get { return m_TestType; }
            set
            {
                if (value == m_TestType)
                {
                    return;
                }
                m_TestType = value;
                RaisePropertyChanged(() => TestType);
            }
        }

        public string TestResult
        {
            get { return m_TestResult; }
            set
            {
                if (value == m_TestResult)
                {
                    return;
                }
                m_TestResult = value;
                RaisePropertyChanged(() => TestResult);
            }
        }

        public string ElectricKey
        {
            get { return m_ElectricKey; }
            set
            {
                if (value == m_ElectricKey)
                {
                    return;
                }
                m_ElectricKey = value;
                RaisePropertyChanged(() => ElectricKey);
            }
        }

        public string ElectricNetwork
        {
            get { return m_ElectricNetwork; }
            set
            {
                if (value == m_ElectricNetwork)
                {
                    return;
                }
                m_ElectricNetwork = value;
                RaisePropertyChanged(() => ElectricNetwork);
            }
        }

        public string PowerModule
        {
            get { return m_PowerModule; }
            set
            {
                if (value == m_PowerModule)
                {
                    return;
                }
                m_PowerModule = value;
                RaisePropertyChanged(() => PowerModule);
            }
        }

        public string AlarmThreshold
        {
            get { return m_AlarmThreshold; }
            set
            {
                if (value == m_AlarmThreshold)
                {
                    return;
                }
                m_AlarmThreshold = value;
                RaisePropertyChanged(() => AlarmThreshold);
            }
        }

        public override void Clear()
        {
            base.Clear();
            var type = typeof(InsulationViewModel);
            base.Clear(type, this);
        }
    }
}