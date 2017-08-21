using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model.SpeedMonitoringSection
{
    public class SpeedMonitoringSectionBase : ATPPartialBase, ISpeedMonitoringSection
    {
        public SpeedMonitoringSectionBase(ATPDomainBase parent) : base(parent)
        {
        }

        public SpeedMonitoringSectionType Type { get; protected set; }

        public ISpeedProfile SpeedProfile { get; set; }

        public double BrakingStartPoint { get; set; }
    }
}
