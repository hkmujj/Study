using Urban.GuiYang.DDU.Config;
using Urban.GuiYang.DDU.Model.Constant;

namespace Urban.GuiYang.DDU.Model.Train.CarPart
{
    public class MainPageByPass1 : CarPartBase
    {
        private CarItem<SwitchState, CarMainWindLowPresByPassConfig> m_MainWindLowPresByPass;
        private CarItem<SwitchState, CarAlertBypassConfig> m_AlertBypass;
        private CarItem<SwitchState, CarParkingRelaseBypassConfig> m_ParkingRelaseBypass;
        private CarItem<SwitchState, CarNormalBrakeBypassConfig> m_NormalBrakeBypass;
        private CarItem<SwitchState, CarDoorBypassConfig> m_DoorBypass;

        public CarItem<SwitchState, CarDoorBypassConfig> DoorBypass
        {
            get { return m_DoorBypass; }
            set
            {
                if (Equals(value, m_DoorBypass))
                {
                    return;
                }
                m_DoorBypass = value;
                RaisePropertyChanged(() => DoorBypass);
            }
        }
        public CarItem<SwitchState, CarNormalBrakeBypassConfig> NormalBrakeBypass
        {
            get { return m_NormalBrakeBypass; }
            set
            {
                if (Equals(value, m_NormalBrakeBypass))
                {
                    return;
                }
                m_NormalBrakeBypass = value;
                RaisePropertyChanged(() => NormalBrakeBypass);
            }
        }
        public CarItem<SwitchState, CarParkingRelaseBypassConfig> ParkingRelaseBypass
        {
            get { return m_ParkingRelaseBypass; }
            set
            {
                if (Equals(value, m_ParkingRelaseBypass))
                {
                    return;
                }
                m_ParkingRelaseBypass = value;
                RaisePropertyChanged(() => ParkingRelaseBypass);
            }
        }
        public CarItem<SwitchState, CarMainWindLowPresByPassConfig> MainWindLowPresByPass
        {
            get { return m_MainWindLowPresByPass; }
            set
            {
                if (Equals(value, m_MainWindLowPresByPass))
                {
                    return;
                }
                m_MainWindLowPresByPass = value;
                RaisePropertyChanged(() => MainWindLowPresByPass);
            }
        }
        public CarItem<SwitchState, CarAlertBypassConfig> AlertBypass
        {
            get { return m_AlertBypass; }
            set
            {
                if (Equals(value, m_AlertBypass))
                {
                    return;
                }
                m_AlertBypass = value;
                RaisePropertyChanged(() => AlertBypass);
            }
        }
    }
}