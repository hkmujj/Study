using System.Diagnostics;

namespace Motor.ATP.Infrasturcture.Interface
{
    [DebuggerDisplay("DriverId={DriverId}, TrainId={TrainId}")]
    public class DriverDataModel
    {
        [DebuggerStepThrough]
        public DriverDataModel(string driverId, string trainId)
        {
            DriverId = driverId;
            TrainId = trainId;
        }

        public string DriverId { get; private set; }

        public string TrainId { get; private set; }
    }
}