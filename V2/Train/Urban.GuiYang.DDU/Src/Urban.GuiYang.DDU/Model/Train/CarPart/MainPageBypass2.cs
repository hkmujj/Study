using Urban.GuiYang.DDU.Config;
using Urban.GuiYang.DDU.Model.Constant;

namespace Urban.GuiYang.DDU.Model.Train.CarPart
{
    public class MainPageByPass2 : CarPartBase
    {

        private CarItem<CTSState, CarCoulplingStateConfig> m_CoulplingState;
        private CarItem<SwitchState, CarPantographEnableBypassConfig> m_PantographEnableBypass;
        private CarItem<SwitchState, CarATCCutOffBypassConfig> m_ATCCutOffBypass;
        private CarItem<SwitchState, CarDoorEnableBypassConfig> m_DoorEnableBypass;
        private CarItem<SwitchState, CarKeyBypassConfig> m_KeyBypass;

        public CarItem<CTSState, CarCoulplingStateConfig> CoulplingState
        {
            get { return m_CoulplingState; }
            set
            {
                if (Equals(value, m_CoulplingState))
                {
                    return;
                }
                m_CoulplingState = value;
                RaisePropertyChanged(() => CoulplingState);
            }
        }


        public CarItem<SwitchState, CarPantographEnableBypassConfig> PantographEnableBypass
        {
            get { return m_PantographEnableBypass; }
            set
            {
                if (Equals(value, m_PantographEnableBypass))
                {
                    return;
                }
                m_PantographEnableBypass = value;
                RaisePropertyChanged(() => PantographEnableBypass);
            }
        }


        public CarItem<SwitchState, CarATCCutOffBypassConfig> ATCCutOffBypass
        {
            get { return m_ATCCutOffBypass; }
            set
            {
                if (Equals(value, m_ATCCutOffBypass))
                {
                    return;
                }
                m_ATCCutOffBypass = value;
                RaisePropertyChanged(() => ATCCutOffBypass);
            }
        }


        public CarItem<SwitchState, CarDoorEnableBypassConfig> DoorEnableBypass
        {
            get { return m_DoorEnableBypass; }
            set
            {
                if (Equals(value, m_DoorEnableBypass))
                {
                    return;
                }
                m_DoorEnableBypass = value;
                RaisePropertyChanged(() => DoorEnableBypass);
            }
        }


        public CarItem<SwitchState, CarKeyBypassConfig> KeyBypass
        {
            get { return m_KeyBypass; }
            set
            {
                if (Equals(value, m_KeyBypass))
                {
                    return;
                }
                m_KeyBypass = value;
                RaisePropertyChanged(() => KeyBypass);
            }
        }

    }
}