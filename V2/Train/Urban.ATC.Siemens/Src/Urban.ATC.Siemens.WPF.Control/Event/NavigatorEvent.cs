using Microsoft.Practices.Prism.Events;

namespace Urban.ATC.Siemens.WPF.Control.Event
{
    public class NavigatorEvent : CompositePresentationEvent<NavigatorEventArgs>
    {

    }

    public class NavigatorEventArgs
    {
        public string Region { get; set; }
        public string Name { get; set; }
    }
}