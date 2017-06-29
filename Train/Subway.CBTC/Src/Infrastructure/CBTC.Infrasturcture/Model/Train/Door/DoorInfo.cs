using CBTC.Infrasturcture.Model.Constant;
using Microsoft.Practices.Prism.ViewModel;

namespace CBTC.Infrasturcture.Model.Train.Door
{
    public class DoorInfo : NotificationObject
    {
        private DoorState m_RightPSDState;
        private DoorState m_LeftPSDState;
        private DoorState m_RightDoorState;
        private DoorState m_LeftDoorState;
        private DoorAllowState m_DoorAllowState;
        private bool m_Bypassed;
        private DoorControlMode m_DoorControlMode;
        private DoorAllowState m_StationAllowState;
        private NextStationDoorOpenDirection m_NextStationDoorOpenDirection;

        /// <summary>
        /// 被旁路
        /// </summary>
        public bool Bypassed
        {
            get { return m_Bypassed; }
            set
            {
                if (value == m_Bypassed)
                    return;

                m_Bypassed = value;
                RaisePropertyChanged(() => Bypassed);
            }
        }

        /// <summary>
        /// 门控制模式
        /// </summary>
        public DoorControlMode DoorControlMode
        {
            get { return m_DoorControlMode; }
            set
            {
                if (value == m_DoorControlMode)
                    return;

                m_DoorControlMode = value;
                RaisePropertyChanged(() => DoorControlMode);
            }
        }

        /// <summary>
        /// 车门允许状态
        /// </summary>
        public DoorAllowState DoorAllowState
        {
            get { return m_DoorAllowState; }
            set
            {
                if (value == m_DoorAllowState)
                    return;

                m_DoorAllowState = value;
                RaisePropertyChanged(() => DoorAllowState);
            }
        }
        /// <summary>
        /// 站台门允许状态
        /// </summary>
        public DoorAllowState StationAllowState
        {
            get { return m_StationAllowState; }
            set
            {
                if (value == m_StationAllowState)
                    return;
                m_StationAllowState = value;
                RaisePropertyChanged(() => StationAllowState);
            }
        }

        /// <summary>
        /// 左车门
        /// </summary>
        public DoorState LeftDoorState
        {
            get { return m_LeftDoorState; }
            set
            {
                if (value == m_LeftDoorState)
                    return;

                m_LeftDoorState = value;
                RaisePropertyChanged(() => LeftDoorState);
            }
        }

        /// <summary>
        /// 右车门
        /// </summary>
        public DoorState RightDoorState
        {
            get { return m_RightDoorState; }
            set
            {
                if (value == m_RightDoorState)
                    return;

                m_RightDoorState = value;
                RaisePropertyChanged(() => RightDoorState);
            }
        }

        /// <summary>
        /// 左站台车
        /// </summary>
        public DoorState LeftPSDState
        {
            get { return m_LeftPSDState; }
            set
            {
                if (value == m_LeftPSDState)
                    return;

                m_LeftPSDState = value;
                RaisePropertyChanged(() => LeftPSDState);
            }
        }

        /// <summary>
        /// 右站台门
        /// </summary>
        public DoorState RightPSDState
        {
            get { return m_RightPSDState; }
            set
            {
                if (value == m_RightPSDState)
                    return;

                m_RightPSDState = value;
                RaisePropertyChanged(() => RightPSDState);
            }
        }

        /// <summary>
        /// 下一站开门方向
        /// </summary>
        public NextStationDoorOpenDirection NextStationDoorOpenDirection
        {
            get { return m_NextStationDoorOpenDirection; }
            set
            {
                if (value == m_NextStationDoorOpenDirection)
                    return;
                m_NextStationDoorOpenDirection = value;
                RaisePropertyChanged(() => NextStationDoorOpenDirection);
            }
        }
    }
}