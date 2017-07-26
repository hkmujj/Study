using System.Diagnostics;

namespace Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents
{
    public class DriverInputDriverId
    {
        [DebuggerStepThrough]
        public DriverInputDriverId(string driverId)
        {
            DriverId = driverId;
        }

        public string DriverId { get; private set; }
    }
}