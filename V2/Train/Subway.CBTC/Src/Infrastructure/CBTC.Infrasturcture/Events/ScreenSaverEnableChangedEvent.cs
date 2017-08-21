using System.Diagnostics;
using Microsoft.Practices.Prism.Events;

namespace CBTC.Infrasturcture.Events
{
    public class ScreenSaverEnableChangedEvent : CompositePresentationEvent<ScreenSaverEnableChangedEvent.Args>
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