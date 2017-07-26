using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using Motor.ATP.Infrasturcture.Interface.Service;

namespace Motor.ATP.Infrasturcture.Model.Service
{
    public class EventAggregatorProvider : IEventAggregatorProvider
    {
        public static EventAggregatorProvider Instance { private set; get; }

        public IEventAggregator EventAggregator { get; private set; }

        static EventAggregatorProvider()
        {
            Instance = new EventAggregatorProvider();
        }

        private EventAggregatorProvider()
        {
            EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
        }
    }
}