using Engine.Angola.TCMS.Model.BtnStragy;
using Microsoft.Practices.Prism.Events;
using System.ComponentModel.Composition;
using System.Diagnostics;

namespace Engine.Angola.TCMS.Events
{
    public class AsyncNavigateToByStateInterfaceKeyEvent : CompositePresentationEvent<AsyncNavigateToByStateInterfaceKeyEvent.Args>
    {
        [DebuggerStepThrough]
        public class Args
        {
            [ImportingConstructor]
            public Args(StateInterfaceKey interfaceKey)
            {
                InterfaceKey = interfaceKey;
            }

            public StateInterfaceKey InterfaceKey { get; private set; }
        }
    }
}
