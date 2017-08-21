using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Model.Domain.Constant;
using Microsoft.Practices.Prism.Events;

namespace Engine.TPX21F.HXN5B.Event
{
    public class EmergerBrakeCountDownEvent : CompositePresentationEvent<EmergerBrakeCountDownEvent.Args>
    {
        public class Args
        {
            [ImportingConstructor]
            public Args(CountDownState state)
            {
                State = state;
            }

            public CountDownState State { get; private set; }
        }
    }
}