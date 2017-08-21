using System.Diagnostics;
using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model.Events.DriverInputEvents
{
    public class DriverInputCTCS
    {
        [DebuggerStepThrough]
        public DriverInputCTCS(CTCSType inputtedCTCSType)
        {
            InputtedCTCSType = inputtedCTCSType;
        }

        public CTCSType InputtedCTCSType { private set; get; }
    }
}