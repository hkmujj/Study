using System;
using System.Diagnostics;
using Engine.TCMS.HXD3.Model.ConfigModel;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.HXD3.ViewModel.Domain.Detail
{
    public class FaultItem : NotificationObject
    {
        private float m_Speed;
        private float m_Level;
        private float m_Votage;
        private DateTime m_ResumeDateTime;
        private DateTime m_OccuseDateTime;
        private FaultItemConfig m_ItemConfig;
        private bool m_IsSelected;

        [DebuggerStepThrough]
        public FaultItem(FaultItemConfig itemConfig)
        {
            ItemConfig = itemConfig;
        }

        public FaultItemConfig ItemConfig
        {
            private set
            {
                if (Equals(value, m_ItemConfig))
                {
                    return;
                }

                m_ItemConfig = value;
                RaisePropertyChanged(() => ItemConfig);
            }
            get { return m_ItemConfig; }
        }

        public bool IsSelected
        {
            set
            {
                if (value == m_IsSelected)
                {
                    return;
                }

                m_IsSelected = value;
                RaisePropertyChanged(() => IsSelected);
            }
            get { return m_IsSelected; }
        }

        public DateTime OccuseDateTime
        {
            set
            {
                if (value.Equals(m_OccuseDateTime))
                {
                    return;
                }

                m_OccuseDateTime = value;
                RaisePropertyChanged(() => OccuseDateTime);
            }
            get { return m_OccuseDateTime; }
        }

        public DateTime ResumeDateTime
        {
            set
            {
                if (value.Equals(m_ResumeDateTime))
                {
                    return;
                }

                m_ResumeDateTime = value;
                RaisePropertyChanged(() => ResumeDateTime);
            }
            get { return m_ResumeDateTime; }
        }

        public float Votage
        {
            set
            {
                if (value.Equals(m_Votage))
                {
                    return;
                }

                m_Votage = value;
                RaisePropertyChanged(() => Votage);
            }
            get { return m_Votage; }
        }

        public float Level
        {
            set
            {
                if (value.Equals(m_Level))
                {
                    return;
                }

                m_Level = value;
                RaisePropertyChanged(() => Level);
            }
            get { return m_Level; }
        }

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