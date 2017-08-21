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
    public class TrainStatusAsisstView : CarPartBase
    {
        private CarItem<OpenOrClosedStatus, PrechargeContactorStatusConfig> m_PrechargeContactorStatus;
        private CarItem<OpenOrClosedStatus, SeparationBreakerStatusConfig> m_SeparationBreakerStatus;
        private CarItem<ValidateFloatItem, TrainAssistDirectCurrentLinkVoltageConfig> m_DirectCurrentLinkVoltage;
        private CarItem<ValidateFloatItem, TrainAssistDirectCurrentLinkCurrentConfig> m_DirectCurrentLinkCurrent;
        private CarItem<OpenOrClosedStatus, AuxiliaryLoadStatusConfig> m_AuxiliaryLoadStatus;
        private CarItem<ValidateFloatItem, AuxiliaryLoadCurrentConfig> m_AuxiliaryLoadCurrent;
        private CarItem<ValidateFloatItem, AuxiliaryLoadVoltageConfig> m_AuxiliaryLoadVoltage;
        private CarItem<ValidateFloatItem, CurrentBatteryChargerConfig> m_CurrentBatteryCharger;
        private CarItem<ValidateFloatItem, BatteryBusVoltageConfig> m_BatteryBusVoltage;
        private CarItem<ValidateFloatItem, BatteryBusCurrentConfig> m_BatteryBusCurrent;

        public CarItem<OpenOrClosedStatus, PrechargeContactorStatusConfig> PrechargeContactorStatus
        {
            get { return m_PrechargeContactorStatus; }
            set
            {
                if (value == m_PrechargeContactorStatus)
                {
                    return;
                }

                m_PrechargeContactorStatus = value;
                RaisePropertyChanged(() => PrechargeContactorStatus);
            }
        }

        public CarItem<OpenOrClosedStatus, SeparationBreakerStatusConfig> SeparationBreakerStatus
        {
            get { return m_SeparationBreakerStatus; }
            set
            {
                if (value == m_SeparationBreakerStatus)
                {
                    return;
                }

                m_SeparationBreakerStatus = value;
                RaisePropertyChanged(() => SeparationBreakerStatus);
            }
        }

        public CarItem<ValidateFloatItem, TrainAssistDirectCurrentLinkVoltageConfig> DirectCurrentLinkVoltageAsisstStatus
        {
            get { return m_DirectCurrentLinkVoltage; }
            set
            {
                if (value == m_DirectCurrentLinkVoltage)
                {
                    return;
                }

                m_DirectCurrentLinkVoltage = value;
                RaisePropertyChanged(() => DirectCurrentLinkVoltageAsisstStatus);
            }
        }

        public CarItem<ValidateFloatItem, TrainAssistDirectCurrentLinkCurrentConfig> DirectCurrentLinkCurrentAssistStatus
        {
            get { return m_DirectCurrentLinkCurrent; }
            set
            {
                if (value == m_DirectCurrentLinkCurrent)
                {
                    return;
                }

                m_DirectCurrentLinkCurrent = value;
                RaisePropertyChanged(() => DirectCurrentLinkCurrentAssistStatus);
            }
        }
       
        public CarItem<OpenOrClosedStatus, AuxiliaryLoadStatusConfig> AuxiliaryLoadStatus
        {
            get { return m_AuxiliaryLoadStatus; }
            set
            {
                if (value == m_AuxiliaryLoadStatus)
                {
                    return;
                }

                m_AuxiliaryLoadStatus = value;
                RaisePropertyChanged(() => AuxiliaryLoadStatus);
            }
        }
        public CarItem<ValidateFloatItem, AuxiliaryLoadCurrentConfig> AuxiliaryLoadCurrent
        {
            get { return m_AuxiliaryLoadCurrent; }
            set
            {
                if (value == m_AuxiliaryLoadCurrent)
                {
                    return;
                }

                m_AuxiliaryLoadCurrent = value;
                RaisePropertyChanged(() => AuxiliaryLoadCurrent);
            }
        }

        public CarItem<ValidateFloatItem, AuxiliaryLoadVoltageConfig> AuxiliaryLoadVoltage
        {
            get { return m_AuxiliaryLoadVoltage; }
            set
            {
                if (value == m_AuxiliaryLoadVoltage)
                {
                    return;
                }

                m_AuxiliaryLoadVoltage = value;
                RaisePropertyChanged(() => AuxiliaryLoadVoltage);
            }
        }

        public CarItem<ValidateFloatItem, CurrentBatteryChargerConfig> CurrentBatteryCharger
        {
            get { return m_CurrentBatteryCharger; }
            set
            {
                if (value == m_CurrentBatteryCharger)
                {
                    return;
                }

                m_CurrentBatteryCharger = value;
                RaisePropertyChanged(() => CurrentBatteryCharger);
            }
        }
        public CarItem<ValidateFloatItem, BatteryBusVoltageConfig> BatteryBusVoltage
        {
            get { return m_BatteryBusVoltage; }
            set
            {
                if (value == m_BatteryBusVoltage)
                {
                    return;
                }

                m_BatteryBusVoltage = value;
                RaisePropertyChanged(() => BatteryBusVoltage);
            }
        }

        public CarItem<ValidateFloatItem, BatteryBusCurrentConfig> BatteryBusCurrent
        {
            get { return m_BatteryBusCurrent; }
            set
            {
                if (value == m_BatteryBusCurrent)
                {
                    return;
                }
                m_BatteryBusCurrent = value;
                RaisePropertyChanged(() => BatteryBusCurrent);
            }
        }
    }
}
