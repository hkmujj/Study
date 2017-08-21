using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model
{
    public class CabSignal : ATPPartialBase, ICabSignal
    {
        private bool m_Visible;
        private CabSignalCode m_Code;
        private bool m_IsEffective;

        public CabSignal(ATPDomain parent)
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
        /// 机车信号码
        /// </summary>
        public CabSignalCode Code
        {
            get { return m_Code; }
            set
            {
                if (value == m_Code)
                {
                    return;
                }
                m_Code = value;
                RaisePropertyChanged(() => Code);
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
