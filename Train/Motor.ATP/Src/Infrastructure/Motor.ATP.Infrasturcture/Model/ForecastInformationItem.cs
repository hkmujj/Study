using System;
using System.Diagnostics;
using Microsoft.Practices.Prism.ViewModel;
using Motor.ATP.Infrasturcture.Interface;

namespace Motor.ATP.Infrasturcture.Model
{
    [DebuggerDisplay("Type={Type}, Distance={Distance}")]
    public class ForecastInformationItem : NotificationObject, IForecastInformationItem
    {
        private float m_Distance;
        private ForecastInformationType m_Type;

        /// <summary>
        /// 距离最小的变化值
        /// </summary>
        private static readonly float DistanceMinStep = 10;

        [DebuggerStepThrough]
        public ForecastInformationItem(ForecastInformationType type = ForecastInformationType.None, float distance = float.MaxValue)
        {
            Distance = distance;
            Type = type;
        }

        public ForecastInformationType Type
        {
            get { return m_Type; }
            set
            {
                if (value == m_Type)
                {
                    return;
                }
                m_Type = value;
                RaisePropertyChanged(() => Type);
            }
        }

        public float Distance
        {
            get { return m_Distance; }
            set
            {
                // 过滤变化太小的数据
                if (Math.Abs(m_Distance - value) <= DistanceMinStep && Math.Abs(value) > float.Epsilon)
                {
                    return;
                }

                if (value.Equals(m_Distance))
                {
                    return;
                }
                m_Distance = value;
                RaisePropertyChanged(() => Distance);
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="type"></param>
        /// <param name="distance"></param>
        public void Update(ForecastInformationType type, float distance)
        {
            Type = type;
            Distance = distance;
        }
    }
}