using System.Diagnostics;

namespace Motor.ATP.Domain.Model.Events.DriverInputEvents
{
    public class DriverInputTrainData
    {
        [DebuggerStepThrough]
        public DriverInputTrainData(string inputtedTrainData)
        {
            InputtedTrainData = inputtedTrainData;
        }
        public string InputtedTrainData { private set; get; }
    }
}