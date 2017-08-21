
using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model
{
    public class Speed :  IISpeed
    {
        public ITrainInfo Parent { get; set; }

        public float CurrentSpeed { get; set; }

        public float TargetSpeed { get; set; }

        public float ServiceBrakeInterventionSpeed { get; set; }

        public float EmergencyBrakeInterventionSpeed { get; set; }

        public float PermittedLimitSpeed { get; set; }

        public float WarningLimitSpeed { get; set; }
    }
}
