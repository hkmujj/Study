using Microsoft.Practices.Prism.Events;

namespace Engine.LCDM.HXD3.Events
{
    public class VieChangedNotifiEvent : CompositePresentationEvent<VieChangedNotifiEvent.NotifiArgs>
    {
        public class NotifiArgs
        {
            public string FullName { get; set; }
        }
    }
}