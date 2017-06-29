using CBTC.Infrasturcture.Model.Constant;
using Microsoft.Practices.Prism.ViewModel;

namespace CBTC.Infrasturcture.Model.Fault
{
    public class FaultInfo : NotificationObject
    {
        private FaultLocation m_FaultLocation;
        private bool m_HasFault;
        private bool m_RADRedFault;
        private bool m_ATPYellowFault;
        private bool m_ATORedFault;
        private bool m_AMRedFault;

        public bool HasFault
        {
            get { return m_HasFault; }
            set
            {
                if (value == m_HasFault)
                    return;

                m_HasFault = value;
                RaisePropertyChanged(() => HasFault);
            }
        }

        /// <summary>
        /// 无线通信中断
        /// </summary>
        public bool RADRedFault
        {
            get { return m_RADRedFault; }
            set
            {
                if (value == m_RADRedFault)
                    return;

                m_RADRedFault = value;
                RaisePropertyChanged(() => RADRedFault);
            }
        }

        /// <summary>
        /// ATP故障
        /// </summary>
        public bool ATPYellowFault
        {
            get { return m_ATPYellowFault; }
            set
            {
                if (value == m_ATPYellowFault)
                    return;

                m_ATPYellowFault = value;
                RaisePropertyChanged(() => ATPYellowFault);
            }
        }

        /// <summary>
        /// ATO故障
        /// </summary>
        public bool ATORedFault
        {
            get { return m_ATORedFault; }
            set
            {
                if (value == m_ATORedFault)
                    return;

                m_ATORedFault = value;
                RaisePropertyChanged(() => ATORedFault);
            }
        }

        /// <summary>
        /// AM不可用
        /// </summary>
        public bool AMRedFault
        {
            get { return m_AMRedFault; }
            set
            {
                if (value == m_AMRedFault)
                    return;

                m_AMRedFault = value;
                RaisePropertyChanged(() => AMRedFault);
            }
        }

        public FaultLocation FaultLocation
        {
            get { return m_FaultLocation; }
            set
            {
                if (value == m_FaultLocation)
                    return;

                m_FaultLocation = value;
                HasFault = value != FaultLocation.None;
                RaisePropertyChanged(() => FaultLocation);
            }
        }
    }
}