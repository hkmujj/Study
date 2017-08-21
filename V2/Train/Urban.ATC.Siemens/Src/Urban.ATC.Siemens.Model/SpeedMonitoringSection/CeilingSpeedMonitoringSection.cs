using Motor.ATP.Domain.Interface.SpeedMonitoringSection;

namespace Motor.ATP.Domain.Model.SpeedMonitoringSection
{
    public class CeilingSpeedMonitoringSection : SpeedMonitoringSection
    {
        public CeilingSpeedMonitoringSection(ATPDomain parent)
            : base(parent)
        {
            Type = SpeedMonitoringSectionType.CSM;
        }
    }
}