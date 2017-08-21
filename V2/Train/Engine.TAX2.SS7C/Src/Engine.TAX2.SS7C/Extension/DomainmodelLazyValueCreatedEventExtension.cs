using System.Threading;
using Engine.TAX2.SS7C.Events;

namespace Engine.TAX2.SS7C.Extension
{
    public static class DomainmodelLazyValueCreatedEventExtension
    {
        public static void PublishLater(this DomainmodelLazyValueCreatedEvent et,
            DomainmodelLazyValueCreatedEvent.Args args = null, int millisecondsTimeout = 100)
        {
            ThreadPool.QueueUserWorkItem(
                o =>
                {
                    Thread.Sleep(millisecondsTimeout);
                    et.Publish(args);
                });
        }
    }
}