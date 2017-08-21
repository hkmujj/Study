using System.ComponentModel.Composition;
using Subway.ShenZhenLine7.Extention;
using Subway.ShenZhenLine7.Interfaces;

namespace Subway.ShenZhenLine7.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    [Export]
    [Export(typeof(ICourceStatusChanged))]
    public class MainContentBrnViewModel : ViewModelBase
    {
        private bool m_HSCBFault;
        private bool m_AirPumpFault;
        private bool m_SmokeFault;
        private bool m_TractionFault;
        private bool m_BrakeFault;
        private bool m_EmergencyTalkFault;
        private bool m_DoorFault;
        private bool m_EmergencyBorderCastFault;
        private bool m_AssistFault;
        private bool m_AirConditionFault;

        /// <summary>
        /// 初始化运行数据
        /// </summary>
        public override void Start()
        {
            this.Cast(false);
        }

        /// <summary>
        /// 空调功能区故障
        /// </summary>
        public bool AirConditionFault
        {
            get { return m_AirConditionFault; }
            set
            {
                if (value == m_AirConditionFault)
                {
                    return;
                }
                m_AirConditionFault = value;
                RaisePropertyChanged(() => AirConditionFault);
            }
        }

        /// <summary>
        /// 辅助电源功能区故障
        /// </summary>
        public bool AssistFault
        {
            get { return m_AssistFault; }
            set
            {
                if (value == m_AssistFault)
                {
                    return;
                }
                m_AssistFault = value;
                RaisePropertyChanged(() => AssistFault);
            }
        }

        /// <summary>
        /// 紧急广播功能区故障
        /// </summary>
        public bool EmergencyBorderCastFault
        {
            get { return m_EmergencyBorderCastFault; }
            set
            {
                if (value == m_EmergencyBorderCastFault)
                {
                    return;
                }
                m_EmergencyBorderCastFault = value;
                RaisePropertyChanged(() => EmergencyBorderCastFault);
            }
        }

        /// <summary>
        /// 门功能区故障
        /// </summary>
        public bool DoorFault
        {
            get { return m_DoorFault; }
            set
            {
                if (value == m_DoorFault)
                {
                    return;
                }
                m_DoorFault = value;
                RaisePropertyChanged(() => DoorFault);
            }
        }

        /// <summary>
        /// 紧急对讲功能区故障
        /// </summary>
        public bool EmergencyTalkFault
        {
            get { return m_EmergencyTalkFault; }
            set
            {
                if (value == m_EmergencyTalkFault)
                {
                    return;
                }
                m_EmergencyTalkFault = value;
                RaisePropertyChanged(() => EmergencyTalkFault);
            }
        }

        /// <summary>
        /// 制动功能区故障
        /// </summary>
        public bool BrakeFault
        {
            get { return m_BrakeFault; }
            set
            {
                if (value == m_BrakeFault)
                {
                    return;
                }
                m_BrakeFault = value;
                RaisePropertyChanged(() => BrakeFault);
            }
        }

        /// <summary>
        /// 牵引功能区故障
        /// </summary>
        public bool TractionFault
        {
            get { return m_TractionFault; }
            set
            {
                if (value == m_TractionFault)
                {
                    return;
                }
                m_TractionFault = value;
                RaisePropertyChanged(() => TractionFault);
            }
        }

        /// <summary>
        /// 烟火报警功能区故障
        /// </summary>
        public bool SmokeFault
        {
            get { return m_SmokeFault; }
            set
            {
                if (value == m_SmokeFault)
                {
                    return;
                }
                m_SmokeFault = value;
                RaisePropertyChanged(() => SmokeFault);
            }
        }

        /// <summary>
        /// 空压机功能区故障
        /// </summary>
        public bool AirPumpFault
        {
            get { return m_AirPumpFault; }
            set
            {
                if (value == m_AirPumpFault)
                {
                    return;
                }
                m_AirPumpFault = value;
                RaisePropertyChanged(() => AirPumpFault);
            }
        }

        /// <summary>
        /// HSCB功能区故障
        /// </summary>
        public bool HSCBFault
        {
            get { return m_HSCBFault; }
            set
            {
                if (value == m_HSCBFault)
                {
                    return;
                }
                m_HSCBFault = value;
                RaisePropertyChanged(() => HSCBFault);
            }
        }
    }
}