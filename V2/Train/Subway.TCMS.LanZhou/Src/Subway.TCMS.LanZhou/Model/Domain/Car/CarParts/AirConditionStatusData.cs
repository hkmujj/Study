
using Subway.TCMS.LanZhou.Config;
using Subway.TCMS.LanZhou.Model.Domain.Constant;

namespace Subway.TCMS.LanZhou.Model.Domain.Car.CarParts
{
    public class AirConditionStatusData:CarPartBase
    {
        private CarItem<AirConditionRunningModel, CarAirConditionRunningModelConfig> m_AirConditionRunningModelStatus;
       
        private CarItem<AirConditionRunningStatus, CarAirConditionCompressorConfig> m_AirConditionCompressor1Status;
        private CarItem<AirConditionRunningStatus, CarAirConditionCompressorConfig> m_AirConditionCompressor2Status;
        private CarItem<AirConditionRunningStatus, CarAirConditionCompressorConfig> m_AirConditionCompressor3Status;
        private CarItem<AirConditionRunningStatus, CarAirConditionCompressorConfig> m_AirConditionCompressor4Status;

        private CarItem<AirConditionRunningStatus, CarAirConditionCondensingMachineConfig> m_AirConditionCondensing1Status;
        private CarItem<AirConditionRunningStatus, CarAirConditionCondensingMachineConfig> m_AirConditionCondensing2Status;
        private CarItem<AirConditionRunningStatus, CarAirConditionCondensingMachineConfig> m_AirConditionCondensing3Status;
        private CarItem<AirConditionRunningStatus, CarAirConditionCondensingMachineConfig> m_AirConditionCondensing4Status;

        private CarItem<AirConditionRunningStatus, CarAirConditionVentilatorConfig> m_AirConditionVentilator1Status;
        private CarItem<AirConditionRunningStatus, CarAirConditionVentilatorConfig> m_AirConditionVentilator2Status;
        private CarItem<AirConditionRunningStatus, CarAirConditionVentilatorConfig> m_AirConditionVentilator3Status;
        private CarItem<AirConditionRunningStatus, CarAirConditionVentilatorConfig> m_AirConditionVentilator4Status;

        private CarItem<AirConditionOutsideDamper, CarAirConditionOutsideDamperStatusConfig> m_AirConditionAirConditionOutsideDamper1Status;
        private CarItem<AirConditionOutsideDamper, CarAirConditionOutsideDamperStatusConfig> m_AirConditionAirConditionOutsideDamper2Status;

        public CarItem<AirConditionRunningModel, CarAirConditionRunningModelConfig> AirConditionRunningStatus
        {
            get { return m_AirConditionRunningModelStatus; }
            set
            {
                if (value == m_AirConditionRunningModelStatus)
                {
                    return;
                }

                m_AirConditionRunningModelStatus = value;
                RaisePropertyChanged(() => AirConditionRunningStatus);
            }
        }

        public CarItem<AirConditionRunningStatus, CarAirConditionCompressorConfig> AirConditionCompressor1Status
        {
            get { return m_AirConditionCompressor1Status; }
            set
            {
                if (value == m_AirConditionCompressor1Status)
                {
                    return;
                }

                m_AirConditionCompressor1Status = value;
                RaisePropertyChanged(() => AirConditionCompressor1Status);
            }
        }
        public CarItem<AirConditionRunningStatus, CarAirConditionCompressorConfig> AirConditionCompressor2Status
        {
            get { return m_AirConditionCompressor2Status; }
            set
            {
                if (value == m_AirConditionCompressor2Status)
                {
                    return;
                }

                m_AirConditionCompressor2Status = value;
                RaisePropertyChanged(() => AirConditionCompressor2Status);
            }
        }
        public CarItem<AirConditionRunningStatus, CarAirConditionCompressorConfig> AirConditionCompressor3Status
        {
            get { return m_AirConditionCompressor3Status; }
            set
            {
                if (value == m_AirConditionCompressor3Status)
                {
                    return;
                }

                m_AirConditionCompressor3Status = value;
                RaisePropertyChanged(() => AirConditionCompressor3Status);
            }
        }
        public CarItem<AirConditionRunningStatus, CarAirConditionCompressorConfig> AirConditionCompressor4Status
        {
            get { return m_AirConditionCompressor4Status; }
            set
            {
                if (value == m_AirConditionCompressor4Status)
                {
                    return;
                }

                m_AirConditionCompressor4Status = value;
                RaisePropertyChanged(() => AirConditionCompressor4Status);
            }
        }


        public CarItem<AirConditionRunningStatus, CarAirConditionCondensingMachineConfig> AirConditionCondensing1Status
        {
            get { return m_AirConditionCondensing1Status; }
            set
            {
                if (value == m_AirConditionCondensing1Status)
                {
                    return;
                }

                m_AirConditionCondensing1Status = value;
                RaisePropertyChanged(() => AirConditionCondensing1Status);
            }
        }
        public CarItem<AirConditionRunningStatus, CarAirConditionCondensingMachineConfig> AirConditionCondensing2Status
        {
            get { return m_AirConditionCondensing2Status; }
            set
            {
                if (value == m_AirConditionCondensing2Status)
                {
                    return;
                }

                m_AirConditionCondensing2Status = value;
                RaisePropertyChanged(() => AirConditionCondensing2Status);
            }
        }
        public CarItem<AirConditionRunningStatus, CarAirConditionCondensingMachineConfig> AirConditionCondensing3Status
        {
            get { return m_AirConditionCondensing3Status; }
            set
            {
                if (value == m_AirConditionCondensing3Status)
                {
                    return;
                }

                m_AirConditionCondensing3Status = value;
                RaisePropertyChanged(() => AirConditionCondensing3Status);
            }
        }
        public CarItem<AirConditionRunningStatus, CarAirConditionCondensingMachineConfig> AirConditionCondensing4Status
        {
            get { return m_AirConditionCondensing4Status; }
            set
            {
                if (value == m_AirConditionCondensing4Status)
                {
                    return;
                }

                m_AirConditionCondensing4Status = value;
                RaisePropertyChanged(() => AirConditionCondensing4Status);
            }
        }

        public CarItem<AirConditionRunningStatus, CarAirConditionVentilatorConfig> AirConditionVentilator1Status
        {
            get { return m_AirConditionVentilator1Status; }
            set
            {
                if (value == m_AirConditionVentilator1Status)
                {
                    return;
                }

                m_AirConditionVentilator1Status = value;
                RaisePropertyChanged(() => AirConditionVentilator1Status);
            }
        }
        public CarItem<AirConditionRunningStatus, CarAirConditionVentilatorConfig> AirConditionVentilator2Status
        {
            get { return m_AirConditionVentilator2Status; }
            set
            {
                if (value == m_AirConditionVentilator2Status)
                {
                    return;
                }

                m_AirConditionVentilator2Status = value;
                RaisePropertyChanged(() => AirConditionVentilator2Status);
            }
        }
        public CarItem<AirConditionRunningStatus, CarAirConditionVentilatorConfig> AirConditionVentilator3Status
        {
            get { return m_AirConditionVentilator3Status; }
            set
            {
                if (value == m_AirConditionVentilator3Status)
                {
                    return;
                }

                m_AirConditionVentilator3Status = value;
                RaisePropertyChanged(() => AirConditionVentilator3Status);
            }
        }
        public CarItem<AirConditionRunningStatus, CarAirConditionVentilatorConfig> AirConditionVentilator4Status
        {
            get { return m_AirConditionVentilator4Status; }
            set
            {
                if (value == m_AirConditionVentilator4Status)
                {
                    return;
                }

                m_AirConditionVentilator4Status = value;
                RaisePropertyChanged(() => AirConditionVentilator4Status);
            }
        }

        public CarItem<AirConditionOutsideDamper, CarAirConditionOutsideDamperStatusConfig> AirConditionOutsideDamper1Status
        {
            get { return m_AirConditionAirConditionOutsideDamper1Status; }
            set
            {
                if (value == m_AirConditionAirConditionOutsideDamper1Status)
                {
                    return;
                }

                m_AirConditionAirConditionOutsideDamper1Status = value;
                RaisePropertyChanged(() => AirConditionOutsideDamper1Status);
            }
        }
        public CarItem<AirConditionOutsideDamper, CarAirConditionOutsideDamperStatusConfig> AirConditionOutsideDamper2Status
        {
            get { return m_AirConditionAirConditionOutsideDamper2Status; }
            set
            {
                if (value == m_AirConditionAirConditionOutsideDamper2Status)
                {
                    return;
                }

                m_AirConditionAirConditionOutsideDamper2Status = value;
                RaisePropertyChanged(() => AirConditionOutsideDamper2Status);
            }
        }
    }
}
