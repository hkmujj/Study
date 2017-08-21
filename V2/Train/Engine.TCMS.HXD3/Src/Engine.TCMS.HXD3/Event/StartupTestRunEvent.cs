using System.Diagnostics;
using Microsoft.Practices.Prism.Events;

namespace Engine.TCMS.HXD3.Event
{
    public class StartupTestRunEvent : CompositePresentationEvent<StartupTestRunEvent.EventArgs>
    {
        public class EventArgs
        {
            [DebuggerStepThrough]
            public EventArgs(bool isSet )
            {
                IsSet = isSet;
            }

            public bool IsSet { private set; get; }

        }
    }
}