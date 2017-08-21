using Urban.GuiYang.DDU.Config;
using Urban.GuiYang.DDU.Model.Constant;

namespace Urban.GuiYang.DDU.Model.Train.CarPart
{
    public class MainViewPage : CarPartBase
    {
        private CarItem<TractionInvertorState, CarTowInvertorConfig> m_TractionInvertorState;
        private CarItem<ParkingBrakeState, CarParkingBrakeConfig> m_ParkingBrake;
        private CarItem<NormalBrakeState, CarNormalBrakeConfig> m_NormalBrake2;
        private CarItem<NormalBrakeState, CarNormalBrakeConfig> m_NormalBrake1;
        private CarItem<SwitchState, CarHightSwitchConfig> m_HightSwitchState1;
        private CarItem<SwitchState, CarHightSwitchConfig> m_HightSwitchState2;
        private CarItem<AssistInvertorState, CarAssistInvertorConfig> m_AssistInvertorState;

        public CarItem<TractionInvertorState, CarTowInvertorConfig> TractionInvertorState
        {
            get { return m_TractionInvertorState; }
            set
            {
                if (value == m_TractionInvertorState)
                {
                    return;
                }

                m_TractionInvertorState = value;
                RaisePropertyChanged(() => TractionInvertorState);
            }
        }

        public CarItem<SwitchState, CarHightSwitchConfig> HightSwitchState1
        {
            get { return m_HightSwitchState1; }
            set
            {
                if (value == m_HightSwitchState1)
                {
                    return;
                }

                m_HightSwitchState1 = value;
                RaisePropertyChanged(() => HightSwitchState1);
            }
        }

        public CarItem<SwitchState, CarHightSwitchConfig> HightSwitchState2
        {
            get { return m_HightSwitchState2; }
            set
            {
                if (value == m_HightSwitchState2)
                {
                    return;
                }

                m_HightSwitchState2 = value;
                RaisePropertyChanged(() => HightSwitchState2);
            }
        }




        public CarItem<NormalBrakeState, CarNormalBrakeConfig> NormalBrake1
        {
            get { return m_NormalBrake1; }
            set
            {
                if (value == m_NormalBrake1)
                {
                    return;
                }

                m_NormalBrake1 = value;
                RaisePropertyChanged(() => NormalBrake1);
            }
        }

        public CarItem<NormalBrakeState, CarNormalBrakeConfig> NormalBrake2
        {
            get { return m_NormalBrake2; }
            set
            {
                if (value == m_NormalBrake2)
                {
                    return;
                }

                m_NormalBrake2 = value;
                RaisePropertyChanged(() => NormalBrake2);
            }
        }

        public CarItem<ParkingBrakeState, CarParkingBrakeConfig> ParkingBrake
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

        public CarItem<AssistInvertorState, CarAssistInvertorConfig> AssistInvertorState
        {
            get { return m_AssistInvertorState; }
            set
            {
                if (value == m_AssistInvertorState)
                {
                    return;
                }

                m_AssistInvertorState = value;
                RaisePropertyChanged(() => AssistInvertorState);
            }
        }
    }
}