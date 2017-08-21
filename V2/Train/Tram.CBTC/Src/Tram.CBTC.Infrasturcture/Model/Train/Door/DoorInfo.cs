using Microsoft.Practices.Prism.ViewModel;
using Tram.CBTC.Infrasturcture.Model.Constant;

namespace Tram.CBTC.Infrasturcture.Model.Train.Door
{
    /// <summary>
    /// 门信息
    /// </summary>
    public class DoorInfo : NotificationObject
    {
        private DoorStatus m_RightDoorStatus;
        private DoorStatus m_LeftDoorStatus;
        private DoorAllowStatus m_RightDoorAllowStatus;
        private DoorAllowStatus m_LeftDoorAllowStatus;

        /// <summary>
        /// 左门允许状态
        /// </summary>
        public DoorAllowStatus LeftDoorAllowStatus
        {
            get { return m_LeftDoorAllowStatus; }
            set
            {
                if (value == m_LeftDoorAllowStatus)
                    return;
                m_LeftDoorAllowStatus = value;
                RaisePropertyChanged(() => LeftDoorAllowStatus);
            }
        }

        /// <summary>
        /// 右门允许状态
        /// </summary>
        public DoorAllowStatus RightDoorAllowStatus
        {
            get { return m_RightDoorAllowStatus; }
            set
            {
                if (value == m_RightDoorAllowStatus)
                    return;
                m_RightDoorAllowStatus = value;
                RaisePropertyChanged(() => RightDoorAllowStatus);
            }
        }

        /// <summary>
        /// 左门状态
        /// </summary>
        public DoorStatus LeftDoorStatus
        {
            get { return m_LeftDoorStatus; }
            set
            {
                if (value == m_LeftDoorStatus)
                    return;
                m_LeftDoorStatus = value;
                RaisePropertyChanged(() => LeftDoorStatus);
            }
        }

        /// <summary>
        /// 右门状态
        /// </summary>
        public DoorStatus RightDoorStatus
        {
            get { return m_RightDoorStatus; }
            set
            {
                if (value == m_RightDoorStatus)
                    return;
                m_RightDoorStatus = value;
                RaisePropertyChanged(() => RightDoorStatus);
            }
        }
    }
}