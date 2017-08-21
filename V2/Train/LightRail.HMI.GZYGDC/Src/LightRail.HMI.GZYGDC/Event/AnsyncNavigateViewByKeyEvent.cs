using System.ComponentModel.Composition;
using System.Diagnostics;
using LightRail.HMI.GZYGDC.Model.BtnStragy;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;

namespace LightRail.HMI.GZYGDC.Event
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