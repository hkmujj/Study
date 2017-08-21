using Microsoft.Practices.Prism.ViewModel;
using Motor.TCMS.CRH400BF.Model.Constant;

namespace Motor.TCMS.CRH400BF.Model.Train.CartPart
{
    public class ValidateAirBrakeItem : NotificationObject
    {
        private AirBrakeState m_AirBrakeState;
        private AirBrakeEffectState m_AirBrakeEffectState;

        public AirBrakeState AirBrakeState
        {
            get { return m_AirBrakeState; }
            set
            {
                if (value == m_AirBrakeState)
                {
                    return;
                }

                m_AirBrakeState = value;
                RaisePropertyChanged(() => AirBrakeState);
            }
        }

        public AirBrakeEffectState AirBrakeEffectState
        {
            get { return m_AirBrakeEffectState; }
            set
            {
                if (value == m_AirBrakeEffectState)
                {
                    return;
                }

                m_AirBrakeEffectState = value;
                RaisePropertyChanged(() => AirBrakeEffectState);
            }
        }
    }
}
