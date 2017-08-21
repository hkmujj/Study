using System;
using System.Diagnostics;

namespace Urban.Iran.DMI.Model
{
    [DebuggerDisplay("SpeedState={SpeedState}, Speed={Speed}")]
    public class BarCurveValue
    {
        private float m_Speed;

        [DebuggerStepThrough]
        public BarCurveValue(float minSpeed, float maxSpeed)
        {
            MinSpeed = minSpeed;
            MaxSpeed = maxSpeed;
        }

        public float Speed
        {
            set { m_Speed = Math.Max(MinSpeed, Math.Min(MaxSpeed, value)); }
            get { return m_Speed; }
        }

        public SpeedState SpeedState { set; get; }

        public float MaxSpeed { private set; get; }

        public float MinSpeed { private set; get; }
    }
}