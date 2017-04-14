using System.Collections.Generic;
using MMI.Communacation.Interface.AppLayer;

namespace MMI.Communacation.Control.PresentationLayer.BroadcastStragy
{
    internal class NetDataRecvBoolStartIndexCompare : IComparer<NetDatas>
    {
        public int Compare(NetDatas x, NetDatas y)
        {
            return x.ReceivedBools.StartIndex.CompareTo(y.ReceivedBools.StartIndex);
        }
    }
}