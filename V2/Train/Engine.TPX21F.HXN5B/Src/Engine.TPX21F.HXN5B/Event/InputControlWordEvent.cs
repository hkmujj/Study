using System.Diagnostics;
using Microsoft.Practices.Prism.Events;

namespace Engine.TPX21F.HXN5B.Event
{
    public class InputControlWordEvent : CompositePresentationEvent<InputControlWordEvent.Args>
    {
        public class Args
        {
            [DebuggerStepThrough]
            public Args(OkOrCancel word)
            {
                Word = word;
            }

            public OkOrCancel Word { get; private set; }
        }


        public enum OkOrCancel
        {
            Ok,

            Cancel,
        }
    }
}