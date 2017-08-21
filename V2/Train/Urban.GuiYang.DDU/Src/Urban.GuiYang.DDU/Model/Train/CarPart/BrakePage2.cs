using Urban.GuiYang.DDU.Config;

namespace Urban.GuiYang.DDU.Model.Train.CarPart
{
    public class BrakePage2 : CarPartBase
    {

        private CarItem<ValidateFloatItem, CarMainPressureConfig> m_MainPressure;

        private CarItem<ValidateFloatItem, CarBrakePressureConfig> m_BrakePressure1;

        private CarItem<ValidateFloatItem, CarBrakePressureConfig> m_BrakePressure2;

        private CarItem<ValidateFloatItem, CarAirSpringPreesureConfig> m_AirSpringPreesure1;

        private CarItem<ValidateFloatItem, CarAirSpringPreesureConfig> m_AirSpringPreesure2;

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
        public CarItem<ValidateFloatItem, CarAirSpringPreesureConfig> AirSpringPreesure1
        {
            get { return m_AirSpringPreesure1; }
            set
            {
                if (value == m_AirSpringPreesure1)
                {
                    return;
                }

                m_AirSpringPreesure1 = value;
                RaisePropertyChanged(() => AirSpringPreesure1);
            }
        }

        public CarItem<ValidateFloatItem, CarAirSpringPreesureConfig> AirSpringPreesure2
        {
            get { return m_AirSpringPreesure2; }
            set
            {
                if (value == m_AirSpringPreesure2)
                {
                    return;
                }

                m_AirSpringPreesure2 = value;
                RaisePropertyChanged(() => AirSpringPreesure2);
            }
        }
    }
}