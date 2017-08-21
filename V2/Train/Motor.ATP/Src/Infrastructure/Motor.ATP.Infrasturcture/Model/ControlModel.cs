using Motor.ATP.Infrasturcture.Interface;

namespace Motor.ATP.Infrasturcture.Model
{
    public class ControlModel : ATPPartialBase, IControlModel
    {
        private bool m_InEffect;
        private bool m_Visible;
        private ControlType m_CurrentControlType;
        private ControlType m_NextControlType;
        private bool m_NextControlTypeVisible;

        public ControlModel(ATPDomain parent)
            : base(parent)
        {
            Visible = true;
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

        /// <summary>
        /// 可见性
        /// </summary>
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
        /// 下一模式是否可见
        /// </summary>
        public bool NextControlTypeVisible
        {
            get { return m_NextControlTypeVisible; }
            set
            {
                if (value == m_NextControlTypeVisible)
                    return;

                m_NextControlTypeVisible = value;
                RaisePropertyChanged(() => NextControlTypeVisible);
            }
        }
    }
}
