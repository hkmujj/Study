using Microsoft.Practices.Prism.ViewModel;
using Motor.TCMS.CRH400BF.Model.Constant;

namespace Motor.TCMS.CRH400BF.Model.Train.CartPart
{
    public class ValidateParkBrakeItem : NotificationObject
    {
        private ParkBrakeState m_ParkBrakeState;
        private ParkBrakeEffectState m_ParkBrakeEffectState;

        public ParkBrakeState ParkBrakeState
        {
            get { return m_ParkBrakeState; }
            set
            {
                if (value == m_ParkBrakeState)
                {
                    return;
                }

                m_ParkBrakeState = value;
                RaisePropertyChanged(() => ParkBrakeState);
            }
        }

        public ParkBrakeEffectState ParkBrakeEffectState
        {
            get { return m_ParkBrakeEffectState; }
            set
            {
                if (value == m_ParkBrakeEffectState)
                {
                    return;
                }

                m_ParkBrakeEffectState = value;
                RaisePropertyChanged(() => ParkBrakeEffectState);
            }
        }
    }
}
