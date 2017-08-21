
using Subway.TCMS.LanZhou.Config;

namespace Subway.TCMS.LanZhou.Model.Domain.Car.CarParts
{
    public class AirConditionFloatData : CarPartBase
    {
        private CarItem<ValidateFloatItem, CarAirConditionTargetTempConfig> m_AirConditionTargetTemp;
        private CarItem<ValidateFloatItem, CarAirConditionIndoorTempConfig> m_AirConditionIndoorTemp;
        private CarItem<ValidateFloatItem, CarAirConditionOutdoorTempConfig> m_AirConditionOutdoorTemp;

        public CarItem<ValidateFloatItem, CarAirConditionTargetTempConfig> AirConditionTargetTemp
        {
            get { return m_AirConditionTargetTemp; }
            set
            {
                if (value == m_AirConditionTargetTemp)
                {
                    return;
                }

                m_AirConditionTargetTemp = value;
                RaisePropertyChanged(() => AirConditionTargetTemp);
            }
        }

        public CarItem<ValidateFloatItem, CarAirConditionIndoorTempConfig> AirConditionIndoorTemp
        {
            get { return m_AirConditionIndoorTemp; }
            set
            {
                if (value == m_AirConditionIndoorTemp)
                {
                    return;
                }

                m_AirConditionIndoorTemp = value;
                RaisePropertyChanged(() => AirConditionIndoorTemp);
            }
        }

        public CarItem<ValidateFloatItem, CarAirConditionOutdoorTempConfig> AirConditionOutdoorTemp
        {
            get { return m_AirConditionOutdoorTemp; }
            set
            {
                if (value == m_AirConditionOutdoorTemp)
                {
                    return;
                }

                m_AirConditionOutdoorTemp = value;
                RaisePropertyChanged(() => AirConditionOutdoorTemp);
            }
        }
    }
}
