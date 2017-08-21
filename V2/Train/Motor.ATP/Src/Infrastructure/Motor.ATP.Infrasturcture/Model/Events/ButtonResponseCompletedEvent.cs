using System.Diagnostics;
using Microsoft.Practices.Prism.Events;
using Motor.ATP.Infrasturcture.Interface;

namespace Motor.ATP.Infrasturcture.Model.Events
{
    public class ButtonResponseCompletedEvent: CompositePresentationEvent<ButtonResponseCompletedEvent.Args>
    {
        public class Args
        {
            [DebuggerStepThrough]
            public Args(ATPType atpType)
            {
                ATPType = atpType;
            }

            public ATPType ATPType { get; private set; }
        }
    }
}