using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model
{
    public class Driver :  IDriver
    {
        public ITrainInfo Parent { get; set; }

        public string DriverId { get; set; }

        public string TrainLine { get; set; }
    }
}
