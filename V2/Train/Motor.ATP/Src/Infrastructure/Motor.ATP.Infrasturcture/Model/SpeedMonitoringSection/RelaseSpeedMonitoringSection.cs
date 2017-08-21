using Motor.ATP.Infrasturcture.Interface.SpeedMonitoringSection;

namespace Motor.ATP.Infrasturcture.Model.SpeedMonitoringSection
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
