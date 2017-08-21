using Microsoft.Practices.Prism.ViewModel;
using Motor.TCMS.CRH400BF.Config;
using Motor.TCMS.CRH400BF.Model.Constant;

namespace Motor.TCMS.CRH400BF.Model.Train.CartPart
{
    public class BrakeInfoPage : NotificationObject
    {
        private CarItem<BrakeInfoCommonState, CarAssistAirCompressorStateConfig> m_AssistAirCompressorState;
        private CarItem<BrakeInfoCommonState, CarBcu1StateConfig> m_Bcu1State;
        private CarItem<BrakeInfoCommonState, CarBcu2StateConfig> m_Bcu2State;
        private CarItem<float, CarBrakeCylinderPressConfig> m_BrakeCylinderPress;
        private CarItem<float, CarEmptySpring1PressConfig> m_EmptySpring1Press;
        private CarItem<float, CarEmptySpring2PressConfig> m_EmptySpring2Press;
        private CarItem<BrakeInfoCommonState, CarKeepBrakeStateConfig> m_KeepBrakeState;
        private CarItem<float, CarParkingCylinderPressConfig> m_ParkingCylinderPress;
        private CarItem<BrakeInfoCommonState, CarPercentBrakeStateConfig> m_PercentBrakeState;
        private CarItem<float, CarWindPipePressConfig> m_WindPipePress;
        
      
        public CarItem<BrakeInfoCommonState, CarAssistAirCompressorStateConfig> AssistAirCompressorState
        {
            get { return m_AssistAirCompressorState; }
            set
            {
                if (value == m_AssistAirCompressorState)
                {
                    return;
                }

                m_AssistAirCompressorState = value;
                RaisePropertyChanged(() => AssistAirCompressorState);
            }
        }

        public CarItem<BrakeInfoCommonState, CarBcu1StateConfig> Bcu1State
        {
            get { return m_Bcu1State; }
            set
            {
                if (value == m_Bcu1State)
                {
                    return;
                }

                m_Bcu1State = value;
                RaisePropertyChanged(() => Bcu1State);
            }
        }

        public CarItem<BrakeInfoCommonState, CarBcu2StateConfig> Bcu2State
        {
            get { return m_Bcu2State; }
            set
            {
                if (value == m_Bcu2State)
                {
                    return;
                }

                m_Bcu2State = value;
                RaisePropertyChanged(() => Bcu2State);
            }
        }

        public CarItem<float, CarBrakeCylinderPressConfig> BrakeCylinderPress
        {
            get { return m_BrakeCylinderPress; }
            set
            {
                if (value == m_BrakeCylinderPress)
                {
                    return;
                }

                m_BrakeCylinderPress = value;
                RaisePropertyChanged(() => BrakeCylinderPress);
            }
        }

        public CarItem<float, CarEmptySpring1PressConfig> EmptySpring1Press
        {
            get { return m_EmptySpring1Press; }
            set
            {
                if (value == m_EmptySpring1Press)
                {
                    return;
                }

                m_EmptySpring1Press = value;
                RaisePropertyChanged(() => EmptySpring1Press);
            }
        }



        public CarItem<float, CarEmptySpring2PressConfig> EmptySpring2Press
        {
            get { return m_EmptySpring2Press; }
            set
            {
                if (value == m_EmptySpring2Press)
                {
                    return;
                }

                m_EmptySpring2Press = value;
                RaisePropertyChanged(() => EmptySpring2Press);
            }
        }

        public CarItem<BrakeInfoCommonState, CarKeepBrakeStateConfig> KeepBrakeState
        {
            get { return m_KeepBrakeState; }
            set
            {
                if (value == m_KeepBrakeState)
                {
                    return;
                }

                m_KeepBrakeState = value;
                RaisePropertyChanged(() => KeepBrakeState);
            }
        }

        public CarItem<float, CarParkingCylinderPressConfig> ParkingCylinderPress
        {
            get { return m_ParkingCylinderPress; }
            set
            {
                if (value == m_ParkingCylinderPress)
                {
                    return;
                }

                m_ParkingCylinderPress = value;
                RaisePropertyChanged(() => ParkingCylinderPress);
            }
        }

        public CarItem<BrakeInfoCommonState, CarPercentBrakeStateConfig> PercentBrakeState
        {
            get { return m_PercentBrakeState; }
            set
            {
                if (value == m_PercentBrakeState)
                {
                    return;
                }

                m_PercentBrakeState = value;
                RaisePropertyChanged(() => PercentBrakeState);
            }
        }

        public CarItem<float, CarWindPipePressConfig> WindPipePress
        {
            get { return m_WindPipePress; }
            set
            {
                if (value == m_WindPipePress)
                {
                    return;
                }

                m_WindPipePress = value;
                RaisePropertyChanged(() => WindPipePress);
            }
        }

    }
}
