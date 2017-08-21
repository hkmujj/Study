using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model
{
    public class ControlModel : ATPPartialBase, IControlModel
    {
        private bool m_InEffect;
        private ControlType m_CurrentControlType;
        private ControlType m_NextControlType;

        public ControlModel(ATPDomain parent)
            : base(parent)
        {
        }

        public ControlType CurrentControlType
        {
            get { return m_CurrentControlType; }
            set
            {
                if (value == m_CurrentControlType)
                {
                    return;
                }
                m_CurrentControlType = value;
                RaisePropertyChanged(() => CurrentControlType);
            }
        }

        /// <summary>
        /// 准备进入的IControlModel
        /// </summary>
        public ControlType NextControlType
        {
            get { return m_NextControlType; }
            set
            {
                if (value == m_NextControlType)
                {
                    return;
                }
                m_NextControlType = value;
                RaisePropertyChanged(() => NextControlType);
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
    }
}
