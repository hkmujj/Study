using ATPComControl.SpeedArc;

namespace ATP200C
{
    public class Atp300CSpeedAngleConverter : ISpeedConverter
    {
        private readonly float m_StartAngle;
        private readonly float m_StartSpeed;
        private readonly float m_MaxSpeed;

        private const float AngleOf200 = 179.5f - 45;

        public Atp300CSpeedAngleConverter()
        {
            m_StartAngle = -45;
            m_StartSpeed = 0;
            m_MaxSpeed = 400;
        }

        public virtual float Convert(float speed)
        {
            var angle = m_StartAngle;
            if (speed - 0f >= float.Epsilon && 200f >= speed)
            {
                angle = (speed - m_StartSpeed) / (200 - m_StartSpeed) * (AngleOf200 - m_StartAngle) + m_StartAngle;
            }
            else if (speed > 200f && m_MaxSpeed >= speed)
            {
                angle = ( speed - 200 ) / ( m_MaxSpeed - 200 ) * 90 + AngleOf200;
            }
            else if (speed > m_MaxSpeed)
            {
                angle = 225;
            }
            return angle;
        }
    }

    class Atp300CSpeedAngleAcrConverter: Atp300CSpeedAngleConverter
    {
        public override float Convert(float speed)
        {
            return base.Convert(speed) - 180;
        }
    }
}