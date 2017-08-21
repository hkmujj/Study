using Motor.ATP.Infrasturcture.Interface.SpeedMonitoringSection;

namespace Motor.ATP.Infrasturcture.Model.SpeedMonitoringSection
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