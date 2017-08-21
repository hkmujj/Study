using System.ComponentModel.Composition;
using Engine._6A.Constance;
using Engine._6A.Interface.ViewModel.DataMonitor.ForTheColumn;
using Engine._6A.ViewModel.Common;

namespace Engine._6A.ViewModel.Axle6
{
    [Export(ContractName.TrainUp, typeof(IForTheColumnOneViewModel))]
    [Export(ContractName.TrainDown, typeof(IForTheColumnOneViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ForTheColumnOneViewModel : ViewModelBase, IForTheColumnOneViewModel
    {
        private string m_UsePowerTwoWay;
        private string m_HalfVoltageTwoWay;
        private string m_SupplyCurrentTwoWay;
        private string m_SupplyVoltageTwoWay;
        private string m_InputCurrentTwoWay;
        private string m_InputVoltageTwoWay;
        private string m_LeakageCurrentTwoWay;
        private string m_UsePowerOneWay;
        private string m_HalfVoltageOneWay;
        private string m_SupplyCurrentOneWay;
        private string m_SupplyVoltageOneWay;
        private string m_InputCurrentOneWay;
        private string m_InputVoltageOneWay;
        private string m_LeakageCurrentOneWay;

        public string LeakageCurrentOneWay
        {
            get { return m_LeakageCurrentOneWay; }
            set
            {
                if (value == m_LeakageCurrentOneWay)
                {
                    return;
                }
                m_LeakageCurrentOneWay = value;
                RaisePropertyChanged(() => LeakageCurrentOneWay);
            }
        }

        public string InputVoltageOneWay
        {
            get { return m_InputVoltageOneWay; }
            set
            {
                if (value == m_InputVoltageOneWay)
                {
                    return;
                }
                m_InputVoltageOneWay = value;
                RaisePropertyChanged(() => InputVoltageOneWay);
            }
        }

        public string InputCurrentOneWay
        {
            get { return m_InputCurrentOneWay; }
            set
            {
                if (value == m_InputCurrentOneWay)
                {
                    return;
                }
                m_InputCurrentOneWay = value;
                RaisePropertyChanged(() => InputCurrentOneWay);
            }
        }

        public string SupplyVoltageOneWay
        {
            get { return m_SupplyVoltageOneWay; }
            set
            {
                if (value == m_SupplyVoltageOneWay)
                {
                    return;
                }
                m_SupplyVoltageOneWay = value;
                RaisePropertyChanged(() => SupplyVoltageOneWay);
            }
        }

        public string SupplyCurrentOneWay
        {
            get { return m_SupplyCurrentOneWay; }
            set
            {
                if (value == m_SupplyCurrentOneWay)
                {
                    return;
                }
                m_SupplyCurrentOneWay = value;
                RaisePropertyChanged(() => SupplyCurrentOneWay);
            }
        }

        public string HalfVoltageOneWay
        {
            get { return m_HalfVoltageOneWay; }
            set
            {
                if (value == m_HalfVoltageOneWay)
                {
                    return;
                }
                m_HalfVoltageOneWay = value;
                RaisePropertyChanged(() => HalfVoltageOneWay);
            }
        }

        public string UsePowerOneWay
        {
            get { return m_UsePowerOneWay; }
            set
            {
                if (value == m_UsePowerOneWay)
                {
                    return;
                }
                m_UsePowerOneWay = value;
                RaisePropertyChanged(() => UsePowerOneWay);
            }
        }

        public string LeakageCurrentTwoWay
        {
            get { return m_LeakageCurrentTwoWay; }
            set
            {
                if (value == m_LeakageCurrentTwoWay)
                {
                    return;
                }
                m_LeakageCurrentTwoWay = value;
                RaisePropertyChanged(() => LeakageCurrentTwoWay);
            }
        }

        public string InputVoltageTwoWay
        {
            get { return m_InputVoltageTwoWay; }
            set
            {
                if (value == m_InputVoltageTwoWay)
                {
                    return;
                }
                m_InputVoltageTwoWay = value;
                RaisePropertyChanged(() => InputVoltageTwoWay);
            }
        }

        public string InputCurrentTwoWay
        {
            get { return m_InputCurrentTwoWay; }
            set
            {
                if (value == m_InputCurrentTwoWay)
                {
                    return;
                }
                m_InputCurrentTwoWay = value;
                RaisePropertyChanged(() => InputCurrentTwoWay);
            }
        }

        public string SupplyVoltageTwoWay
        {
            get { return m_SupplyVoltageTwoWay; }
            set
            {
                if (value == m_SupplyVoltageTwoWay)
                {
                    return;
                }
                m_SupplyVoltageTwoWay = value;
                RaisePropertyChanged(() => SupplyVoltageTwoWay);
            }
        }

        public string SupplyCurrentTwoWay
        {
            get { return m_SupplyCurrentTwoWay; }
            set
            {
                if (value == m_SupplyCurrentTwoWay)
                {
                    return;
                }
                m_SupplyCurrentTwoWay = value;
                RaisePropertyChanged(() => SupplyCurrentTwoWay);
            }
        }

        public string HalfVoltageTwoWay
        {
            get { return m_HalfVoltageTwoWay; }
            set
            {
                if (value == m_HalfVoltageTwoWay)
                {
                    return;
                }
                m_HalfVoltageTwoWay = value;
                RaisePropertyChanged(() => HalfVoltageTwoWay);
            }
        }

        public string UsePowerTwoWay
        {
            get { return m_UsePowerTwoWay; }
            set
            {
                if (value == m_UsePowerTwoWay)
                {
                    return;
                }
                m_UsePowerTwoWay = value;
                RaisePropertyChanged(() => UsePowerTwoWay);
            }
        }

        public override void Clear()
        {
            base.Clear();
            var type = typeof(IForTheColumnOneViewModel);
            base.Clear(type, this);
        }
    }
}