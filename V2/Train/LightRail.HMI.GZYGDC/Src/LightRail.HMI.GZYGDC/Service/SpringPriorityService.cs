using System.Collections.Generic;
using LightRail.HMI.GZYGDC.Config;
using LightRail.HMI.GZYGDC.Model.Units;

namespace LightRail.HMI.GZYGDC.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class SpringPriorityService : PriorityServiceBase<SpringState>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        public SpringPriorityService(IList<Priority<SpringState, int>> list)
        {
            Priority = list;
        }
    }
}