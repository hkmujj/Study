using Motor.ATP.Infrasturcture.Interface.SpeedMonitoringSection;

namespace Motor.ATP.Infrasturcture.Model.SpeedMonitoringSection
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
