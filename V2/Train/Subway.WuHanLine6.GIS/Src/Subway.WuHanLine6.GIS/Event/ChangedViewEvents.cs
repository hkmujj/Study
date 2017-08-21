using Microsoft.Practices.Prism.Events;

namespace Subway.WuHanLine6.GIS.Event
{
    public class ChangedViewEvents : CompositePresentationEvent<ChangedViewEvents.Args>
    {
        public class Args
        {
            public Args(bool isMap)
            {
                IsMap = isMap;
            }

            public bool IsMap { get; private set; }
        }
    }
}