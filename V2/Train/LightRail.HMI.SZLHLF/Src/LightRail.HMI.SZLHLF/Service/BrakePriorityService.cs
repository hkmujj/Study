using System.Collections.Generic;
using LightRail.HMI.SZLHLF.Config;
using LightRail.HMI.SZLHLF.Model.Units;

namespace LightRail.HMI.SZLHLF.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class BrakePriorityService : PriorityServiceBase<BrakeState>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        public BrakePriorityService(IList<Priority<BrakeState, int>> list)
        {
            Priority = list;
        }
    }
}