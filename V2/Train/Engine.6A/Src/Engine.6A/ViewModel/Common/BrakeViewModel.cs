using System.ComponentModel.Composition;
using Engine._6A.Interface.ViewModel.DataMonitor;

namespace Engine._6A.ViewModel.Common
{
    [Export(typeof(IBrakeViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BrakeViewModel : ViewModelBase, IBrakeViewModel
    {
        private string m_FlowTransmitter;
        private string m_PressureTransmitter;
        private string m_BoardInformation;
        private string m_CurrentNetworkFlow;
        private string m_CurrentState;
        private string m_TrainNumber;
        private double m_BrakeCylinder;
        private double m_BalancingCylinderTwo;
        private double m_BalancingCylinderOne;
        private double m_ParkingCylinderTwo;
        private double m_ParkingCylinderOne;
        private double m_TrainPipe;
        private string m_TransNum;
        private string m_ReferSpeed;
        private string m_ParkingBrake;

        public BrakeViewModel()
        {
            ParkingBrake = "— —";
            ReferSpeed = "0";
            TransNum = "— —";
        }

        public double TrainPipe
        {
            get { return m_TrainPipe; }
            set
            {
                if (value == m_TrainPipe)
                {
                    return;
                }
                m_TrainPipe = value;
                RaisePropertyChanged(() => TrainPipe);
            }
        }

        public double ParkingCylinderOne
        {
            get { return m_ParkingCylinderOne; }
            set
            {
                if (value == m_ParkingCylinderOne)
                {
                    return;
                }
                m_ParkingCylinderOne = value;
                RaisePropertyChanged(() => ParkingCylinderOne);
            }
        }

        public double ParkingCylinderTwo
        {
            get { return m_ParkingCylinderTwo; }
            set
            {
                if (value == m_ParkingCylinderTwo)
                {
                    return;
                }
                m_ParkingCylinderTwo = value;
                RaisePropertyChanged(() => ParkingCylinderTwo);
            }
        }

        public double BalancingCylinderOne
        {
            get { return m_BalancingCylinderOne; }
            set
            {
                if (value == m_BalancingCylinderOne)
                {
                    return;
                }
                m_BalancingCylinderOne = value;
                RaisePropertyChanged(() => BalancingCylinderOne);
            }
        }

        public double BalancingCylinderTwo
        {
            get { return m_BalancingCylinderTwo; }
            set
            {
                if (value == m_BalancingCylinderTwo)
                {
                    return;
                }
                m_BalancingCylinderTwo = value;
                RaisePropertyChanged(() => BalancingCylinderTwo);
            }
        }

        public double BrakeCylinder
        {
            get { return m_BrakeCylinder; }
            set
            {
                if (value == m_BrakeCylinder)
                {
                    return;
                }
                m_BrakeCylinder = value;
                RaisePropertyChanged(() => BrakeCylinder);
            }
        }

        public string TrainNumber
        {
            get { return m_TrainNumber; }
            set
            {
                if (value == m_TrainNumber)
                {
                    return;
                }
                m_TrainNumber = value;
                RaisePropertyChanged(() => TrainNumber);
            }
        }

        public string CurrentState
        {
            get { return m_CurrentState; }
            set
            {
                if (value == m_CurrentState)
                {
                    return;
                }
                m_CurrentState = value;
                RaisePropertyChanged(() => CurrentState);
            }
        }

        public string CurrentNetworkFlow
        {
            get { return m_CurrentNetworkFlow; }
            set
            {
                if (value == m_CurrentNetworkFlow)
                {
                    return;
                }
                m_CurrentNetworkFlow = value;
                RaisePropertyChanged(() => CurrentNetworkFlow);
            }
        }

        public string BoardInformation
        {
            get { return m_BoardInformation; }
            set
            {
                if (value == m_BoardInformation)
                {
                    return;
                }
                m_BoardInformation = value;
                RaisePropertyChanged(() => BoardInformation);
            }
        }

        public string PressureTransmitter
        {
            get { return m_PressureTransmitter; }
            set
            {
                if (value == m_PressureTransmitter)
                {
                    return;
                }
                m_PressureTransmitter = value;
                RaisePropertyChanged(() => PressureTransmitter);
            }
        }

        public string FlowTransmitter
        {
            get { return m_FlowTransmitter; }
            set
            {
                if (value == m_FlowTransmitter)
                {
                    return;
                }
                m_FlowTransmitter = value;
                RaisePropertyChanged(() => FlowTransmitter);
            }
        }

        public string ParkingBrake
        {
            get { return m_ParkingBrake; }
            set
            {
                if (value == m_ParkingBrake)
                {
                    return;
                }
                m_ParkingBrake = value;
                RaisePropertyChanged(() => ParkingBrake);
            }
        }

        public string ReferSpeed
        {
            get { return m_ReferSpeed; }
            set
            {
                if (value == m_ReferSpeed)
                {
                    return;
                }
                m_ReferSpeed = value;
                RaisePropertyChanged(() => ReferSpeed);
            }
        }

        public string TransNum
        {
            get { return m_TransNum; }
            set
            {
                if (value == m_TransNum)
                {
                    return;
                }
                m_TransNum = value;
                RaisePropertyChanged(() => TransNum);
            }
        }

        public override void Clear()
        {
            base.Clear();
            var typ = typeof(IBrakeViewModel);
            base.Clear(typ, this);
            //foreach (var info in typ.GetProperties())
            //{
            //    if (info.PropertyType == typeof(string))
            //    {
            //        info.SetValue(this, "", null);
            //    }
            //}
        }
    }
}