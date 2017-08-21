using ATPComControl.SpeedArc;

namespace Motor.ATP._200H.Common
{
    public class ATP200HConvert : ISpeedConverter
    {
        private readonly float m_StartAngle;
        private readonly float m_StartSpeed;
        private readonly float m_MaxSpeed;

        private const float AngleOf200 = 179.5f - 45;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ATP200HConvert()
        {
            m_StartAngle = -45;
            m_StartSpeed = 0;
            m_MaxSpeed = 400;
        }

        /// <summary>
        /// 角度转换函数
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
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
}