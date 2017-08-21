using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.LCDM.HDX2.Entity.Model.Domain
{
    public class AirBrake : NotificationObject, IResetable
    {
        private AirBrakePressure m_Pressure;

        public AirBrakePressure Pressure
        {
            set
            {
                if (value == m_Pressure)
                {
                    return;
                }

                m_Pressure = value;
                RaisePropertyChanged(() => Pressure);
            }
            get { return m_Pressure; }
        }

        public UseState UseState { set; get; }

        public AirBrakeLocation Location { set; get; }

        public MakeupAirState MakeupAirState { set; get; }

        public void ResetDatas()
        {
            Pressure = AirBrakePressure.KP600;
            UseState = UseState.Using;
            Location = AirBrakeLocation.InGoods;
            MakeupAirState = MakeupAirState.Makeup;
        }
    }
}