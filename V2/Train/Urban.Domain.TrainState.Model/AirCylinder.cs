using Urban.Domain.TrainState.Interface;

namespace Urban.Domain.TrainState.Model
{
    public class AirCylinder : UpdatingProvider<AirCylinder>, IAirCylinder
    {
        public float TotalPressure { get; set; }
        public float BrakePressure { get; set; }
    }
}