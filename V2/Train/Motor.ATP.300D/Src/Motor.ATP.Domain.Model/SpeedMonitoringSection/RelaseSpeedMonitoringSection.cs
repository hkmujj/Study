using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model.SpeedMonitoringSection
{
    public class RelaseSpeedMonitoringSection : SpeedMonitoringSectionBase
    {
        public RelaseSpeedMonitoringSection(ATPDomainBase parent)
            : base(parent)
        {
            this.Type = SpeedMonitoringSectionType.RSM;
        }
    }
}
