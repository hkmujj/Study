using Motor.ATP.Infrasturcture.Interface;

namespace Motor.ATP.Infrasturcture.Model
{
    public class CTCS : ATPPartialBase, ICTCS
    {
        private bool m_InEffect;
        private CTCSType m_CurrentCTCSType;
        private bool m_Visible;
        private CTCSType m_NextCTCSType;

        public CTCS(ATPDomain parent)
            : base(parent)
        {
            Visible = false;
        }

        public CTCSType CurrentCTCSType
        {
            get { return m_CurrentCTCSType; }
            set
            {
                if (value == m_CurrentCTCSType)
                {
                    return;
                }

                m_CurrentCTCSType = value;
                RaisePropertyChanged(() => CurrentCTCSType);
            }
        }

        /// <summary>
        /// 准备进入的CTCS
        /// </summary>
        public CTCSType NextCTCSType
        {
            get { return m_NextCTCSType; }
            set
            {
                if (value == m_NextCTCSType)
                {
                    return;
                }

                m_NextCTCSType = value;
                RaisePropertyChanged(() => NextCTCSType);
            }
        }

        /// <summary>
        /// 已经生效
        /// </summary>
        public bool InEffect
        {
            get { return m_InEffect; }
            set
            {
                m_InEffect = value;
                RaisePropertyChanged(() => InEffect);
            }
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
    }
}
