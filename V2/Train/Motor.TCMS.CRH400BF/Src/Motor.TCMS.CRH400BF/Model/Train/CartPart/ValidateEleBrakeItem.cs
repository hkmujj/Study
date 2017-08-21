using Microsoft.Practices.Prism.ViewModel;
using Motor.TCMS.CRH400BF.Model.Constant;

namespace Motor.TCMS.CRH400BF.Model.Train.CartPart
{
    public class ValidateEleBrakeItem : NotificationObject
    {
        private EleBrakeState m_EleBrakeState;
        private EleBrakeEffectState m_EleBrakeEffectState;

        public EleBrakeState EleBrakeState
        {
            get { return m_EleBrakeState; }
            set
            {
                if (value == m_EleBrakeState)
                {
                    return;
                }

                m_EleBrakeState = value;
                RaisePropertyChanged(() => EleBrakeState);
            }
        }

        public EleBrakeEffectState EleBrakeEffectState
        {
            get { return m_EleBrakeEffectState; }
            set
            {
                if (value == m_EleBrakeEffectState)
                {
                    return;
                }

                m_EleBrakeEffectState = value;
                RaisePropertyChanged(() => EleBrakeEffectState);
            }
        }
    }
}
