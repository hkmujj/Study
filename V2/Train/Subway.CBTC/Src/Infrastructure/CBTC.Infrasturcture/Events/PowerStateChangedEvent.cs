using System.Diagnostics;
using CBTC.Infrasturcture.Model.Constant;
using Microsoft.Practices.Prism.Events;

namespace CBTC.Infrasturcture.Events
{
    public class PowerStateChangedEvent : CompositePresentationEvent<PowerStateChangedEvent.Args>
    {
        public class Args
        {
            [DebuggerStepThrough]
            public Args(PowerState currentState)
            {
                CurrentState = currentState;
            }

            public PowerState CurrentState { get; private set; }
        }
    }
}