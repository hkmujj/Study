
using Subway.TCMS.LanZhou.Config;
using Subway.TCMS.LanZhou.Model.Domain.Constant;

namespace Subway.TCMS.LanZhou.Model.Domain.Car.CarParts
{
    public class TrainBodyViewData : CarPartBase
    {
        private CarItem<TowState, TowSystemConfig> m_TowState;

        private CarItem<AirCompressorStatus, AirCompressorConfig> m_AirCompressorStatus;

        private CarItem<BrakeStatus, BrakeStatusConfig> m_Brake1Status;

        private CarItem<BrakeStatus, BrakeStatusConfig> m_Brake2Status;

        private CarItem<TrainParkingBrakeStatus, ParkingBrakeConfig> m_ParkingBrakeStatus;

        private CarItem<AuxiliarySystemStatus, AuxiliarySystemStatusConfig> m_AuxiliarySystemStatus;

        public CarItem<AuxiliarySystemStatus, AuxiliarySystemStatusConfig> AuxiliarySystemStatus
        {
            get { return m_AuxiliarySystemStatus; }
            set
            {
                if (value == m_AuxiliarySystemStatus)
                {
                    return;
                }

                m_AuxiliarySystemStatus = value;
                RaisePropertyChanged(() => AuxiliarySystemStatus);
            }
        }
        public CarItem<TowState, TowSystemConfig> TowState
        {
            get { return m_TowState; }
            set
            {
                if (value == m_TowState)
                {
                    return;
                }

                m_TowState = value;
                RaisePropertyChanged(() => TowState);
            }
        }

        public CarItem<AirCompressorStatus, AirCompressorConfig> AirCompressorStatus
        {
            get { return m_AirCompressorStatus; }
            set
            {
                if (value == m_AirCompressorStatus)
                {
                    return;
                }

                m_AirCompressorStatus = value;
                RaisePropertyChanged(() => AirCompressorStatus);
            }
        }

        public CarItem<BrakeStatus, BrakeStatusConfig> Brake1Status
        {
            get { return m_Brake1Status; }
            set
            {
                if (value == m_Brake1Status)
                {
                    return;
                }

                m_Brake1Status = value;
                RaisePropertyChanged(() => Brake1Status);
            }
        }

        public CarItem<BrakeStatus, BrakeStatusConfig> Brake2Status
        {
            get { return m_Brake2Status; }
            set
            {
                if (value == m_Brake2Status)
                {
                    return;
                }

                m_Brake2Status = value;
                RaisePropertyChanged(() => Brake2Status);
            }
        }

        public CarItem<TrainParkingBrakeStatus, ParkingBrakeConfig> ParkingBrakeStatus
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
    }
}
