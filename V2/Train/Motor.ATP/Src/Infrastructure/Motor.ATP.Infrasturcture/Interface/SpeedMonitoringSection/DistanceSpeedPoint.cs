using System.Diagnostics;
using Microsoft.Practices.Prism.ViewModel;

namespace Motor.ATP.Infrasturcture.Interface.SpeedMonitoringSection
{
    /// <summary>
    /// 距离速度点.
    /// </summary>
    [DebuggerDisplay("Distance = {Distance} Speed = {Speed}")]
    public class DistanceSpeedPoint : NotificationObject
    {
        private float m_Distance;
        private float m_Speed;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="speed"></param>
        [DebuggerStepThrough]
        public DistanceSpeedPoint(float distance = 0f, float speed = 0f)
        {
            Speed = speed;
            Distance = distance;
        }

        /// <summary>
        /// 距离, X 轴
        /// </summary>
        public float Distance
        {
            set
            {
                if (value.Equals(m_Distance))
                {
                    return;
                }
                m_Distance = value;
                RaisePropertyChanged(() => Distance);
            }
            get { return m_Distance; }
        }

        /// <summary>
        /// 速度 , Y 轴
        /// </summary>
        public float Speed
        {
            set
            {
                if (value.Equals(m_Speed))
                {
                    return;
                }
                m_Speed = value;
                RaisePropertyChanged(() => Speed);
            }
            get { return m_Speed; }
        }
    }
}