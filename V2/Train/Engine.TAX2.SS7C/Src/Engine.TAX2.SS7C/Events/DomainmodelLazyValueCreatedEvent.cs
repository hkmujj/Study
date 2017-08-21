using System.Diagnostics;
using Microsoft.Practices.Prism.Events;

namespace Engine.TAX2.SS7C.Events
{
    public class DomainmodelLazyValueCreatedEvent : CompositePresentationEvent<DomainmodelLazyValueCreatedEvent.Args>
    {
        public class Args
        {
            [DebuggerStepThrough]
            public Args()
            {
            }
        }
    }
}