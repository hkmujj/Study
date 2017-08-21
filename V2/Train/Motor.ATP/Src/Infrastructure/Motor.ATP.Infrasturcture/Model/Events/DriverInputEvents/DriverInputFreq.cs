using System.Diagnostics;
using Motor.ATP.Infrasturcture.Interface;

namespace Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents
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