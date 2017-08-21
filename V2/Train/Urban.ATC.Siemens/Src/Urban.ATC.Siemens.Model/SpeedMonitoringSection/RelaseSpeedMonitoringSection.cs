using Motor.ATP.Domain.Interface.SpeedMonitoringSection;

namespace Motor.ATP.Domain.Model.SpeedMonitoringSection
{
    public class RelaseSpeedMonitoringSection : SpeedMonitoringSection
    {
        public RelaseSpeedMonitoringSection(ATPDomain parent)
            : base(parent)
        {
            Type = SpeedMonitoringSectionType.RSM;
        }
    }
}
