using Microsoft.Practices.Prism.Events;

namespace Subway.ShenZhenLine11.Event
{
    public class DeactiveRegionEvent : CompositePresentationEvent<DeactiveRegionEvent.DeActiveEventArgs>
    {
        public class DeActiveEventArgs
        {
            public string Name { get; set; }
        }
    }
}