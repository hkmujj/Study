using CBTC.Infrasturcture.Model.Constant;
using CBTC.Infrasturcture.Model.Signal.Warning;
using Microsoft.Practices.Prism.ViewModel;

namespace CBTC.Infrasturcture.Model.Signal
{
    public class SignalInfo : NotificationObject
    {
        private HighDirveModel m_HighDirveModel;
        private CabSignalCode m_CabSignalCode;
        private WarningIntervention m_WarningIntervention;
        private JumpStopDetainCar m_JumpStop;
        private ATPConnectState m_ATPConnectState;
        private bool m_HighDirveModelFlick;
        private HightDriveModelColor m_HightDriveModelColor;

        public SignalInfo()
        {
            Speed = new Speed.Speed();
            WarningIntervention = new WarningIntervention();
        }

        public ATPConnectState ATPConnectState
        {
            get { return m_ATPConnectState; }
            set
            {
                if (value == m_ATPConnectState)
                    return;

                m_ATPConnectState = value;
                RaisePropertyChanged(() => ATPConnectState);
            }
        }

        public JumpStopDetainCar JumpStop
        {
            set
            {
                if (value == m_JumpStop)
                    return;

                m_JumpStop = value;
                RaisePropertyChanged(() => JumpStop);
            }
            get { return m_JumpStop; }
        }

        /// <summary>
        /// 机车信号
        /// </summary>
        public CabSignalCode CabSignalCode
        {
            get { return m_CabSignalCode; }
            set
            {
                if (value == m_CabSignalCode)
                    return;

                m_CabSignalCode = value;
                RaisePropertyChanged(() => CabSignalCode);
            }
        }
        /// <summary>
        /// 最高可用模式
        /// </summary>
        public HighDirveModel HighDirveModel
        {
            set
            {
                if (value == m_HighDirveModel)
                    return;

                m_HighDirveModel = value;
                RaisePropertyChanged(() => HighDirveModel);
            }
            get { return m_HighDirveModel; }
        }
        /// <summary>
        /// 最高可用驾驶模式颜色
        /// </summary>
        public HightDriveModelColor HightDriveModelColor
        {
            get { return m_HightDriveModelColor; }
            set
            {
                if (value == m_HightDriveModelColor)
                    return;
                m_HightDriveModelColor = value;
                RaisePropertyChanged(() => HightDriveModelColor);
            }
        }

        /// <summary>
        /// 最高可用驾驶模式闪烁
        /// </summary>
        public bool HighDirveModelFlick
        {
            get { return m_HighDirveModelFlick; }
            set
            {
                if (value == m_HighDirveModelFlick)
                    return;

                m_HighDirveModelFlick = value;
                RaisePropertyChanged(() => HighDirveModelFlick);
            }
        }

        public Speed.Speed Speed { get; protected set; }

        /// <summary>
        /// 报警介入
        /// </summary>
        public WarningIntervention WarningIntervention
        {
            get { return m_WarningIntervention; }
            private set
            {
                if (Equals(value, m_WarningIntervention))
                    return;

                m_WarningIntervention = value;
                RaisePropertyChanged(() => WarningIntervention);
            }
        }
    }


}