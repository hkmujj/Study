using System.Collections.Generic;
using LightRail.HMI.GZYGDC.Config;
using LightRail.HMI.GZYGDC.Model.Units;

namespace LightRail.HMI.GZYGDC.Service
{
    /// <summary>
    /// 门优先级服务
    /// </summary>
    public class DoorPriorityService : PriorityServiceBase<DoorState>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dooPriorities"></param>
        public DoorPriorityService(IList<Priority<DoorState, int>> dooPriorities)
        {
            Priority = dooPriorities;
        }
    }
}