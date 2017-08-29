using System.Collections.Generic;
using LightRail.HMI.GZYGDC.Config;
using LightRail.HMI.GZYGDC.Model.Units;
using LightRail.HMI.GZYGDC.Service;

namespace LightRail.HMI.GZYGDC.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class NetTopologyPriorityService : PriorityServiceBase<NetTopologyState>
    {
        /// <summary>
        /// 
        /// </summary>ss
        /// <param name="list"></param>
        public NetTopologyPriorityService(IList<Priority<NetTopologyState, int>> list)
        {
            Priority = list;
        }
    }
}