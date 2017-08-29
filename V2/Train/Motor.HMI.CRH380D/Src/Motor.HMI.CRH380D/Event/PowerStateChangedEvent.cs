using System.Diagnostics;
using Microsoft.Practices.Prism.Events;

namespace Motor.HMI.CRH380D.Event
{
    public class PowerStateChangedEvent : CompositePresentationEvent<PowerStateChangedEvent.Args>
    {
        public class Args
        {
            [DebuggerStepThrough]
            public Args(bool currentState)
            {
                CurrentState = currentState;
            }

            public bool CurrentState { get; private set; }
        }
    }
}