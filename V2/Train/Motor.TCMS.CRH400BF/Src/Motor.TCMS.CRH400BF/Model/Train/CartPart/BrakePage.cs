using Microsoft.Practices.Prism.ViewModel;
using Motor.TCMS.CRH400BF.Config;

namespace Motor.TCMS.CRH400BF.Model.Train.CartPart
{
    public class BrakePage : NotificationObject
    {

        private CarItem<ValidateEleBrakeItem, CarEleBrakeConfig> m_EleBrake;
        private CarItem<ValidateAirBrakeItem, CarAirBrakeConfig> m_AirBrake;
        private CarItem<ValidateParkBrakeItem, CarParkBrakeConfig> m_ParkBrake;

     
        public CarItem<ValidateEleBrakeItem, CarEleBrakeConfig> EleBrake
        {
            get { return m_EleBrake; }
            set
            {
                if (value == m_EleBrake)
                {
                    return;
                }

                m_EleBrake = value;
                RaisePropertyChanged(() => EleBrake);
            }
        }

        //需改动
        public CarItem<ValidateAirBrakeItem, CarAirBrakeConfig> AirBrake
        {
            get { return m_AirBrake; }
            set
            {
                if (value == m_AirBrake)
                {
                    return;
                }

                m_AirBrake = value;
                RaisePropertyChanged(() => AirBrake);
            }
        }



        public CarItem<ValidateParkBrakeItem, CarParkBrakeConfig> ParkBrake
        {
            get { return m_ParkBrake; }
            set
            {
                if (value == m_ParkBrake)
                {
                    return;
                }

                m_ParkBrake = value;
                RaisePropertyChanged(() => ParkBrake);
            }
        }
    }
}
