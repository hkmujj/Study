using System.Diagnostics;
using Microsoft.Practices.Prism.Events;

namespace Engine.TPX21F.HXN5B.Event
{
    public class InputCharEvent : CompositePresentationEvent<InputCharEvent.Args>
    {
        public class Args
        {
            [DebuggerStepThrough]
            public Args(char c)
            {
                C = c;
            }

            public char C { get; private set; }
        }
    }
}