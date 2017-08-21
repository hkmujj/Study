using System.Collections.Generic;
using Motor.HMI.CRH380D.Models.ConfigModel;
using Motor.HMI.CRH380D.Models.Units;

namespace Motor.HMI.CRH380D.Service
{
    /// <summary>
    /// 门优先级服务
    /// </summary>
    public class StationPriorityService : PriorityServiceBase<StationState>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dooPriorities"></param>
        public StationPriorityService(IList<Priority<StationState, int>> dooPriorities)
        {
            Priority = dooPriorities;
        }
    }
}