using System.Diagnostics;

namespace Urban.Iran.DMI.Model
{
    public class LineSpeedAngleCollection : ISpeedAngleCollection
    {
        public float MinAngle { get; private set; }
        public float MaxAngle { get; private set; }
        public float MinSpeed { get; private set; }
        public float MaxSpeed { get; private set; }

        [DebuggerStepThrough]
        public LineSpeedAngleCollection(float minAngle, float maxAngle, float minSpeed, float maxSpeed)
        {
            MinAngle = minAngle;
            MaxAngle = maxAngle;
            MinSpeed = minSpeed;
            MaxSpeed = maxSpeed;
        }

        public float ConvertToAngle(float speed)
        {
            return speed * (MaxAngle - MinAngle) / (MaxSpeed - MinSpeed) + MinAngle;
        }
    }
}