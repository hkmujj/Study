using System.Diagnostics;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;

namespace Engine.Angola.TCMS.Events
{
    public class AsyncAdapteDataEvent : CompositePresentationEvent<AsyncAdapteDataEvent.Args>
    {
        public class Args
        {
            [DebuggerStepThrough]
            public Args(CommunicationDataChangedArgs dataChangedArgs)
            {
                DataChangedArgs = dataChangedArgs;
            }

            public CommunicationDataChangedArgs DataChangedArgs { get; private set; }
        }
    }
}