using System.Diagnostics;
using Microsoft.Practices.Prism.Events;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Infomation;

namespace Motor.ATP._200C.Subsys.Events
{
    public class EnsureMessageEvent : CompositePresentationEvent<EnsureMessageEvent.Args>
    {
        public class Args
        {
            [DebuggerStepThrough]
            public Args(ATPType atpType, InfomationResponseType responseType)
            {
                ATPType = atpType;
                ResponseType = responseType;
            }

            public ATPType  ATPType { get; private set; }

            public InfomationResponseType ResponseType { private set; get; }
        }
    }
}