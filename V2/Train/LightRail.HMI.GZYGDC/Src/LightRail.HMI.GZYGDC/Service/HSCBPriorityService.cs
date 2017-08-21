using System.Collections.Generic;
using LightRail.HMI.GZYGDC.Config;
using LightRail.HMI.GZYGDC.Model.Units;

namespace LightRail.HMI.GZYGDC.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class HSCBPriorityService : PriorityServiceBase<HSCBState>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        public HSCBPriorityService(IList<Priority<HSCBState, int>> list)
        {
            Priority = list;
        }
    }
}