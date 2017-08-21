using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urban.GuiYang.DDU.Config;
using Urban.GuiYang.DDU.Model.Constant;

namespace Urban.GuiYang.DDU.Model.Train.CarPart
{
    public class AssistPage : CarPartBase
    {
        private CarItem<SwitchState, CarPowerSwitchConfig> m_CarPowerSwitch;
        private CarItem<ValidateFloatStateItem, CarBatteryChargerStateConfig> m_BatteryChargerState;
        private CarItem<ValidateFloatItem, CarStorageBatterytemperatureConfig> m_StorageBatterytemperature;
        private CarItem<SwitchState, CarAssistLoadSwitchConfig> m_AssistLoadSwitch;
        private CarItem<ValidateFloatItem, CarStorageBatteryAConfig> m_StorageBatteryA;

        public CarItem<ValidateFloatItem, CarStorageBatteryAConfig> StorageBatteryA
        {
            get { return m_StorageBatteryA; }
            set
            {
                if (Equals(value, m_StorageBatteryA))
                {
                    return;
                }
                m_StorageBatteryA = value;
                RaisePropertyChanged(() => StorageBatteryA);
            }
        }

        public CarItem<SwitchState, CarAssistLoadSwitchConfig> AssistLoadSwitch
        {
            get { return m_AssistLoadSwitch; }
            set
            {
                if (Equals(value, m_AssistLoadSwitch))
                {
                    return;
                }
                m_AssistLoadSwitch = value;
                RaisePropertyChanged(() => AssistLoadSwitch);
            }
        }

        public CarItem<ValidateFloatItem, CarStorageBatterytemperatureConfig> StorageBatterytemperature
        {
            get { return m_StorageBatterytemperature; }
            set
            {
                if (Equals(value, m_StorageBatterytemperature))
                {
                    return;
                }
                m_StorageBatterytemperature = value;
                RaisePropertyChanged(() => StorageBatterytemperature);
            }
        }

        public CarItem<ValidateFloatStateItem, CarBatteryChargerStateConfig> BatteryChargerState
        {
            get { return m_BatteryChargerState; }
            set
            {
                if (Equals(value, m_BatteryChargerState))
                {
                    return;
                }
                m_BatteryChargerState = value;
                RaisePropertyChanged(() => BatteryChargerState);
            }
        }


        public CarItem<SwitchState, CarPowerSwitchConfig> CarPowerSwitch
        {
            get { return m_CarPowerSwitch; }
            set
            {
                if (Equals(value, m_CarPowerSwitch))
                {
                    return;
                }
                m_CarPowerSwitch = value;
                RaisePropertyChanged(() =>CarPowerSwitch);
            }
        }
    }
}
