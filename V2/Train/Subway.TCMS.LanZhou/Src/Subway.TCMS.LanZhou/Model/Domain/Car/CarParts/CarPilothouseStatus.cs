using Subway.TCMS.LanZhou.Model.Domain.Constant;

namespace Subway.TCMS.LanZhou.Model.Domain.Car.CarParts
{
    public class CarPilothouseStatus : CarPartBase
    {
        private SpaceGateStatus m_SpaceGateStatus;
        private CabStatus m_CabStatus;
        private EmergencyExitStatus m_EmergencyExitStatus;

        public SpaceGateStatus SpaceGateStatus
        {
            get { return m_SpaceGateStatus; }
            set
            {
                if (value == m_SpaceGateStatus)
                {
                    return;
                }

                m_SpaceGateStatus = value;
                RaisePropertyChanged(() => SpaceGateStatus);
            }
        }

        public CabStatus CabStatus
        {
            get { return m_CabStatus; }
            set
            {
                if (value == m_CabStatus)
                {
                    return;
                }

                m_CabStatus = value;
                RaisePropertyChanged(() => CabStatus);
            }
        }

        public EmergencyExitStatus EmergencyExitStatus
        {
            get { return m_EmergencyExitStatus; }
            set
            {
                if (value == m_EmergencyExitStatus)
                {
                    return;
                }

                m_EmergencyExitStatus = value;
                RaisePropertyChanged(() => EmergencyExitStatus);
            }
        }
    }
}
