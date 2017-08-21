using Microsoft.Practices.Prism.ViewModel;
using Tram.CBTC.Infrasturcture.Model.Constant;

namespace Tram.CBTC.Infrasturcture.Model.Train
{
    public class System : NotificationObject
    {
        private SystemStatus m_SystemStatus;
        private GPSWorkStatus m_GPSWorkStatus;
        private VehicleCommunicationStatus m_VehicleCommunicationStatus;
        private VehicleLocationStatus m_VehicleLocationStatus;
        private VOBCOnBoardHostStatus m_VOBCOnBoardHostStatus;
        private CPStatus m_CPStatus;
        private RRStatus m_RRStatus;
        private ATPStatus m_ATPStatus;
        private ELSStatus m_ELSStatus;
        private bool m_IsSoundOpen;

        /// <summary>
        /// 系统状态
        /// </summary>
        public SystemStatus SystemStatus
        {
            get { return m_SystemStatus; }
            set
            {
                if (value == m_SystemStatus)
                    return;

                m_SystemStatus = value;
                RaisePropertyChanged(() => SystemStatus);
            }
        }

        /// <summary>
        /// ELS状态
        /// </summary>
        public ELSStatus ELSStatus
        {
            get { return m_ELSStatus; }
            set
            {
                if (value == m_ELSStatus)
                    return;

                m_ELSStatus = value;
                RaisePropertyChanged(() => ELSStatus);
            }
        }

        /// <summary>
        /// ATP状态
        /// </summary>
        public ATPStatus ATPStatus
        {
            get { return m_ATPStatus; }
            set
            {
                if (value == m_ATPStatus)
                    return;

                m_ATPStatus = value;
                RaisePropertyChanged(() => ATPStatus);
            }
        }

        /// <summary>
        /// RR状态
        /// </summary>
        public RRStatus RRStatus
        {
            get { return m_RRStatus; }
            set
            {
                if (value == m_RRStatus)
                    return;

                m_RRStatus = value;
                RaisePropertyChanged(() => RRStatus);
            }
        }

        /// <summary>
        /// CP状态
        /// </summary>
        public CPStatus CPStatus
        {
            get { return m_CPStatus; }
            set
            {
                if (value == m_CPStatus)
                    return;

                m_CPStatus = value;
                RaisePropertyChanged(() => CPStatus);
            }
        }

        /// <summary>
        /// 车载主机状态
        /// </summary>
        public VOBCOnBoardHostStatus VOBCOnBoardHostStatus
        {
            get { return m_VOBCOnBoardHostStatus; }
            set
            {
                if (value == m_VOBCOnBoardHostStatus)
                    return;

                m_VOBCOnBoardHostStatus = value;
                RaisePropertyChanged(() => VOBCOnBoardHostStatus);
            }
        }

        /// <summary>
        /// 定位
        /// </summary>
        public VehicleLocationStatus VehicleLocationStatus
        {
            get { return m_VehicleLocationStatus; }
            set
            {
                if (value == m_VehicleLocationStatus)
                    return;

                m_VehicleLocationStatus = value;
                RaisePropertyChanged(() => VehicleLocationStatus);
            }
        }

        /// <summary>
        /// 车载主机与调度中心
        /// </summary>
        public VehicleCommunicationStatus VehicleCommunicationStatus
        {
            get { return m_VehicleCommunicationStatus; }
            set
            {
                if (value == m_VehicleCommunicationStatus)
                    return;

                m_VehicleCommunicationStatus = value;
                RaisePropertyChanged(() => VehicleCommunicationStatus);
            }
        }

        /// <summary>
        /// GPS状态
        /// </summary>
        public GPSWorkStatus GPSWorkStatus
        {
            get { return m_GPSWorkStatus; }
            set
            {
                if (value == m_GPSWorkStatus)
                    return;

                m_GPSWorkStatus = value;
                RaisePropertyChanged(() => GPSWorkStatus);
            }
        }

        /// <summary>
        /// 声音系统状态  true 打开 fasle 关闭
        /// </summary>
        public bool IsSoundOpen
        {
            get { return m_IsSoundOpen; }
            set
            {
                if (value == m_IsSoundOpen)
                    return;

                m_IsSoundOpen = value;
                RaisePropertyChanged(() => IsSoundOpen);
            }
        }
    }
}
