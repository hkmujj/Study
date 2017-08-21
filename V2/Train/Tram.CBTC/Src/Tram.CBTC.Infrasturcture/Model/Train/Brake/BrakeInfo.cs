using Microsoft.Practices.Prism.ViewModel;
using Tram.CBTC.Infrasturcture.Model.Constant;

namespace Tram.CBTC.Infrasturcture.Model.Train.Brake
{
    /// <summary>
    /// 制动信息
    /// </summary>
    public class BrakeInfo : NotificationObject
    {
        private RactingInstructions m_RactingInstructions;
        private SignalBrakeStatus m_SignalBrakeStatus;

        /// <summary>
        /// 信号制动状态
        /// </summary>
        public SignalBrakeStatus SignalBrakeStatus
        {
            get { return m_SignalBrakeStatus; }
            set
            {
                if (value == m_SignalBrakeStatus)
                    return;
                m_SignalBrakeStatus = value;
                RaisePropertyChanged(() => SignalBrakeStatus);
            }
        }

        /// <summary>
        /// 空转指示
        /// </summary>

        public RactingInstructions RactingInstructions
        {
            get { return m_RactingInstructions; }
            set
            {
                if (value == m_RactingInstructions)
                    return;
                m_RactingInstructions = value;
                RaisePropertyChanged(() => RactingInstructions);
            }
        }
    }
}