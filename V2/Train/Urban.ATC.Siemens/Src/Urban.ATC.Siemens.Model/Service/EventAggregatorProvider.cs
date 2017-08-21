using Microsoft.Practices.Prism.Events;
using Motor.ATP.Domain.Interface.Service;

namespace Motor.ATP.Domain.Model.Service
{
    public class EventAggregatorProvider : IEventAggregatorProvider
    {
        public static EventAggregatorProvider Instance { private set; get; }

        public IEventAggregator EventAggregator { get; private set; }

        static EventAggregatorProvider() { Instance = new EventAggregatorProvider(); }

        private EventAggregatorProvider()
        {
            EventAggregator = new EventAggregator();
        }
    }
}