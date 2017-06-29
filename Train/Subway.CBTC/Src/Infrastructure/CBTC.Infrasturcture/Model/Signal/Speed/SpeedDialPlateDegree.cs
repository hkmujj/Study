using System.Diagnostics;

namespace CBTC.Infrasturcture.Model.Signal.Speed
{
    [DebuggerDisplay("Speed={Speed}, Angle={Angle}")]
    public class SpeedDialPlateDegree
    {
        [DebuggerStepThrough]
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