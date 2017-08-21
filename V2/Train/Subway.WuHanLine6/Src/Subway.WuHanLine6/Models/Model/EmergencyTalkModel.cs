using System.ComponentModel.Composition;
using Subway.WuHanLine6.Models.States;

namespace Subway.WuHanLine6.Models.Model
{
    /// <summary>
    /// 紧急报警Model
    /// </summary>
    [Export]
    public class EmergencyTalkModel : ModelBase
    {
        private EmergencyTalkState m_StateF0062;
        private EmergencyTalkState m_StateF0052;
        private EmergencyTalkState m_StateF0042;
        private EmergencyTalkState m_StateF0032;
        private EmergencyTalkState m_StateF0022;
        private EmergencyTalkState m_StateF0012;
        private EmergencyTalkState m_StateF0061;
        private EmergencyTalkState m_StateF0051;
        private EmergencyTalkState m_StateF0041;
        private EmergencyTalkState m_StateF0031;
        private EmergencyTalkState m_StateF0021;
        private EmergencyTalkState m_StateF0011;

        /// <summary>
        /// 状态
        /// </summary>
        public EmergencyTalkState StateF0011
        {
            get { return m_StateF0011; }
            set
            {
                if (value == m_StateF0011)
                {
                    return;
                }
                m_StateF0011 = value;
                RaisePropertyChanged(() => StateF0011);
            }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public EmergencyTalkState StateF0021
        {
            get { return m_StateF0021; }
            set
            {
                if (value == m_StateF0021)
                {
                    return;
                }
                m_StateF0021 = value;
                RaisePropertyChanged(() => StateF0021);
            }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public EmergencyTalkState StateF0031
        {
            get { return m_StateF0031; }
            set
            {
                if (value == m_StateF0031)
                {
                    return;
                }
                m_StateF0031 = value;
                RaisePropertyChanged(() => StateF0031);
            }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public EmergencyTalkState StateF0041
        {
            get { return m_StateF0041; }
            set
            {
                if (value == m_StateF0041)
                {
                    return;
                }
                m_StateF0041 = value;
                RaisePropertyChanged(() => StateF0041);
            }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public EmergencyTalkState StateF0051
        {
            get { return m_StateF0051; }
            set
            {
                if (value == m_StateF0051)
                {
                    return;
                }
                m_StateF0051 = value;
                RaisePropertyChanged(() => StateF0051);
            }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public EmergencyTalkState StateF0061
        {
            get { return m_StateF0061; }
            set
            {
                if (value == m_StateF0061)
                {
                    return;
                }
                m_StateF0061 = value;
                RaisePropertyChanged(() => StateF0061);
            }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public EmergencyTalkState StateF0012
        {
            get { return m_StateF0012; }
            set
            {
                if (value == m_StateF0012)
                {
                    return;
                }
                m_StateF0012 = value;
                RaisePropertyChanged(() => StateF0012);
            }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public EmergencyTalkState StateF0022
        {
            get { return m_StateF0022; }
            set
            {
                if (value == m_StateF0022)
                {
                    return;
                }
                m_StateF0022 = value;
                RaisePropertyChanged(() => StateF0022);
            }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public EmergencyTalkState StateF0032
        {
            get { return m_StateF0032; }
            set
            {
                if (value == m_StateF0032)
                {
                    return;
                }
                m_StateF0032 = value;
                RaisePropertyChanged(() => StateF0032);
            }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public EmergencyTalkState StateF0042
        {
            get { return m_StateF0042; }
            set
            {
                if (value == m_StateF0042)
                {
                    return;
                }
                m_StateF0042 = value;
                RaisePropertyChanged(() => StateF0042);
            }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public EmergencyTalkState StateF0052
        {
            get { return m_StateF0052; }
            set
            {
                if (value == m_StateF0052)
                {
                    return;
                }
                m_StateF0052 = value;
                RaisePropertyChanged(() => StateF0052);
            }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public EmergencyTalkState StateF0062
        {
            get { return m_StateF0062; }
            set
            {
                if (value == m_StateF0062)
                {
                    return;
                }
                m_StateF0062 = value;
                RaisePropertyChanged(() => StateF0062);
            }
        }
    }
}