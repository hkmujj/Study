using System.ComponentModel.Composition;
using System.Diagnostics;
using Microsoft.Practices.Prism.Events;

namespace Motor.HMI.CRH380D.Event
{
    public class AnsyncNavigateViewByKeyEvent : CompositePresentationEvent<AnsyncNavigateViewByKeyEvent.Args>
    {
        [DebuggerStepThrough]
        public class Args
        {
            [ImportingConstructor]
            public Args(string stateKey)
            {
                StateKey = stateKey;
            }

            public string StateKey { private set; get; }
        }
    }
}