
using Subway.TCMS.LanZhou.Config;

namespace Subway.TCMS.LanZhou.Model.Domain.Car.CarParts
{
    public class RunningViewFloatData : CarPartBase
    {
        private CarItem<ValidateFloatItem, CarLadenConfig> m_Laden;

        private CarItem<ValidateFloatItem, CarBrakePressureConfig> m_BrakePressure1;

        private CarItem<ValidateFloatItem, CarBrakePressureConfig> m_BrakePressure2;

        private CarItem<ValidateFloatItem, CarAirConditionConfig> m_AirCoditionTemp;

        private CarItem<ValidateFloatItem, CarMainPressureConfig> m_MainPressure;

        public CarItem<ValidateFloatItem, CarLadenConfig> Laden
        {
            get { return m_Laden; }
            set
            {
                if (value == m_Laden)
                {
                    return;
                }

                m_Laden = value;
                RaisePropertyChanged(() => Laden);
            }
        }

        public CarItem<ValidateFloatItem, CarBrakePressureConfig> BrakePressure1
        {
            get { return m_BrakePressure1; }
            set
            {
                if (value == m_BrakePressure1)
                {
                    return;
                }

                m_BrakePressure1 = value;
                RaisePropertyChanged(() => BrakePressure1);
            }
        }

        public CarItem<ValidateFloatItem, CarBrakePressureConfig> BrakePressure2
        {
            get { return m_BrakePressure2; }
            set
            {
                if (value == m_BrakePressure2)
                {
                    return;
                }

                m_BrakePressure2 = value;
                RaisePropertyChanged(() => BrakePressure2);
            }
        }

        public CarItem<ValidateFloatItem, CarMainPressureConfig> MainPressure
        {
            get { return m_MainPressure; }
            set
            {
                if (value == m_MainPressure)
                {
                    return;
                }

                m_MainPressure = value;
                RaisePropertyChanged(() => MainPressure);
            }
        }

        public CarItem<ValidateFloatItem, CarAirConditionConfig> AirCoditionTemp
        {
            get { return m_AirCoditionTemp; }
            set
            {
                if (value == m_AirCoditionTemp)
                {
                    return;
                }

                m_AirCoditionTemp = value;
                RaisePropertyChanged(() => AirCoditionTemp);
            }
        }
    }
}
