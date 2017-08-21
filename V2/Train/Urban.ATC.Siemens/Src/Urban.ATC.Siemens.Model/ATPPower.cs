using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model
{
    public class ATPPower : ATPPartialBase, IATPPower
    {
        private ATPPowerState m_ATPPowerState;

        public ATPPower(ATPDomain parent)
            : base(parent)
        {
            ATPPowerState = ATPPowerState.Stopped;
        }

        public ATPPowerState ATPPowerState
        {
            get { return m_ATPPowerState; }
            set
            {
                if (value == m_ATPPowerState)
                {
                    return;
                }
                m_ATPPowerState = value;
                RaisePropertyChanged(() => ATPPowerState);
            }
        }
    }
}