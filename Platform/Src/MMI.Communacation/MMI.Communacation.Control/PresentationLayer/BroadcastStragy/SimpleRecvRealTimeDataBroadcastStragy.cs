using MMI.Communacation.Interface.AppLayer;
using MMI.Facility.Interface;

namespace MMI.Communacation.Control.PresentationLayer.BroadcastStragy
{
    internal class SimpleRecvRealTimeDataBroadcastStragy : RecvRealTimeDataBroadcastStragy
    {
        public SimpleRecvRealTimeDataBroadcastStragy(IIPresentationLayerDataBroadcastProvider broadcastProvider)
            : base(broadcastProvider)
        {
        }

        public override void OnDataReceived(ProjectType type, NetDatas data)
        {
            BroadcastProvider.OnDataReceived(type, data);
        }
    }
}