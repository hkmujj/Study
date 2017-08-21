using Urban.GuiYang.DDU.Config;
using Urban.GuiYang.DDU.Model.Constant;

namespace Urban.GuiYang.DDU.Model.Train.CarPart
{
    public class BrakePage1 : CarPartBase
    {

        private CarItem<IsolationValveState, CarBogieIsolationValveConfig> m_BogieIsolationValveState1;
        private CarItem<IsolationValveState, CarBogieIsolationValveConfig> m_BogieIsolationValveState2;
        private CarItem<IsolationValveState, CarPackingBrakeIsolationValveConfig> m_PackingBrakeIsolationValveState;
        private CarItem<AirCompreeState, CarAirCompreeConfig> m_AirCompreeState;
        private CarItem<ElectricRelayState, CarEmergBrakeRelayConfig> m_EmergBrakeRelayState1;
        private CarItem<ElectricRelayState, CarEmergBrakeRelayConfig> m_EmergBrakeRelayState2;


        public CarItem<IsolationValveState, CarBogieIsolationValveConfig> BogieIsolationValveState1
        {
            get { return m_BogieIsolationValveState1; }
            set
            {
                if (value == m_BogieIsolationValveState1)
                {
                    return;
                }

                m_BogieIsolationValveState1 = value;
                RaisePropertyChanged(() => BogieIsolationValveState1);
            }
        }

        public CarItem<IsolationValveState, CarBogieIsolationValveConfig> BogieIsolationValveState2
        {
            get { return m_BogieIsolationValveState2; }
            set
            {
                if (value == m_BogieIsolationValveState2)
                {
                    return;
                }

                m_BogieIsolationValveState2 = value;
                RaisePropertyChanged(() => BogieIsolationValveState2);
            }
        }

        public CarItem<IsolationValveState, CarPackingBrakeIsolationValveConfig> PackingBrakeIsolationValveState
        {
            get { return m_PackingBrakeIsolationValveState; }
            set
            {
                if (value == m_PackingBrakeIsolationValveState)
                {
                    return;
                }

                m_PackingBrakeIsolationValveState = value;
                RaisePropertyChanged(() => PackingBrakeIsolationValveState);
            }
        }


        public CarItem<AirCompreeState, CarAirCompreeConfig> AirCompreeState
        {
            get { return m_AirCompreeState; }
            set
            {
                if (value == m_AirCompreeState)
                {
                    return;
                }

                m_AirCompreeState = value;
                RaisePropertyChanged(() => AirCompreeState);
            }
        }

        public CarItem<ElectricRelayState, CarEmergBrakeRelayConfig> EmergBrakeRelayState1
        {
            get { return m_EmergBrakeRelayState1; }
            set
            {
                if (value == m_EmergBrakeRelayState1)
                {
                    return;
                }

                m_EmergBrakeRelayState1 = value;
                RaisePropertyChanged(() => EmergBrakeRelayState1);
            }
        }

        public CarItem<ElectricRelayState, CarEmergBrakeRelayConfig> EmergBrakeRelayState2
        {
            get { return m_EmergBrakeRelayState2; }
            set
            {
                if (value == m_EmergBrakeRelayState2)
                {
                    return;
                }

                m_EmergBrakeRelayState2 = value;
                RaisePropertyChanged(() => EmergBrakeRelayState2);
            }
        }




















        /*private AirCompreeState? m_AirCompreeState;
        private IsolationValveState? m_EmergBrakeIsolationValveState;
        private IsolationValveState? m_PackingBrakeIsolationValveState;
        private IsolationValveState? m_BogieIsolationValveState2;
        private IsolationValveState? m_BogieIsolationValveState1;
        private ElectricRelayState? m_EmergBrakeRelayState1;
        private ElectricRelayState? m_EmergBrakeRelayState2;

        public ElectricRelayState? EmergBrakeRelayState1
        {
            set
            {
                if (value == m_EmergBrakeRelayState1)
                {
                    return;
                }

                m_EmergBrakeRelayState1 = value;
                RaisePropertyChanged(() => EmergBrakeRelayState1);
            }
            get { return m_EmergBrakeRelayState1; }
        }

        public ElectricRelayState? EmergBrakeRelayState2
        {
            set
            {
                if (value == m_EmergBrakeRelayState2)
                {
                    return;
                }

                m_EmergBrakeRelayState2 = value;
                RaisePropertyChanged(() => EmergBrakeRelayState2);
            }
            get { return m_EmergBrakeRelayState2; }
        }

        public IsolationValveState? BogieIsolationValveState1
        {
            get { return m_BogieIsolationValveState1; }
            set
            {
                if (value == m_BogieIsolationValveState1)
                {
                    return;
                }

                m_BogieIsolationValveState1 = value;
                RaisePropertyChanged(() => BogieIsolationValveState1);
            }
        }

        public IsolationValveState? BogieIsolationValveState2
        {
            get { return m_BogieIsolationValveState2; }
            set
            {
                if (value == m_BogieIsolationValveState2)
                {
                    return;
                }

                m_BogieIsolationValveState2 = value;
                RaisePropertyChanged(() => BogieIsolationValveState2);
            }
        }

        public IsolationValveState? PackingBrakeIsolationValveState
        {
            get { return m_PackingBrakeIsolationValveState; }
            set
            {
                if (value == m_PackingBrakeIsolationValveState)
                {
                    return;
                }

                m_PackingBrakeIsolationValveState = value;
                RaisePropertyChanged(() => PackingBrakeIsolationValveState);
            }
        }

        public IsolationValveState? EmergBrakeIsolationValveState
        {
            get { return m_EmergBrakeIsolationValveState; }
            set
            {
                if (value == m_EmergBrakeIsolationValveState)
                {
                    return;
                }

                m_EmergBrakeIsolationValveState = value;
                RaisePropertyChanged(() => EmergBrakeIsolationValveState);
            }
        }

        public AirCompreeState? AirCompreeState
        {
            get { return m_AirCompreeState; }
            set
            {
                if (value == m_AirCompreeState)
                {
                    return;
                }

                m_AirCompreeState = value;
                RaisePropertyChanged(() => AirCompreeState);
            }
        }
*/
    }
}