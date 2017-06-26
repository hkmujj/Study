using Microsoft.Practices.Prism.Events;

namespace Engine.TCMS.Turkmenistan.Event
{
    public class ReconnectionChangedEvent:CompositePresentationEvent<ReconnectionChangedEvent.Args>
    {
        public class Args
        {
            public Args(bool isReconnection)
            {
                IsReconnection = isReconnection;
            }

            public bool IsReconnection { get; private set; }
        }
    }
}
