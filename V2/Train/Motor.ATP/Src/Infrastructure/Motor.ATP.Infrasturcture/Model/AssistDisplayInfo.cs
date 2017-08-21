using System.Diagnostics.CodeAnalysis;
using Motor.ATP.Infrasturcture.Interface;

namespace Motor.ATP.Infrasturcture.Model
{
    public class AssistDisplayInfo : ATPPartialBase, IAssistDisplayInfo
    {
        private bool m_Visible;
        [SuppressMessage("ReSharper", "InconsistentNaming")] protected string m_TargetDistance;
        [SuppressMessage("ReSharper", "InconsistentNaming")] protected string m_CabSignalCodeTargetSpeedPair;
        [SuppressMessage("ReSharper", "InconsistentNaming")] protected string m_LimitedSpeed;
        [SuppressMessage("ReSharper", "InconsistentNaming")] protected string m_CurrentSpeed;
        private bool m_FrequencyUp;
        private bool m_FrequencyDown;
        private bool m_SplitPhaseEnable;
        private bool m_SplitPhaseExecute;

        /// <summary>
        /// 3位无效
        /// </summary>
        public const string Invalidate3Bit = "---";

        /// <summary>
        /// 4位无效
        /// </summary>
        public const string Invalidate4Bit = "----";

        /// <summary>
        /// 3位全码
        /// </summary>
        public const string Full3Bit = "8.8.8.";

        /// <summary>
        /// 4位全码
        /// </summary>
        public const string Full4Bit = "8.8.8.8.";

        public virtual bool Visible
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

        public virtual string CurrentSpeed
        {
            get { return m_CurrentSpeed; }
            set
            {
                if (value == m_CurrentSpeed)
                {
                    return;
                }
                m_CurrentSpeed = value;
                RaisePropertyChanged(() => CurrentSpeed);
            }
        }

        public virtual string LimitedSpeed
        {
            get { return m_LimitedSpeed; }
            set
            {
                if (value == m_LimitedSpeed)
                {
                    return;
                }
                m_LimitedSpeed = value;
                RaisePropertyChanged(() => LimitedSpeed);
            }
        }

        public virtual string CabSignalCodeTargetSpeedPair
        {
            get { return m_CabSignalCodeTargetSpeedPair; }
            set
            {
                if (value == m_CabSignalCodeTargetSpeedPair)
                {
                    return;
                }
                m_CabSignalCodeTargetSpeedPair = value;
                RaisePropertyChanged(() => CabSignalCodeTargetSpeedPair);
            }
        }

        public virtual string TargetDistance
        {
            get { return m_TargetDistance; }
            set
            {
                if (value == m_TargetDistance)
                {
                    return;
                }
                m_TargetDistance = value;
                RaisePropertyChanged(() => TargetDistance);
            }
        }

        public bool FrequencyUp
        {
            get { return m_FrequencyUp; }
            set
            {
                if (value == m_FrequencyUp) return;
                m_FrequencyUp = value;
                RaisePropertyChanged(() => FrequencyUp);
            }
        }

        public bool FrequencyDown
        {
            get { return m_FrequencyDown; }
            set
            {
                if (value == m_FrequencyDown) return;
                m_FrequencyDown = value;
                RaisePropertyChanged(() => FrequencyDown);
            }
        }

        public bool SplitPhaseEnable
        {
            get { return m_SplitPhaseEnable; }
            set
            {
                if (value == m_SplitPhaseEnable) return;
                m_SplitPhaseEnable = value;
                RaisePropertyChanged(() => SplitPhaseEnable);
            }
        }

        public bool SplitPhaseExecute
        {
            get { return m_SplitPhaseExecute; }
            set
            {
                if (value == m_SplitPhaseExecute) return;
                m_SplitPhaseExecute = value;
                RaisePropertyChanged(() => SplitPhaseExecute);
            }
        }

        public AssistDisplayInfo(ATPDomain parent) : base(parent)
        {
        }
    }
}