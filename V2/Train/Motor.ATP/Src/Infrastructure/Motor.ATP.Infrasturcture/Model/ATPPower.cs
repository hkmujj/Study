using MMI.Facility.Interface;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Service;

namespace Motor.ATP.Infrasturcture.Model
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