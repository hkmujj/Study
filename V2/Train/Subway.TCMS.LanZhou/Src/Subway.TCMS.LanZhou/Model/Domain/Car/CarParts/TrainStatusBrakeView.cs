using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Subway.TCMS.LanZhou.Config;
using Subway.TCMS.LanZhou.Config.TrainStatus;
using Subway.TCMS.LanZhou.Model.Domain.Constant;
using Subway.TCMS.LanZhou.Model.Domain.Constant.TrainStatus;

namespace Subway.TCMS.LanZhou.Model.Domain.Car.CarParts
{
   public class TrainStatusBrakeView : CarPartBase
    {
        private CarItem<AirBrakeAvailableStatus, AirBrakeAvailableStatusConfig> m_AirBrakeAvailableStatus1;
        private CarItem<AirBrakeAvailableStatus, AirBrakeAvailableStatusConfig> m_AirBrakeAvailableStatus2;

        private CarItem<ExertOrNotappliedStatus, AirBrakeApplicationStatusConfig> m_AirBrakeApplicationStatus1;
        private CarItem<ExertOrNotappliedStatus, AirBrakeApplicationStatusConfig> m_AirBrakeApplicationStatus2;

        private CarItem<ExertOrNotappliedStatus, HoldBrakeApplyStatusConfig> m_HoldBrakeApplyStatus1;
        private CarItem<ExertOrNotappliedStatus, HoldBrakeApplyStatusConfig> m_HoldBrakeApplyStatus2;

        private CarItem<EmergencyBrakeAvailableStatus, EmergencyBrakeAvailableStatusConfig> m_EmergencyBrakeAvailableStatus1;
        private CarItem<EmergencyBrakeAvailableStatus, EmergencyBrakeAvailableStatusConfig> m_EmergencyBrakeAvailableStatus2;

        private CarItem<ExertOrNotappliedStatus, EmergencyBrakeApplicationStatusConfig> m_EmergencyBrakeApplicationStatus1;
        private CarItem<ExertOrNotappliedStatus, EmergencyBrakeApplicationStatusConfig> m_EmergencyBrakeApplicationStatus2;

        private CarItem<ParkingBrakeStatus, ParkingReleaseTrainStatusConfig> m_ParkingBrakeStatus;

        private CarItem<BrakeCylinderStatus, BrakeCylinderStatusConfig> m_BrakeCylinderStatus1;
        private CarItem<BrakeCylinderStatus, BrakeCylinderStatusConfig> m_BrakeCylinderStatus2;

        private CarItem<BogieResectionStatus, BogieResectionStatusConfig> m_BogieResectionStatus1;
        private CarItem<BogieResectionStatus, BogieResectionStatusConfig> m_BogieResectionStatus2;

        private CarItem<AirSpringPreStatus, AirSpringPreStatusConfig> m_AirSpringPreStatus1;
        private CarItem<AirSpringPreStatus, AirSpringPreStatusConfig> m_AirSpringPreStatus2;

        private CarItem<ValidateFloatItem, AirSpringPreDataConfig> m_AirSpringPreData1;
        private CarItem<ValidateFloatItem, AirSpringPreDataConfig> m_AirSpringPreData2;

        public CarItem<AirBrakeAvailableStatus, AirBrakeAvailableStatusConfig> AirBrakeAvailableStatus1
        {
            get { return m_AirBrakeAvailableStatus1; }
            set
            {
                if (value == m_AirBrakeAvailableStatus1)
                {
                    return;
                }

                m_AirBrakeAvailableStatus1 = value;
                RaisePropertyChanged(() => AirBrakeAvailableStatus1);
            }
        }

        public CarItem<AirBrakeAvailableStatus, AirBrakeAvailableStatusConfig> AirBrakeAvailableStatus2
        {
            get { return m_AirBrakeAvailableStatus2; }
            set
            {
                if (value == m_AirBrakeAvailableStatus2)
                {
                    return;
                }

                m_AirBrakeAvailableStatus2 = value;
                RaisePropertyChanged(() => AirBrakeAvailableStatus2);
            }
        }

        public CarItem<ExertOrNotappliedStatus, AirBrakeApplicationStatusConfig> AirBrakeApplicationStatus1
        {
            get { return m_AirBrakeApplicationStatus1; }
            set
            {
                if (value == m_AirBrakeApplicationStatus1)
                {
                    return;
                }

                m_AirBrakeApplicationStatus1 = value;
                RaisePropertyChanged(() => AirBrakeApplicationStatus1);
            }
        }

        public CarItem<ExertOrNotappliedStatus, AirBrakeApplicationStatusConfig> AirBrakeApplicationStatus2
        {
            get { return m_AirBrakeApplicationStatus2; }
            set
            {
                if (value == m_AirBrakeApplicationStatus2)
                {
                    return;
                }

                m_AirBrakeApplicationStatus2 = value;
                RaisePropertyChanged(() => AirBrakeApplicationStatus1);
            }
        }

        public CarItem<ExertOrNotappliedStatus, HoldBrakeApplyStatusConfig> HoldBrakeApplyStatus1
        {
            get { return m_HoldBrakeApplyStatus1; }
            set
            {
                if (value == m_HoldBrakeApplyStatus1)
                {
                    return;
                }

                m_HoldBrakeApplyStatus1 = value;
                RaisePropertyChanged(() => HoldBrakeApplyStatus1);
            }
        }

        public CarItem<ExertOrNotappliedStatus, HoldBrakeApplyStatusConfig> HoldBrakeApplyStatus2
        {
            get { return m_HoldBrakeApplyStatus2; }
            set
            {
                if (value == m_HoldBrakeApplyStatus2)
                {
                    return;
                }

                m_HoldBrakeApplyStatus2 = value;
                RaisePropertyChanged(() => HoldBrakeApplyStatus2);
            }
        }

        public CarItem<EmergencyBrakeAvailableStatus, EmergencyBrakeAvailableStatusConfig> EmergencyBrakeAvailableStatus1
        {
            get { return m_EmergencyBrakeAvailableStatus1; }
            set
            {
                if (value == m_EmergencyBrakeAvailableStatus1)
                {
                    return;
                }

                m_EmergencyBrakeAvailableStatus1 = value;
                RaisePropertyChanged(() => EmergencyBrakeAvailableStatus1);
            }
        }

        public CarItem<EmergencyBrakeAvailableStatus, EmergencyBrakeAvailableStatusConfig> EmergencyBrakeAvailableStatus2
        {
            get { return m_EmergencyBrakeAvailableStatus2; }
            set
            {
                if (value == m_EmergencyBrakeAvailableStatus2)
                {
                    return;
                }

                m_EmergencyBrakeAvailableStatus2 = value;
                RaisePropertyChanged(() => EmergencyBrakeAvailableStatus2);
            }
        }

        public CarItem<ExertOrNotappliedStatus, EmergencyBrakeApplicationStatusConfig> EmergencyBrakeApplicationStatus1
        {
            get { return m_EmergencyBrakeApplicationStatus1; }
            set
            {
                if (value == m_EmergencyBrakeApplicationStatus1)
                {
                    return;
                }

                m_EmergencyBrakeApplicationStatus1 = value;
                RaisePropertyChanged(() => EmergencyBrakeApplicationStatus1);
            }
        }

        public CarItem<ExertOrNotappliedStatus, EmergencyBrakeApplicationStatusConfig> EmergencyBrakeApplicationStatus2
        {
            get { return m_EmergencyBrakeApplicationStatus2; }
            set
            {
                if (value == m_EmergencyBrakeApplicationStatus2)
                {
                    return;
                }

                m_EmergencyBrakeApplicationStatus2 = value;
                RaisePropertyChanged(() => EmergencyBrakeApplicationStatus2);
            }
        }

        public CarItem<ParkingBrakeStatus, ParkingReleaseTrainStatusConfig> ParkingBrakeStatus
        {
            get { return m_ParkingBrakeStatus; }
            set
            {
                if (value == m_ParkingBrakeStatus)
                {
                    return;
                }

                m_ParkingBrakeStatus = value;
                RaisePropertyChanged(() => ParkingBrakeStatus);
            }
        }

        public CarItem<BrakeCylinderStatus, BrakeCylinderStatusConfig> BrakeCylinderStatus1
        {
            get { return m_BrakeCylinderStatus1; }
            set
            {
                if (value == m_BrakeCylinderStatus1)
                {
                    return;
                }

                m_BrakeCylinderStatus1 = value;
                RaisePropertyChanged(() => BrakeCylinderStatus1);
            }
        }

        public CarItem<BrakeCylinderStatus, BrakeCylinderStatusConfig> BrakeCylinderStatus2
        {
            get { return m_BrakeCylinderStatus2; }
            set
            {
                if (value == m_BrakeCylinderStatus2)
                {
                    return;
                }

                m_BrakeCylinderStatus2 = value;
                RaisePropertyChanged(() => BrakeCylinderStatus2);
            }
        }

        public CarItem<BogieResectionStatus, BogieResectionStatusConfig> BogieResectionStatus1
        {
            get { return m_BogieResectionStatus1; }
            set
            {
                if (value == m_BogieResectionStatus1)
                {
                    return;
                }

                m_BogieResectionStatus1 = value;
                RaisePropertyChanged(() => BogieResectionStatus1);
            }
        }

        public CarItem<BogieResectionStatus, BogieResectionStatusConfig> BogieResectionStatus2
        {
            get { return m_BogieResectionStatus2; }
            set
            {
                if (value == m_BogieResectionStatus2)
                {
                    return;
                }

                m_BogieResectionStatus2 = value;
                RaisePropertyChanged(() => BogieResectionStatus2);
            }
        }

        public CarItem<AirSpringPreStatus, AirSpringPreStatusConfig> AirSpringPreStatus1
        {
            get { return m_AirSpringPreStatus1; }
            set
            {
                if (value == m_AirSpringPreStatus1)
                {
                    return;
                }

                m_AirSpringPreStatus1 = value;
                RaisePropertyChanged(() => AirSpringPreStatus1);
            }
        }

        public CarItem<AirSpringPreStatus, AirSpringPreStatusConfig> AirSpringPreStatus2
        {
            get { return m_AirSpringPreStatus2; }
            set
            {
                if (value == m_AirSpringPreStatus2)
                {
                    return;
                }

                m_AirSpringPreStatus2 = value;
                RaisePropertyChanged(() => AirSpringPreStatus2);
            }
        }
        
        public CarItem<ValidateFloatItem, AirSpringPreDataConfig> AirSpringPreData1
        {
            get { return m_AirSpringPreData1; }
            set
            {
                if (value == m_AirSpringPreData1)
                {
                    return;
                }

                m_AirSpringPreData1 = value;
                RaisePropertyChanged(() => AirSpringPreData1);
            }
        }

        public CarItem<ValidateFloatItem, AirSpringPreDataConfig> AirSpringPreData2
        {
            get { return m_AirSpringPreData2; }
            set
            {
                if (value == m_AirSpringPreData2)
                {
                    return;
                }

                m_AirSpringPreData2 = value;
                RaisePropertyChanged(() => AirSpringPreData2);
            }
        }
    }
}
