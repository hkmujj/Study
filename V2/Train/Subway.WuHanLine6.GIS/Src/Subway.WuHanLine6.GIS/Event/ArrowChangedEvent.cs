using Microsoft.Practices.Prism.Events;

namespace Subway.WuHanLine6.GIS.Event
{
    public class ArrowChangedEvent:CompositePresentationEvent<ArrowChangedEvent.Args>
    {
        public class Args
        {
            public Args(bool isStart)
            {
                IsStart = isStart;
            }

            public bool IsStart { get; private set; }
        }
    }
}