using Microsoft.Practices.Prism.ViewModel;
using Motor.TCMS.CRH400BF.Config;
using Motor.TCMS.CRH400BF.Model.Constant;

namespace Motor.TCMS.CRH400BF.Model.Train.CartPart
{
    public class EquipmentCutOffPage : NotificationObject
    {
        private CarItem<PantographState, CarPantographConfig> m_PantographState;
        private CarItem<MainBreakerState, CarMainBreakerConfig> m_MainBreakerState;
        private CarItem<HighPressSwitchState, CarHighPressSwitchConfig> m_HighPressSwitchState;
        private CarItem<TractionConverterState, CarTractionConverterConfig> m_TractionConverterState;
        private CarItem<TractionInvertorState, CarTractionInvertorConfig> m_TractionInvertorState;
        private CarItem<AssistConverterState, CarAssistConverterConfig> m_AssistConverterState;
        private CarItem<BatteryChargerState, CarBatteryChargerConfig> m_BatteryChargerState1;
        private CarItem<BatteryChargerState, CarBatteryChargerConfig> m_BatteryChargerState2;
        private CarItem<AirCompressorState, CarAirCompressorConfig> m_AirCompressorState;


        public CarItem<PantographState, CarPantographConfig> PantographState
        {
            get { return m_PantographState; }
            set
            {
                if (value == m_PantographState)
                {
                    return;
                }

                m_PantographState = value;
                RaisePropertyChanged(() => PantographState);
            }
        }
        public CarItem<MainBreakerState, CarMainBreakerConfig> MainBreakerState
        {
            get { return m_MainBreakerState; }
            set
            {
                if (value == m_MainBreakerState)
                {
                    return;
                }

                m_MainBreakerState = value;
                RaisePropertyChanged(() => MainBreakerState);
            }
        }

        public CarItem<HighPressSwitchState, CarHighPressSwitchConfig> HighPressSwitchState
        {
            get { return m_HighPressSwitchState; }
            set
            {
                if (value == m_HighPressSwitchState)
                {
                    return;
                }

                m_HighPressSwitchState = value;
                RaisePropertyChanged(() => HighPressSwitchState);
            }
        }

        public CarItem<TractionConverterState, CarTractionConverterConfig> TractionConverterState
        {
            get { return m_TractionConverterState; }
            set
            {
                if (value == m_TractionConverterState)
                {
                    return;
                }

                m_TractionConverterState = value;
                RaisePropertyChanged(() => TractionConverterState);
            }
        }

        public CarItem<TractionInvertorState, CarTractionInvertorConfig> TractionInvertorState
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

        public CarItem<AssistConverterState, CarAssistConverterConfig> AssistConverterState
        {
            get { return m_AssistConverterState; }
            set
            {
                if (value == m_AssistConverterState)
                {
                    return;
                }

                m_AssistConverterState = value;
                RaisePropertyChanged(() => AssistConverterState);
            }
        }

        public CarItem<BatteryChargerState, CarBatteryChargerConfig> BatteryChargerState1
        {
            get { return m_BatteryChargerState1; }
            set
            {
                if (value == m_BatteryChargerState1)
                {
                    return;
                }

                m_BatteryChargerState1 = value;
                RaisePropertyChanged(() => BatteryChargerState1);
            }
        }

        public CarItem<BatteryChargerState, CarBatteryChargerConfig> BatteryChargerState2
        {
            get { return m_BatteryChargerState2; }
            set
            {
                if (value == m_BatteryChargerState2)
                {
                    return;
                }

                m_BatteryChargerState2 = value;
                RaisePropertyChanged(() => BatteryChargerState2);
            }
        }

        public CarItem<AirCompressorState, CarAirCompressorConfig> AirCompressorState
        {
            get { return m_AirCompressorState; }
            set
            {
                if (value == m_AirCompressorState)
                {
                    return;
                }

                m_AirCompressorState = value;
                RaisePropertyChanged(() => AirCompressorState);
            }
        }

    }
}
