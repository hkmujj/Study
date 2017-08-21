using Microsoft.Practices.Prism.Events;

namespace LightRail.HMI.SZLHLF.Event
{
    public class ViewChangedNotifiEvent : CompositePresentationEvent<ViewChangedNotifiEvent.NotifiArgs>
    {
        public class NotifiArgs
        {
            public string FullName { get; set; }
        }
    }
}
