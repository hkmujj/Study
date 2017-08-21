using Motor.ATP.Domain.Interface;
using Motor.ATP.Domain.Interface.TargetDistance;
using Motor.ATP.Domain.Model.TargitDistance;

namespace Motor.ATP.Domain.Model
{

    public class WarningIntervention : ATPPartialBase, IWarningIntervention
    {
        private double m_WarningTime;
        private double m_TargetDistance;
        private ATPColor m_BrakeWaringColor;
        private BrakeWarningLevel m_BrakeWarningLevel;
        private bool m_TargetDistanceVisible;
        private bool m_BrakeWarningVisible;
        private bool m_Visible;
        private ITargitDistanceScale m_TargitDistanceScale;

        public WarningIntervention(ATPDomain parent)
            : base(parent)
        {
            TargitDistanceScale = new DefaultTargitDistanceScale();
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
                if (!value)
                {
                    BrakeWarningVisible = false;
                    TargetDistanceVisible = false;
                }
                RaisePropertyChanged(() => Visible);
            }
        }

        public ITargitDistanceScale TargitDistanceScale
        {
            private set
            {
                if (Equals(value, m_TargitDistanceScale))
                {
                    return;
                }
                m_TargitDistanceScale = value;
                RaisePropertyChanged(() => TargitDistanceScale);
            }
            get { return m_TargitDistanceScale; }
        }


        public bool BrakeWarningVisible
        {
            get { return m_BrakeWarningVisible; }
            set
            {
                if (value == m_BrakeWarningVisible)
                {
                    return;
                }
                m_BrakeWarningVisible = value;
                
                RaisePropertyChanged(() => BrakeWarningVisible);
            }
        }

        public BrakeWarningLevel BrakeWarningLevel
        {
            get { return m_BrakeWarningLevel; }
            set
            {
                if (value == m_BrakeWarningLevel)
                {
                    return;
                }
                m_BrakeWarningLevel = value;
                RaisePropertyChanged(() => BrakeWarningLevel);
            }
        }

        public ATPColor BrakeWaringColor
        {
            get { return m_BrakeWaringColor; }
            set
            {
                if (value.Equals(m_BrakeWaringColor))
                {
                    return;
                }
                m_BrakeWaringColor = value;
                RaisePropertyChanged(() => BrakeWaringColor);
            }
        }

        /// <summary>
        /// 报警时间, 单位 : 秒
        /// </summary>
        public double WarningTime
        {
            get { return m_WarningTime; }
            set { m_WarningTime = value; RaisePropertyChanged(() => WarningTime); }
        }

        /// <summary>
        /// 目标距离, 单位 : 米
        /// </summary>
        public double TargetDistance
        {
            get { return m_TargetDistance; }
            set
            {
                m_TargetDistance = value;
                RaisePropertyChanged(() => TargetDistance);
            }
        }

        public bool TargetDistanceVisible
        {
            get { return m_TargetDistanceVisible; }
            set
            {
                if (value == m_TargetDistanceVisible)
                {
                    return;
                }
                m_TargetDistanceVisible = value;
                RaisePropertyChanged(() => TargetDistanceVisible);
            }
        }
    }
}