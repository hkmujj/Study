using Urban.Domain.TrainState.Interface;

namespace Urban.Domain.TrainState.Model
{
    public class SpeedModel : UpdatingProvider<SpeedModel> ,ISpeedModel
    {
        public float Value { get; set; }
    }
}