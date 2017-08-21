using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.Angola.TCMS.Model.Screen
{
    [Export]
    public class Other : NotificationObject
    {
        public Other()
        {
            NetTime = DateTime.Now;
        }

        private DateTime m_NetTime;
        private TimeSpan m_AdjustSpan;

        public DateTime NetTime
        {
            set
            {
                if (value.Equals(m_NetTime))
                {
                    return;
                }

                m_NetTime = value;
                RaisePropertyChanged(() => NetTime);
                RaisePropertyChanged(() => ShowingTime);
            }
            get { return m_NetTime; }
        }

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
                RaisePropertyChanged(() => ShowingTime);
            }
            get { return m_AdjustSpan; }
        }

        public DateTime ShowingTime
        {
            get { return NetTime + AdjustSpan; }
        }
    }
}