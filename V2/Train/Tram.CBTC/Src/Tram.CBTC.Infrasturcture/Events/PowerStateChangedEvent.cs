using System.Diagnostics;
using Microsoft.Practices.Prism.Events;
using Tram.CBTC.Infrasturcture.Model.Constant;

namespace Tram.CBTC.Infrasturcture.Events
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