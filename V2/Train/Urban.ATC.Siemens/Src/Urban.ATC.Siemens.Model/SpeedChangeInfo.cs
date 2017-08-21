using System;
using System.Diagnostics;
using Microsoft.Practices.Prism.ViewModel;
using Motor.ATP.Domain.Interface.SpeedMonitoringSection;

namespace Motor.ATP.Domain.Model
{
    [DebuggerDisplay("SpeedChangeType={SpeedChangeType}, Distance={Distance}")]
    public class SpeedChangeInfo : NotificationObject, ISpeedChangeInfo
    {
        private SpeedChangeType m_SpeedChangeType;
        private double m_Distance;

        [DebuggerStepThrough]
        public SpeedChangeInfo(SpeedChangeType speedChangeType, double distance = Double.MaxValue)
        {
            Distance = distance;
            SpeedChangeType = speedChangeType;
        }

        public SpeedChangeType SpeedChangeType
        {
            get { return m_SpeedChangeType; }
            set
            {
                if (value == m_SpeedChangeType)
                {
                    return;
                }
                m_SpeedChangeType = value;
                RaisePropertyChanged(() => SpeedChangeType);
            }
        }

        public double Distance
        {
            get { return m_Distance; }
            set
            {
                if (value.Equals(m_Distance))
                {
                    return;
                }
                m_Distance = value;
                RaisePropertyChanged(() => Distance);
            }
        }
    }
}