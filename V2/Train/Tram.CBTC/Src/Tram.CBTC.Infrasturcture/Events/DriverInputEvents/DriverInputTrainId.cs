using System.Diagnostics;

namespace Tram.CBTC.Infrasturcture.Events.DriverInputEvents
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