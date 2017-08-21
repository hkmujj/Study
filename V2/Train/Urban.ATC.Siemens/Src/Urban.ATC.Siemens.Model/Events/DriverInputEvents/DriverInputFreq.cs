using System.Diagnostics;
using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model.Events.DriverInputEvents
{
    public class DriverInputFreq
    {
        [DebuggerStepThrough]
        public DriverInputFreq(TrainFreq inputtedTrainFreq)
        {
            InputtedTrainFreq = inputtedTrainFreq;
        }

        public TrainFreq InputtedTrainFreq { private set; get; }
    }
}