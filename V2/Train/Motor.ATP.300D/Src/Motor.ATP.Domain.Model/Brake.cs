using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model
{
    public class Brake :IBrake
    {
        public ITrainInfo Parent { get; set; }
    }
}
