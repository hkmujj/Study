using Urban.GuiYang.DDU.Config;
using Urban.GuiYang.DDU.Model.Constant;

namespace Urban.GuiYang.DDU.Model.Train.CarPart
{
    public class TowPage : CarPartBase
    {
        private CarItem<GroundConnectState, CarGroundConnectConfig> m_GroundConnectState;

        private CarItem<ValidateFloatItem, CarElveConfig> m_ElveState;

        private CarItem<ValidateFloatItem, CarMotoTemperatureConfig> m_MotoTemperatureState1;

        private CarItem<ValidateFloatItem, CarMotoTemperatureConfig> m_MotoTemperatureState2;
        private CarItem<ValidateFloatItem, CarMotoTemperatureConfig> m_MotoTemperatureState3;

        private CarItem<ValidateFloatItem, CarMotoTemperatureConfig> m_MotoTemperatureState4;

        private CarItem<ValidateFloatItem, CarTowBrakeKNConfig> m_TowBrakeKNState;

        public CarItem<GroundConnectState, CarGroundConnectConfig> GroundConnectState
        {
            get { return m_GroundConnectState; }
            set
            {
                if (value == m_GroundConnectState)
                {
                    return;
                }

                m_GroundConnectState = value;
                RaisePropertyChanged(() => GroundConnectState);
            }
        }

        public CarItem<ValidateFloatItem, CarMotoTemperatureConfig> MotoTemperatureState1
        {
            get { return m_MotoTemperatureState1; }
            set
            {
                if (value == m_MotoTemperatureState1)
                {
                    return;
                }

                m_MotoTemperatureState1 = value;
                RaisePropertyChanged(() => MotoTemperatureState1);
            }
        }

        public CarItem<ValidateFloatItem, CarMotoTemperatureConfig> MotoTemperatureState2
        {
            get { return m_MotoTemperatureState2; }
            set
            {
                if (value == m_MotoTemperatureState2)
                {
                    return;
                }

                m_MotoTemperatureState2 = value;
                RaisePropertyChanged(() => MotoTemperatureState2);
            }
        }

        public CarItem<ValidateFloatItem, CarMotoTemperatureConfig> MotoTemperatureState3
        {
            get { return m_MotoTemperatureState3; }
            set
            {
                if (value == m_MotoTemperatureState3)
                {
                    return;
                }

                m_MotoTemperatureState3 = value;
                RaisePropertyChanged(() => MotoTemperatureState3);
            }
        }

        public CarItem<ValidateFloatItem, CarMotoTemperatureConfig> MotoTemperatureState4
        {
            get { return m_MotoTemperatureState4; }
            set
            {
                if (value == m_MotoTemperatureState4)
                {
                    return;
                }

                m_MotoTemperatureState4 = value;
                RaisePropertyChanged(() => MotoTemperatureState4);
            }
        }
        public CarItem<ValidateFloatItem, CarTowBrakeKNConfig> TowBrakeKNState
        {
            get { return m_TowBrakeKNState; }
            set
            {
                if (value == m_TowBrakeKNState)
                {
                    return;
                }

                m_TowBrakeKNState = value;
                RaisePropertyChanged(() => TowBrakeKNState);
            }
        }

        public CarItem<ValidateFloatItem, CarElveConfig> ElveState
        {
            get { return m_ElveState; }
            set
            {
                if (value == m_ElveState)
                {
                    return;
                }

                m_ElveState = value;
                RaisePropertyChanged(() => ElveState);
            }
        }
    }
}