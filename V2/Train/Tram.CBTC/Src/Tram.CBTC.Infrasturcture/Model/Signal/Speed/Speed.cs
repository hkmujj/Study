using Microsoft.Practices.Prism.ViewModel;
using Tram.CBTC.Infrasturcture.Model.Constant;

namespace Tram.CBTC.Infrasturcture.Model.Signal.Speed
{
    public class Speed : NotificationObject
    {
        private ISpeedDialPlate m_SpeedDialPlate;
        private BrakeDetailsType m_BrakeDetailsType;

        public Speed()
        {
            RunSpeed = new SpeedModel();
            LimitSpeed = new SpeedModel();
            TargetSpeed = new SpeedModel();
            RecommendedSpeed = new SpeedModel();
            TargetSpeed = new SpeedModel();
            AlarmSpeed = new SpeedModel();
        }
        /// <summary>
        /// 速度表盘
        /// </summary>
        public ISpeedDialPlate SpeedDialPlate
        {
            get { return m_SpeedDialPlate; }
            set
            {
                if (Equals(value, m_SpeedDialPlate))
                    return;

                m_SpeedDialPlate = value;
                RaisePropertyChanged(() => SpeedDialPlate);
            }
        }

        /// <summary>
        /// 运行速度
        /// </summary>
        public SpeedModel RunSpeed { get; protected set; }
        /// <summary>
        /// 限制速度
        /// </summary>
        public SpeedModel LimitSpeed { get; private set; }
        /// <summary>
        /// 推荐速度
        /// </summary>
        public SpeedModel RecommendedSpeed { get; private set; }
        /// <summary>
        /// 目标速度
        /// </summary>
        public SpeedModel TargetSpeed { get; private set; }
        /// <summary>
        /// 报警速度
        /// </summary>
        public SpeedModel AlarmSpeed { get; private set; }


        /// <summary>
        /// 超速报警及输出紧急制动
        /// </summary>
        public BrakeDetailsType BrakeDetailsType
        {
            get { return m_BrakeDetailsType; }
            set
            {
                if (Equals(value, m_BrakeDetailsType))
                    return;

                m_BrakeDetailsType = value;
                RaisePropertyChanged(() => BrakeDetailsType);
            }
        }

    }
}