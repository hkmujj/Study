using Microsoft.Practices.Prism.Events;

namespace Subway.ShenZhenLine11.Event
{
    public class SendDataEvent<T> : CompositePresentationEvent<SendDataEnvetArgs<T>>
    {

    }

    public class SendDataEnvetArgs<T>
    {
        public int Index { get; set; }
        public T Value { get; set; }
        public bool IsClear { get; set; }
    }
}