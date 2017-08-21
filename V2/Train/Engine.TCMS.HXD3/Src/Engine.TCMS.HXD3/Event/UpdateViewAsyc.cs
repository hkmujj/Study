using System.Diagnostics;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;

namespace Engine.TCMS.HXD3.Event
{
    public class UpdateViewAsyc : CompositePresentationEvent<UpdateViewAsyc.EventArgs>
    {
        public class EventArgs
        {
            [DebuggerStepThrough]
            public EventArgs(CommunicationDataChangedArgs dataChangedArgs)
            {
                DataChangedArgs = dataChangedArgs;
            }

            public CommunicationDataChangedArgs DataChangedArgs { private set; get; }
        }

    }
}