using System;
using System.Diagnostics;
using Microsoft.Practices.Prism.ViewModel;
using Motor.ATP.Infrasturcture.Interface.SpeedMonitoringSection;

namespace Motor.ATP.Infrasturcture.Model
{
    [DebuggerDisplay("GradientType={Type}, SlopeValue={SlopeValue}, StartDistance={StartDistance}, EndDistance={EndDistance}")]
    public class SlopeInfomationItem : NotificationObject, IGradientInfomationItem
    {
        private GradientType m_Type;
        private float m_SlopeValue;
        private float m_StartDistance;
        private float m_EndDistance;

        [DebuggerStepThrough]
        public SlopeInfomationItem(IGradientInfomation parent, GradientType type)
        {
            Type = type;
            Parent = parent;
        }

        public IGradientInfomation Parent { get; private set; }

        public GradientType Type
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

        public float SlopeValue
        {
            get { return m_SlopeValue; }
            set
            {
                if (value.Equals(m_SlopeValue))
                {
                    return;
                }
                m_SlopeValue = value;
                RaisePropertyChanged(() => SlopeValue);
                RaisePropertyChanged(() => AbsSlopeValue);
            }
        }

        public float AbsSlopeValue { get { return Math.Abs(SlopeValue); } }

        public float StartDistance
        {
            get { return m_StartDistance; }
            set
            {
                if (value.Equals(m_StartDistance))
                {
                    return;
                }
                m_StartDistance = value;
                RaisePropertyChanged(() => StartDistance);
            }
        }

        public float EndDistance
        {
            get { return m_EndDistance; }
            set
            {
                if (value.Equals(m_EndDistance))
                {
                    return;
                }
                m_EndDistance = value;
                RaisePropertyChanged(() => EndDistance);
            }
        }
    }
}