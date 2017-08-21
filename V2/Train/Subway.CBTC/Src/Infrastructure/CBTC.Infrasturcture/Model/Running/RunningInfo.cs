using CBTC.Infrasturcture.Model.Constant;
using Microsoft.Practices.Prism.ViewModel;

namespace CBTC.Infrasturcture.Model.Running
{
    public class RunningInfo : NotificationObject
    {
        private OperatingGrade m_OperatingGrade;
        private ATCRequestState m_ATCRequestState;
        private TrainOperatingMode m_TrainOperatingMode;
        private TrainPosition m_TrainPosition;
        private ParkingStationState m_ParkingState;
        private DepartState m_DepartState;
        private ParkingAlignmentState m_ParkingAlignmentState;
        private TrainOperatingModeStatus m_IATPAvailable;
        private TrainOperatingModeStatus m_ATOAvailable;
        private TrainOperatingModeStatus m_ATPAvailable;
        private RunDirection m_RunDirection;

        /// <summary>
        /// 发车状态
        /// </summary>
        public DepartState DepartState
        {
            get { return m_DepartState; }
            set
            {
                if (value == m_DepartState)
                    return;

                m_DepartState = value;
                RaisePropertyChanged(() => DepartState);
            }
        }

        /// <summary>
        /// 发车状态
        /// </summary>
        public ATCRequestState ATCRequestState
        {
            get { return m_ATCRequestState; }
            set
            {
                if (value == m_ATCRequestState)
                    return;

                m_ATCRequestState = value;
                RaisePropertyChanged(() => ATCRequestState);
            }
        }

        /// <summary>
        /// 运行等级
        /// </summary>
        public OperatingGrade OperatingGrade
        {
            get { return m_OperatingGrade; }
            set
            {
                if (value == m_OperatingGrade)
                    return;

                m_OperatingGrade = value;
                RaisePropertyChanged(() => OperatingGrade);
            }
        }

        /// <summary>
        /// 操作模式
        /// </summary>
        public TrainOperatingMode TrainOperatingMode
        {
            get { return m_TrainOperatingMode; }
            set
            {
                if (Equals(value, m_TrainOperatingMode))
                    return;

                m_TrainOperatingMode = value;
                RaisePropertyChanged(() => TrainOperatingMode);
            }
        }

        /// <summary>
        /// ATP可用状态
        /// </summary>
        public TrainOperatingModeStatus ATPAvailable
        {
            get { return m_ATPAvailable; }
            set
            {
                if (value == m_ATPAvailable)
                    return;
                m_ATPAvailable = value;
                RaisePropertyChanged(() => ATPAvailable);
            }
        }

        /// <summary>
        /// ATO可用状态
        /// </summary>
        public TrainOperatingModeStatus ATOAvailable
        {
            get { return m_ATOAvailable; }
            set
            {
                if (value == m_ATOAvailable)
                    return;
                m_ATOAvailable = value;
                RaisePropertyChanged(() => ATOAvailable);
            }
        }

        /// <summary>
        /// IATP可用状态
        /// </summary>
        public TrainOperatingModeStatus IATPAvailable
        {
            get { return m_IATPAvailable; }
            set
            {
                if (value == m_IATPAvailable)
                    return;
                m_IATPAvailable = value;
                RaisePropertyChanged(() => IATPAvailable);
            }
        }

        /// <summary>
        /// 列车当前位置
        /// </summary>
        public TrainPosition TrainPosition
        {
            get { return m_TrainPosition; }
            set
            {
                if (value == m_TrainPosition)
                    return;

                m_TrainPosition = value;
                RaisePropertyChanged(() => TrainPosition);
            }
        }

        /// <summary>
        /// 列车停站信息
        /// </summary>
        public ParkingStationState ParkingState
        {
            get { return m_ParkingState; }
            set
            {
                if (value == m_ParkingState)
                    return;

                m_ParkingState = value;
                RaisePropertyChanged(() => ParkingState);
            }
        }

        /// <summary>
        /// 停车对准
        /// </summary>
        public ParkingAlignmentState ParkingAlignmentState
        {
            get { return m_ParkingAlignmentState; }
            set
            {
                if (value == m_ParkingAlignmentState)
                    return;

                m_ParkingAlignmentState = value;
                RaisePropertyChanged(() => ParkingAlignmentState);
            }
        }

        /// <summary>
        /// 运行方向
        /// </summary>
        public RunDirection RunDirection
        {
            get { return m_RunDirection; }
            set
            {
                if (value == m_RunDirection)
                    return;
                m_RunDirection = value;
                RaisePropertyChanged(() => RunDirection);
            }
        }
    }
}