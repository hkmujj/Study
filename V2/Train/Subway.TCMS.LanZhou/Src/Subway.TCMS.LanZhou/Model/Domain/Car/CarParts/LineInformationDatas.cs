using Subway.TCMS.LanZhou.Config;
using Subway.TCMS.LanZhou.Config.LineInformation;
using Subway.TCMS.LanZhou.Model.Domain.Constant;

namespace Subway.TCMS.LanZhou.Model.Domain.Car.CarParts
{
    public class LineInformationDatas : CarPartBase
    {
        private CarItem<BranchInformationStatus, AtpResectionStatusConfig> m_AtpResectionStatus;
        private CarItem<BranchInformationStatus, SemiAutomaticSwitchStatusConfig> m_SemiAutomaticSwitchStatus;
        private CarItem<BranchInformationStatus, CautionButtonStatusConfig> m_CautionButtonStatus;
        private CarItem<BranchInformationStatus, TotalWindStatusConfig> m_TotalWindStatus;
        private CarItem<BranchInformationStatus, ParkingBrakeReleaseStatusConfig> m_ParkingBrakeReleaseStatus;
        private CarItem<BranchInformationStatus, CommonBrakeReleaseStatusConfig> m_CommonBrakeReleaseStatus;
        private CarItem<BranchInformationStatus, AllDoorsClosedStatusConfig> m_AllDoorsClosedStatus;
        private CarItem<BranchInformationStatus, AtpGateEnableStatusConfig> m_AtpGateEnableStatus;
        private CarItem<BranchInformationStatus, ZeroSpeedStatusConfig> m_ZeroSpeedStatus;
        private CarItem<BranchInformationStatus, LiftBowAllowStatusConfig> m_LiftBowAllowStatus;

        public CarItem<BranchInformationStatus, AtpResectionStatusConfig> AtpResectionStatus
        {
            get { return m_AtpResectionStatus; }
            set
            {
                if (value == m_AtpResectionStatus)
                {
                    return;
                }

                m_AtpResectionStatus = value;
                RaisePropertyChanged(() => AtpResectionStatus);
            }
        }

        public CarItem<BranchInformationStatus, SemiAutomaticSwitchStatusConfig> SemiAutomaticSwitchStatus
        {
            get { return m_SemiAutomaticSwitchStatus; }
            set
            {
                if (value == m_SemiAutomaticSwitchStatus)
                {
                    return;
                }

                m_SemiAutomaticSwitchStatus = value;
                RaisePropertyChanged(() => SemiAutomaticSwitchStatus);
            }
        }
        
        public CarItem<BranchInformationStatus, CautionButtonStatusConfig> CautionButtonStatus
        {
            get { return m_CautionButtonStatus; }
            set
            {
                if (value == m_CautionButtonStatus)
                {
                    return;
                }

                m_CautionButtonStatus = value;
                RaisePropertyChanged(() => CautionButtonStatus);
            }
        }

        public CarItem<BranchInformationStatus, TotalWindStatusConfig> TotalWindStatus
        {
            get { return m_TotalWindStatus; }
            set
            {
                if (value == m_TotalWindStatus)
                {
                    return;
                }

                m_TotalWindStatus = value;
                RaisePropertyChanged(() => TotalWindStatus);
            }
        }
       
        public CarItem<BranchInformationStatus, ParkingBrakeReleaseStatusConfig> ParkingBrakeReleaseStatus
        {
            get { return m_ParkingBrakeReleaseStatus; }
            set
            {
                if (value == m_ParkingBrakeReleaseStatus)
                {
                    return;
                }

                m_ParkingBrakeReleaseStatus = value;
                RaisePropertyChanged(() => ParkingBrakeReleaseStatus);
            }
        }

        public CarItem<BranchInformationStatus, CommonBrakeReleaseStatusConfig> CommonBrakeReleaseStatus
        {
            get { return m_CommonBrakeReleaseStatus; }
            set
            {
                if (value == m_CommonBrakeReleaseStatus)
                {
                    return;
                }

                m_CommonBrakeReleaseStatus = value;
                RaisePropertyChanged(() => CommonBrakeReleaseStatus);
            }
        }
        public CarItem<BranchInformationStatus, AllDoorsClosedStatusConfig> AllDoorsClosedStatus
        {
            get { return m_AllDoorsClosedStatus; }
            set
            {
                if (value == m_AllDoorsClosedStatus)
                {
                    return;
                }

                m_AllDoorsClosedStatus = value;
                RaisePropertyChanged(() => AllDoorsClosedStatus);
            }
        }

        public CarItem<BranchInformationStatus, AtpGateEnableStatusConfig> AtpGateEnableStatus
        {
            get { return m_AtpGateEnableStatus; }
            set
            {
                if (value == m_AtpGateEnableStatus)
                {
                    return;
                }

                m_AtpGateEnableStatus = value;
                RaisePropertyChanged(() => AtpGateEnableStatus);
            }
        }
        public CarItem<BranchInformationStatus, ZeroSpeedStatusConfig> ZeroSpeedStatus
        {
            get { return m_ZeroSpeedStatus; }
            set
            {
                if (value == m_ZeroSpeedStatus)
                {
                    return;
                }

                m_ZeroSpeedStatus = value;
                RaisePropertyChanged(() => ZeroSpeedStatus);
            }
        }

        public CarItem<BranchInformationStatus, LiftBowAllowStatusConfig> LiftBowAllowStatus
        {
            get { return m_LiftBowAllowStatus; }
            set
            {
                if (value == m_LiftBowAllowStatus)
                {
                    return;
                }

                m_LiftBowAllowStatus = value;
                RaisePropertyChanged(() => LiftBowAllowStatus);
            }
        }

    }
}
