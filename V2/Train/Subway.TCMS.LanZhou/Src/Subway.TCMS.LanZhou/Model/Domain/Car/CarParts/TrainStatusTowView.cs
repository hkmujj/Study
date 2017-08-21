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
    public class TrainStatusTowView : CarPartBase
    {
        private CarItem<OpenOrClosedStatus, WorkshopPowerProtectStatusConfig> m_WorkshopPowerProtectStatus;

        private CarItem<IesContactorStatus, IesContactorStatusConfig> m_IesContactorStatus;

        private CarItem<WorkshopPowerStatus, WorkshopPowerStatusConfig> m_WorkshopPowerStatus;

        private CarItem<TractionSafetyStatus, TractionSafetyStatusConfig> m_TractionSafetyStatus;

        private CarItem<OpenOrClosedStatus, HighSpeedCircuitBreakerStatus1Config> m_HighSpeedCircuitBreakerStatus1;

        private CarItem<OpenOrClosedStatus, HighSpeedCircuitBreakerStatus2Config> m_HighSpeedCircuitBreakerStatus2;

        private CarItem<OpenOrClosedStatus, SeparationgContactorStatusConfig> m_SeparationgContactorStatus;

        private CarItem<ValidateFloatItem, DirectCurrentLinkVoltageConfig> m_DirectCurrentLinkVoltage;
        
        private CarItem<ValidateFloatItem, DirectCurrentLinkCurrentConfig> m_DirectCurrentLinkCurrent;

        private CarItem<ValidateFloatItem, MotorCurrentConfig> m_MotorCurrent;
        
        private CarItem<OvervoltageStatus, OvervoltageAbsorptionDeviceConfig> m_OvervoltageStatus;

        
        public CarItem<OpenOrClosedStatus, WorkshopPowerProtectStatusConfig> WorkshopPowerProtectStatus
        {
            get { return m_WorkshopPowerProtectStatus; }
            set
            {
                if (value == m_WorkshopPowerProtectStatus)
                {
                    return;
                }

                m_WorkshopPowerProtectStatus = value;
                RaisePropertyChanged(() => WorkshopPowerProtectStatus);
            }
        }

        public CarItem<IesContactorStatus, IesContactorStatusConfig> IesContactorStatus
        {
            get { return m_IesContactorStatus; }
            set
            {
                if (value == m_IesContactorStatus)
                {
                    return;
                }

                m_IesContactorStatus = value;
                RaisePropertyChanged(() => IesContactorStatus);
            }
        }

        public CarItem<WorkshopPowerStatus, WorkshopPowerStatusConfig> WorkshopPowerStatus
        {
            get { return m_WorkshopPowerStatus; }
            set
            {
                if (value == m_WorkshopPowerStatus)
                {
                    return;
                }

                m_WorkshopPowerStatus = value;
                RaisePropertyChanged(() => WorkshopPowerStatus);
            }
        }
        
        public CarItem<TractionSafetyStatus, TractionSafetyStatusConfig> TractionSafetyStatus
        {
            get { return m_TractionSafetyStatus; }
            set
            {
                if (value == m_TractionSafetyStatus)
                {
                    return;
                }

                m_TractionSafetyStatus = value;
                RaisePropertyChanged(() => TractionSafetyStatus);
            }
        }

        public CarItem<OpenOrClosedStatus, HighSpeedCircuitBreakerStatus1Config> HighSpeedCircuitBreaker1Status
        {
            get { return m_HighSpeedCircuitBreakerStatus1; }
            set
            {
                if (value == m_HighSpeedCircuitBreakerStatus1)
                {
                    return;
                }

                m_HighSpeedCircuitBreakerStatus1 = value;
                RaisePropertyChanged(() => HighSpeedCircuitBreaker1Status);
            }
        }
        public CarItem<OpenOrClosedStatus, HighSpeedCircuitBreakerStatus2Config> HighSpeedCircuitBreaker2Status
        {
            get { return m_HighSpeedCircuitBreakerStatus2; }
            set
            {
                if (value == m_HighSpeedCircuitBreakerStatus2)
                {
                    return;
                }

                m_HighSpeedCircuitBreakerStatus2 = value;
                RaisePropertyChanged(() => HighSpeedCircuitBreaker2Status);
            }
        }
      
        public CarItem<OpenOrClosedStatus, SeparationgContactorStatusConfig> SeparationgContactorStatus
        {
            get { return m_SeparationgContactorStatus; }
            set
            {
                if (value == m_SeparationgContactorStatus)
                {
                    return;
                }

                m_SeparationgContactorStatus = value;
                RaisePropertyChanged(() => SeparationgContactorStatus);
            }
        }

        public CarItem<ValidateFloatItem, DirectCurrentLinkVoltageConfig> DirectCurrentLinkVoltage
        {
            get { return m_DirectCurrentLinkVoltage; }
            set
            {
                if (value == m_DirectCurrentLinkVoltage)
                {
                    return;
                }

                m_DirectCurrentLinkVoltage = value;
                RaisePropertyChanged(() => DirectCurrentLinkVoltage);
            }
        }
 
        public CarItem<ValidateFloatItem, DirectCurrentLinkCurrentConfig> DirectCurrentLinkCurrent
        {
            get { return m_DirectCurrentLinkCurrent; }
            set
            {
                if (value == m_DirectCurrentLinkCurrent)
                {
                    return;
                }

                m_DirectCurrentLinkCurrent = value;
                RaisePropertyChanged(() => DirectCurrentLinkCurrent);
            }
        }
        
        public CarItem<ValidateFloatItem, MotorCurrentConfig> MotorCurrent
        {
            get { return m_MotorCurrent; }
            set
            {
                if (value == m_MotorCurrent)
                {
                    return;
                }

                m_MotorCurrent = value;
                RaisePropertyChanged(() => MotorCurrent);
            }
        }
    
       public CarItem<OvervoltageStatus, OvervoltageAbsorptionDeviceConfig> OvervoltageStatus
        {
            get { return m_OvervoltageStatus; }
            set
            {
                if (value == m_OvervoltageStatus)
                {
                    return;
                }

                m_OvervoltageStatus = value;
                RaisePropertyChanged(() => OvervoltageStatus);
            }
        }
    }
}
