using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model.SpeedMonitoringSection
{
    public class NoneSpeedMonitoringSection :SpeedMonitoringSectionBase
    {
        public NoneSpeedMonitoringSection(ATPDomainBase parent)
            : base(parent)
        {
            this.Type = SpeedMonitoringSectionType.None;
        }
    }
}
