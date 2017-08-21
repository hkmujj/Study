using CBTC.Infrasturcture.Model.Constant;
using Microsoft.Practices.Prism.ViewModel;

namespace CBTC.Infrasturcture.Model.Signal.Warning
{
    public class WarningIntervention : NotificationObject
    {
        private bool m_TargetDistanceVisible;
        private double m_TargetDistance;
        private CBTCColor m_WarningColor;
        private ITargitDistanceScale m_TargitDistanceScale;
        private double m_ParkingPointDistance;
        private double m_TagetSpeed;

        public WarningIntervention()
        {
            TargitDistanceScale = DefaultTargitDistanceScale.Instance;
        }

        /// <summary>
        /// 目标距离刻度
        /// </summary>
        public ITargitDistanceScale TargitDistanceScale
        {
            get { return m_TargitDistanceScale; }
            set
            {
                if (Equals(value, m_TargitDistanceScale))
                    return;

                m_TargitDistanceScale = value;
                RaisePropertyChanged(() => TargitDistanceScale);
            }
        }

        /// <summary>
        /// 警报颜色 
        /// </summary>
        public CBTCColor WarningColor
        {
            get { return m_WarningColor; }
            set
            {
                if (value == m_WarningColor)
                    return;

                m_WarningColor = value;
                RaisePropertyChanged(() => WarningColor);
            }
        }

        /// <summary>
        /// 目标距离值
        /// </summary>
        public double TargetDistance
        {
            get { return m_TargetDistance; }
            set
            {
                if (value.Equals(m_TargetDistance))
                    return;

                m_TargetDistance = value;
                RaisePropertyChanged(() => TargetDistance);
            }
        }

        /// <summary>
        /// 目标距离是否显示 
        /// </summary>
        public bool TargetDistanceVisible
        {
            get { return m_TargetDistanceVisible; }
            set
            {
                if (value == m_TargetDistanceVisible)
                    return;

                m_TargetDistanceVisible = value;
                RaisePropertyChanged(() => TargetDistanceVisible);
            }
        }

        /// <summary>
        /// 停车点距离
        /// </summary>
        public double ParkingPointDistance
        {
            get { return m_ParkingPointDistance; }
            set
            {
                if (value.Equals(m_ParkingPointDistance))
                    return;
                m_ParkingPointDistance = value;
                RaisePropertyChanged(() => ParkingPointDistance);
            }
        }

        /// <summary>
        /// 目标速度
        /// </summary>
        public double TagetSpeed
        {
            get { return m_TagetSpeed; }
            set
            {
                if (value.Equals(m_TagetSpeed))
                    return;
                m_TagetSpeed = value;
                RaisePropertyChanged(() => TagetSpeed);
            }
        }
    }
}