using CBTC.Infrasturcture.Model.Constant;
using Microsoft.Practices.Prism.ViewModel;

namespace CBTC.Infrasturcture.Model.Test
{
    public class TestInfo : NotificationObject
    {
        private NetStatus m_RedNetStatus;
        private NetStatus m_BlueNetStatus;
        private EmergencyBrakeStatus m_EmergencyBrakeStatus;
        private BroadcastTestStatus m_BroadcastTestStatus;

        private bool m_ButtonDownBrakeTest;
        private bool m_ButtonDownRemit;
        private bool m_ButtonDownBroadcastTest;
        private bool m_ButtonDownLightTest;

        public TestInfo()
        {
            m_RedNetStatus = NetStatus.None;
            m_BlueNetStatus = NetStatus.None;
            m_EmergencyBrakeStatus = EmergencyBrakeStatus.None;
            m_BroadcastTestStatus = BroadcastTestStatus.None;

            m_ButtonDownBrakeTest = false;
            m_ButtonDownRemit = false;
            m_ButtonDownBroadcastTest = false;
            m_ButtonDownLightTest = false;
        }

        /// <summary>
        /// 红网状态
        /// </summary>
        public NetStatus RedStatus
        {
            get { return m_RedNetStatus; }
            set
            {
                if (value == m_RedNetStatus)
                {
                    return;
                }
                m_RedNetStatus = value;
                RaisePropertyChanged(() => RedStatus);
            }
        }

        /// <summary>
        /// 蓝网状态
        /// </summary>
        public NetStatus BlueStatus
        {
            get { return m_BlueNetStatus; }
            set
            {
                if (value == m_BlueNetStatus)
                {
                    return;
                }
                m_BlueNetStatus = value;
                RaisePropertyChanged(() => BlueStatus);
            }
        }

        /// <summary>
        /// 紧急制动状态
        /// </summary>
        public EmergencyBrakeStatus EmergencyBrakeStatus
        {
            get { return m_EmergencyBrakeStatus; }
            set
            {
                if (value == m_EmergencyBrakeStatus)
                {
                    return;
                }
                m_EmergencyBrakeStatus = value;
                RaisePropertyChanged(() => EmergencyBrakeStatus);
            }
        }

        /// <summary>
        /// 广播测试状态
        /// </summary>
        public BroadcastTestStatus BroadcastTestStatus
        {
            get { return m_BroadcastTestStatus; }
            set
            {
                if (value == m_BroadcastTestStatus)
                {
                    return;
                }
                m_BroadcastTestStatus = value;
                RaisePropertyChanged(() => BroadcastTestStatus);
            }
        }

        /// <summary>
        /// 试闸
        /// </summary>
        public bool ButtonDownBrakeTest
        {
            get { return m_ButtonDownBrakeTest; }
            set
            {
                if (value == m_ButtonDownBrakeTest)
                {
                    return;
                }
                m_ButtonDownBrakeTest = value;
                RaisePropertyChanged(() => ButtonDownBrakeTest);
            }
        }

        /// <summary>
        /// 缓解
        /// </summary>
        public bool ButtonDownRemit
        {
            get { return m_ButtonDownRemit; }
            set
            {
                if (value == m_ButtonDownRemit)
                {
                    return;
                }
                m_ButtonDownRemit = value;
                RaisePropertyChanged(() => ButtonDownRemit);
            }
        }

        /// <summary>
        /// 广播测试
        /// </summary>
        public bool ButtonDownBroadcastTest
        {
            get { return m_ButtonDownBroadcastTest; }
            set
            {
                if (value == m_ButtonDownBroadcastTest)
                {
                    return;
                }
                m_ButtonDownBroadcastTest = value;
                RaisePropertyChanged(() => ButtonDownBroadcastTest);
            }
        }

        /// <summary>
        /// 点灯测试
        /// </summary>
        public bool ButtonDownLightTest
        {
            get { return m_ButtonDownLightTest; }
            set
            {
                if (value == m_ButtonDownLightTest)
                {
                    return;
                }
                m_ButtonDownLightTest = value;
                RaisePropertyChanged(() => ButtonDownLightTest);
            }
        }

    }
}