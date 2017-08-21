using MMI.Communacation.Interface.AppLayer;
using MMI.Facility.Interface;

namespace MMI.Communacation.Control.PresentationLayer
{
    internal interface IIPresentationLayerDataBroadcastProvider
    {
        void OnDataReceived(ProjectType type, NetDatas data);
    }
}