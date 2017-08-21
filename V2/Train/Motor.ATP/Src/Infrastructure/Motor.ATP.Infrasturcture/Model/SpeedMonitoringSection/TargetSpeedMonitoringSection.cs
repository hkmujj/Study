using Motor.ATP.Infrasturcture.Interface.SpeedMonitoringSection;

namespace Motor.ATP.Infrasturcture.Model.SpeedMonitoringSection
{
    public class TargetSpeedMonitoringSection : SpeedMonitoringSection
    {
        public TargetSpeedMonitoringSection(ATPDomain parent)
            : base(parent)
        {
            Type = SpeedMonitoringSectionType.TSM;
        }

    }
}