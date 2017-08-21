using System.ComponentModel.Composition;
using System.Diagnostics;
using Engine.TAX2.SS7C.Model.BtnStragy;
using Microsoft.Practices.Prism.Events;

namespace Engine.TAX2.SS7C.Events
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