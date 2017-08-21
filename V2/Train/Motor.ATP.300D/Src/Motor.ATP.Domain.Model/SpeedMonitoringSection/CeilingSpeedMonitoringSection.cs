using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model.SpeedMonitoringSection
{
    public class CeilingSpeedMonitoringSection : SpeedMonitoringSectionBase
    {
        public CeilingSpeedMonitoringSection(ATPDomainBase parent)
            : base(parent)
        {
            Type = SpeedMonitoringSectionType.CSM;
        }
    }
}