using System;
using MMI.Communacation.Interface.AppLayer;
using MMI.Facility.Interface;

namespace MMI.Communacation.Control.PresentationLayer.BroadcastStragy
{
    /// <summary>
    /// 实时数据广播策略
    /// </summary>
    internal abstract class RecvRealTimeDataBroadcastStragy : IIPresentationLayerDataBroadcastProvider, IDisposable
    {
        protected IIPresentationLayerDataBroadcastProvider BroadcastProvider { private set; get; }

        protected RecvRealTimeDataBroadcastStragy(IIPresentationLayerDataBroadcastProvider broadcastProvider)
        {
            BroadcastProvider = broadcastProvider;
        }

        public abstract void OnDataReceived(ProjectType type, NetDatas data);

        public virtual void OnCourseStarted()
        {
            
        }

        public virtual void OnCourseStopped()
        {
            
        }

        public virtual void Dispose()
        {
            
        }
    }
}