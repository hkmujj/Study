using System.Collections.Generic;
using LightRail.HMI.GZYGDC.Config;
using LightRail.HMI.GZYGDC.Model.Units;

namespace LightRail.HMI.GZYGDC.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class TractionPriorityService : PriorityServiceBase<TractionState>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        public TractionPriorityService(IList<Priority<TractionState, int>> list)
        {
            Priority = list;
        }
    }
}