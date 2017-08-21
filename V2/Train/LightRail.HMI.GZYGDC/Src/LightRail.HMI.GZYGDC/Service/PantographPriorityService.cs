using System.Collections.Generic;
using LightRail.HMI.GZYGDC.Config;
using LightRail.HMI.GZYGDC.Model.Units;

namespace LightRail.HMI.GZYGDC.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class PantographPriorityService : PriorityServiceBase<PantographState>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        public PantographPriorityService(IList<Priority<PantographState, int>> list)
        {
            Priority = list;
        }
    }
}