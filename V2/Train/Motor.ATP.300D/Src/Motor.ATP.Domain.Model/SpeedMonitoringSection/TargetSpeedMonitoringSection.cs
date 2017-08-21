using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model.SpeedMonitoringSection
{
    public class TargetSpeedMonitoringSection : SpeedMonitoringSectionBase
    {
        public TargetSpeedMonitoringSection(ATPDomainBase parent)
            : base(parent)
        {
            Type = SpeedMonitoringSectionType.TSM;
        }

    }
}