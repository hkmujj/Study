using System.Diagnostics;
using Engine.TAX2.SS7C.Model.Domain.TrainState.TAX;
using Microsoft.Practices.Prism.Events;

namespace Engine.TAX2.SS7C.Events
{
    public class CurrentTAXStateChangedEvent : CompositePresentationEvent<CurrentTAXStateChangedEvent.Args>
    {
        public class Args
        {
            [DebuggerStepThrough]
            public Args(TAXModelBase source)
            {
                Source = source;
            }

            public TAXModelBase Source { get; private set; }
        }
    }
}