using Urban.Domain.TrainState.Interface;

namespace Urban.Domain.TrainState.Model
{
    public class ATP : UpdatingProvider<ATP>, IATP
    {
        public object ATPState { get; set; }
        public object WorkModel { get; set; }
    }
}