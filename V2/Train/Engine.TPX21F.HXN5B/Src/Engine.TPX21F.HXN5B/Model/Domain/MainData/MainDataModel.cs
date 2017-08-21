using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Model.Domain.Constant;
using Engine.TPX21F.HXN5B.Model.Domain.MainData.Detail;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.Model.Domain.MainData
{
    [Export]
    public class MainDataModel : NotificationObject
    {
        private double m_AirFlowRate;
        private double m_BrakeReservoirPressure;
        private double m_MainReservoirPressure;
        private double m_OilSize;
        private double m_CurrentSpeed;
        private double m_CurrentWorkRate;
        private double m_Mileage;
        private double m_TowBrakeNewt;
        private double m_BrakePipe;
        private double m_BalanceAirCylinder;
        private double m_OilEnginRotationRate;
        private MainStates m_MainStates;
        private AcceleratedSpeedState m_AcceleratedSpeedState;
        private double m_BrakeCylinderPressure;

        [ImportingConstructor]
        public MainDataModel(MainStates mainStates)
        {
            MainStates = mainStates;
        }

        public MainStates MainStates
        {
            private set
            {
                if (Equals(value, m_MainStates))
                {
                    return;
                }

                m_MainStates = value;
                RaisePropertyChanged(() => MainStates);
            }
            get { return m_MainStates; }
        }

        /// <summary>
        /// 制动缸压力
        /// </summary>
        public double BrakeCylinderPressure
        {
            get { return m_BrakeCylinderPressure; }
            set
            {
                if (value.Equals(m_BrakeCylinderPressure))
                {
                    return;
                }

                m_BrakeCylinderPressure = value;
                RaisePropertyChanged(() => BrakeCylinderPressure);
            }
        }

        public double OilEnginRotationRate
        {
            set
            {
                if (value.Equals(m_OilEnginRotationRate))
                {
                    return;
                }

                m_OilEnginRotationRate = value;
                RaisePropertyChanged(() => OilEnginRotationRate);
            }
            get { return m_OilEnginRotationRate; }
        }

        public double BalanceAirCylinder
        {
            set
            {
                if (value.Equals(m_BalanceAirCylinder))
                {
                    return;
                }

                m_BalanceAirCylinder = value;
                RaisePropertyChanged(() => BalanceAirCylinder);
            }
            get { return m_BalanceAirCylinder; }
        }

        public double BrakePipe
        {
            set
            {
                if (value.Equals(m_BrakePipe))
                {
                    return;
                }

                m_BrakePipe = value;
                RaisePropertyChanged(() => BrakePipe);
            }
            get { return m_BrakePipe; }
        }

        public double TowBrakeNewt
        {
            set
            {
                if (value.Equals(m_TowBrakeNewt))
                {
                    return;
                }

                m_TowBrakeNewt = value;
                RaisePropertyChanged(() => TowBrakeNewt);
            }
            get { return m_TowBrakeNewt; }
        }

        public double Mileage
        {
            set
            {
                if (value.Equals(m_Mileage))
                {
                    return;
                }

                m_Mileage = value;
                RaisePropertyChanged(() => Mileage);
            }
            get { return m_Mileage; }
        }

        public AcceleratedSpeedState AcceleratedSpeedState
        {
            set
            {
                if (value == m_AcceleratedSpeedState)
                {
                    return;
                }

                m_AcceleratedSpeedState = value;
                RaisePropertyChanged(() => AcceleratedSpeedState);
            }
            get { return m_AcceleratedSpeedState; }
        }

        public double CurrentWorkRate
        {
            get { return m_CurrentWorkRate; }
            set
            {
                if (value.Equals(m_CurrentWorkRate))
                {
                    return;
                }

                m_CurrentWorkRate = value;
                RaisePropertyChanged(() => CurrentWorkRate);
            }
        }

        public double CurrentSpeed
        {
            get { return m_CurrentSpeed; }
            set
            {
                if (value.Equals(m_CurrentSpeed))
                {
                    return;
                }

                m_CurrentSpeed = value;
                RaisePropertyChanged(() => CurrentSpeed);
            }
        }

        public double OilSize
        {
            get { return m_OilSize; }
            set
            {
                if (value.Equals(m_OilSize))
                {
                    return;
                }

                m_OilSize = value;
                RaisePropertyChanged(() => OilSize);
            }
        }

        public double MainReservoirPressure
        {
            get { return m_MainReservoirPressure; }
            set
            {
                if (value.Equals(m_MainReservoirPressure))
                {
                    return;
                }

                m_MainReservoirPressure = value;
                RaisePropertyChanged(() => MainReservoirPressure);
            }
        }

        public double BrakeReservoirPressure
        {
            get { return m_BrakeReservoirPressure; }
            set
            {
                if (value.Equals(m_BrakeReservoirPressure))
                {
                    return;
                }

                m_BrakeReservoirPressure = value;
                RaisePropertyChanged(() => BrakeReservoirPressure);
            }
        }

        public double AirFlowRate
        {
            get { return m_AirFlowRate; }
            set
            {
                if (value.Equals(m_AirFlowRate))
                {
                    return;
                }

                m_AirFlowRate = value;
                RaisePropertyChanged(() => AirFlowRate);
            }
        }
    }
}