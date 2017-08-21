using System.Diagnostics;
using Motor.ATP.Infrasturcture.Interface;

namespace Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents
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