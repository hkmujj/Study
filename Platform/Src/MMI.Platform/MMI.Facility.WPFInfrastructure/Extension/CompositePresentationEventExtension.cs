using System.Threading;
using Microsoft.Practices.Prism.Events;

namespace MMI.Facility.WPFInfrastructure.Extension
{
    /// <summary>
    /// CompositePresentationEvent 扩展
    /// </summary>
    public static class CompositePresentationEventExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ev"></param>
        /// <param name="args"></param>
        /// <param name="laterMillisecondsTimeout">延时时间 默认 100ms </param>
        /// <typeparam name="TArg"></typeparam>
        public static void PublishLater<TArg>(this CompositePresentationEvent<TArg> ev, TArg args,
            int laterMillisecondsTimeout = 100)
        {
            ThreadPool.QueueUserWorkItem(state =>
            {
                Thread.Sleep(laterMillisecondsTimeout);
                ev.Publish(args);
            });
        }
    }
}