using Microsoft.Practices.Prism.ViewModel;
using Tram.CBTC.Infrasturcture.Model.Constant;

namespace Tram.CBTC.Infrasturcture.Model.Train
{
    public class Device : NotificationObject
    {
        private DeviceState m_RadarState;
        private DeviceState m_StaState;
        private DeviceState m_NtcState;
        private DeviceState m_BaliseState;
        private DeviceState m_GpsState;
        private DeviceState m_DSRCState;
        private DeviceState m_TodState;

        /// <summary>
        /// 雷达
        /// </summary>
        public DeviceState RadarState
        {
            get { return m_RadarState; }
            set
            {
                if (value == m_RadarState)
                    return;
                m_RadarState = value;
                RaisePropertyChanged(() => RadarState);
            }
        }

        /// <summary>
        /// 无线接收处理单元（无线通讯）
        /// </summary>
        public DeviceState STAState
        {
            get { return m_StaState; }
            set
            {
                if (value == m_StaState)
                    return;
                m_StaState = value;
                RaisePropertyChanged(() => STAState);
            }
        }


        /// <summary>
        /// 车载控制器
        /// </summary>
        public DeviceState NTCState
        {
            get { return m_NtcState; }
            set
            {
                if (value == m_NtcState)
                    return;
                m_NtcState = value;
                RaisePropertyChanged(() => NTCState);
            }
        }


        /// <summary>
        /// 信标
        /// </summary>
        public DeviceState BaliseState
        {
            get { return m_BaliseState; }
            set
            {
                if (value == m_BaliseState)
                    return;
                m_BaliseState = value;
                RaisePropertyChanged(() => BaliseState);
            }
        }

        /// <summary>
        /// 卫星定位
        /// </summary>
        public DeviceState GPSState
        {
            get { return m_GpsState; }
            set
            {
                if (value == m_GpsState)
                    return;
                m_GpsState = value;
                RaisePropertyChanged(() => GPSState);
            }
        }

        /// <summary>
        /// 短程通信
        /// </summary>
        public DeviceState DSRCState
        {
            get { return m_DSRCState; }
            set
            {
                if (value == m_DSRCState)
                    return;
                m_DSRCState = value;
                RaisePropertyChanged(() => DSRCState);
            }
        }

        /// <summary>
        /// 司机显示单元（人机操作界面）
        /// </summary>
        public DeviceState TODState
        {
            get { return m_TodState; }
            set
            {
                if (value == m_TodState)
                    return;
                m_TodState = value;
                RaisePropertyChanged(() => TODState);
            }
        }
    }
}
