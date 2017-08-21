using System.Diagnostics;

namespace CBTC.Infrasturcture.Events.DriverInputEvents
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