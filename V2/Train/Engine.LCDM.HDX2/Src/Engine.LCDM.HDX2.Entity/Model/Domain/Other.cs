using System;
using Engine.LCDM.HDX2.Resource;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.LCDM.HDX2.Entity.Model.Domain
{
    public class Other : NotificationObject, IResetable
    {
        private DateTime m_CurrentDateTime;
        private TimeSpan m_ShowTimeSpan;
        private double m_Opacity;
        private ResourceType m_ResourceType;

        /// <summary>
        /// 当前时间
        /// </summary>
        public DateTime CurrentDateTime
        {
            set
            {
                if (value.Equals(m_CurrentDateTime))
                {
                    return;
                }
                m_CurrentDateTime = value;
                RaisePropertyChanged(() => CurrentDateTime);
                RaisePropertyChanged(() => ShowingDateTime);
            }
            get { return m_CurrentDateTime; }
        }

        /// <summary>
        /// 显示时间和当前时间的差值
        /// </summary>
        public TimeSpan ShowTimeSpan
        {
            set
            {
                if (value.Equals(m_ShowTimeSpan))
                {
                    return;
                }
                m_ShowTimeSpan = value;
                RaisePropertyChanged(() => ShowTimeSpan);
                RaisePropertyChanged(() => ShowingDateTime);
            }
            get { return m_ShowTimeSpan; }
        }

        public DateTime ShowingDateTime { get { return CurrentDateTime + ShowTimeSpan; } }


        /// <summary>
        /// 不透明度， 0-1
        /// </summary>
        public double Opacity
        {
            set
            {
                if (value.Equals(m_Opacity))
                {
                    return;
                }
                m_Opacity = value;
                RaisePropertyChanged(() => Opacity);
            }
            get { return m_Opacity; }
        }


        public ResourceType ResourceType
        {
            set
            {
                if (value == m_ResourceType)
                {
                    return;
                }
                m_ResourceType = value;
                RaisePropertyChanged(() => ResourceType);
            }
            get { return m_ResourceType; }
        }

        public void ResetDatas()
        {
            ShowTimeSpan = TimeSpan.Zero;
        }
    }
}