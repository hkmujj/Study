using Motor.ATP.Domain.Interface.SpeedMonitoringSection;

namespace Motor.ATP.Domain.Model.SpeedMonitoringSection
{
    public class NoneSpeedMonitoringSection :SpeedMonitoringSection
    {
        public NoneSpeedMonitoringSection(ATPDomain parent)
            : base(parent)
        {
            Type = SpeedMonitoringSectionType.None;
        }
    }
}
