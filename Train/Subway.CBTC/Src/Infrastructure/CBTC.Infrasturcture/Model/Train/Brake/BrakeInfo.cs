using CBTC.Infrasturcture.Model.Constant;
using Microsoft.Practices.Prism.ViewModel;

namespace CBTC.Infrasturcture.Model.Train.Brake
{
    /// <summary>
    /// 制动信息
    /// </summary>
    public class BrakeInfo : NotificationObject
    {
        private BrakeState m_BrakeState;
        private BrakeType m_SignalBrake;
        private bool m_EmergencyBrake;

        public BrakeType SignalBrake
        {
            get { return m_SignalBrake; }
            set
            {
                if (value == m_SignalBrake)
                    return;

                m_SignalBrake = value;
                RaisePropertyChanged(() => SignalBrake);
            }
        }

        public BrakeState BrakeState
        {
            get { return m_BrakeState; }
            set
            {
                if (value == m_BrakeState)
                    return;

                m_BrakeState = value;
                RaisePropertyChanged(() => BrakeState);
            }
        }

        /// <summary>
        /// 紧急制动施加
        /// </summary>
        public bool EmergencyBrake
        {
            get { return m_EmergencyBrake; }
            set
            {
                if (value == m_EmergencyBrake)
                    return;
                m_EmergencyBrake = value;
                RaisePropertyChanged(() => EmergencyBrake);
            }
        }
    }
}