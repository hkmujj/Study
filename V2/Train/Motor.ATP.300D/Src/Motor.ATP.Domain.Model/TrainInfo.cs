using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model
{
    public class TrainInfo :  ITrainInfo
    {
        public IATP Parent { get; set; }

        public IISpeed Speed { get; set; }

        public IBrake Brake { get; set; }

        public IDriver Driver { get; set; }

        public IKilometerPost KilometerPost { get; set; }
    }
}
