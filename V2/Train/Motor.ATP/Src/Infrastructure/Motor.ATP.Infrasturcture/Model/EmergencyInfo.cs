using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motor.ATP.Infrasturcture.Interface;

namespace Motor.ATP.Infrasturcture.Model
{
    public class EmergencyInfo : ATPPartialBase,IEmergencyInfo
    {
        private bool m_Visible;
        private EmergencyInfoType m_InfoType;
        private bool m_IsEffective;

        public EmergencyInfo(ATPDomain parent)
            : base(parent)
        {
            Visible = true;
        }

        public bool Visible
        {
            get { return m_Visible; }
            set
            {
                if (value == m_Visible)
                {
                    return;
                }
                m_Visible = value;
                RaisePropertyChanged(() => Visible);
            }
        }

        /// <summary>
        /// 紧急消息
        /// </summary>
        public EmergencyInfoType InfoType
        {
            get { return m_InfoType; }
            set
            {
                if (value == m_InfoType)
                {
                    return;
                }
                m_InfoType = value;
                RaisePropertyChanged(() => InfoType);
            }
        }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsEffective
        {
            get { return m_IsEffective; }
            set
            {
                if (value == m_IsEffective)
                {
                    return;
                }
                m_IsEffective = value;
                RaisePropertyChanged(() => IsEffective);
            }
        }
    }
}
