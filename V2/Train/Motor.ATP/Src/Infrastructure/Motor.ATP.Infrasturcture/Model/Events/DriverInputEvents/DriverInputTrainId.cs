using System.Diagnostics;

namespace Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents
{
    public class DriverInputTrainId
    {
        [DebuggerStepThrough]
        public DriverInputTrainId(string trainId)
        {
            TrainId = trainId;
        }

        public string TrainId { get; private set; }
    }
}