using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.HXD3.Model.TCMS
{
    [Export]
    public class Other : NotificationObject
    {
        private LightLevel m_LightLevel;
        private DateTime m_SimTime;
        private TimeSpan m_AdjustSpan;

        public LightLevel LightLevel
        {
            set
            {
                if (value == m_LightLevel)
                {
                    return;
                }

                m_LightLevel = value;
                RaisePropertyChanged(() => LightLevel);
            }
            get { return m_LightLevel; }
        }

        /// <summary>
        /// 仿真时间
        /// </summary>
        public DateTime SimTime
        {
            set
            {
                if (value.Equals(m_SimTime))
                {
                    return;
                }

                m_SimTime = value;
                RaisePropertyChanged(() => SimTime);
                RaisePropertyChanged(() => ShowTime);
            }
            get { return m_SimTime; }
        }

        /// <summary>
        /// 修改时间的差值
        /// </summary>
        public TimeSpan AdjustSpan
        {
            set
            {
                if (value.Equals(m_AdjustSpan))
                {
                    return;
                }

                m_AdjustSpan = value;
                RaisePropertyChanged(() => AdjustSpan);
                RaisePropertyChanged(() => ShowTime);
            }
            get { return m_AdjustSpan; }
        }

        /// <summary>
        /// 显示时间
        /// </summary>
        public DateTime ShowTime { get { return SimTime + AdjustSpan; } }
    }
}