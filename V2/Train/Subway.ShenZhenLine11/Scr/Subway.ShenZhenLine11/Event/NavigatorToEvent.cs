using Microsoft.Practices.Prism.Events;

namespace Subway.ShenZhenLine11.Event
{
    public class NavigatorToEvent : CompositePresentationEvent<NavigatorToEvent.NavigatorArgs>
    {
        public class NavigatorArgs
        {
            public string Names { get; set; }
        }
    }
}