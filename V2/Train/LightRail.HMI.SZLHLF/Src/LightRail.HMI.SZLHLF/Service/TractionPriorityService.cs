using System.Collections.Generic;
using LightRail.HMI.SZLHLF.Config;
using LightRail.HMI.SZLHLF.Model.Units;

namespace LightRail.HMI.SZLHLF.Service
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