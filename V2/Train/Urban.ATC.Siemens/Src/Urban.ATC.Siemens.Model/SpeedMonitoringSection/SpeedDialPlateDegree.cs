using Motor.ATP.Domain.Interface.SpeedMonitoringSection;

namespace Motor.ATP.Domain.Model.SpeedMonitoringSection
{
    public class SpeedDialPlateDegree : ISpeedDialPlateDegree
    {
        public SpeedDialPlateDegree(float speed, float angle, float lenght, string text = null)
        {
            Text = text;
            Lenght = lenght;
            Angle = angle;
            Speed = speed;
        }

        public float Speed { get; private set; }

        public float Angle { get; private set; }

        public float Lenght { get; private set; }

        public string Text { get; private set; }
    }
}