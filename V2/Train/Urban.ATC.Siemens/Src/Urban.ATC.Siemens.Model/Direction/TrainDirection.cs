using System.Diagnostics;
using RailwaySinulation.Domain.Interface.Motor.ATP;

namespace RailwaySimulation.Motor.ATP.Domain.Model.Direction
{
    public class TrainDirection : ITrainDirection
    {
        [DebuggerStepThrough]
        public TrainDirection(TrainDirectionType direction = TrainDirectionType.Unknown)
        {
            Direction = direction;
        }

        public TrainDirectionType Direction { get; set; }
    }
}