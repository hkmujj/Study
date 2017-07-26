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

        public AssistDisplayInfo(ATPDomain parent) : base(parent)
        {
        }
    }
}