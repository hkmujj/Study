using System.Collections.Generic;
using LightRail.HMI.SZLHLF.Config;
using LightRail.HMI.SZLHLF.Model.Units;

namespace LightRail.HMI.SZLHLF.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class BatteryPriorityService : PriorityServiceBase<BatteryState>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        public BatteryPriorityService(IList<Priority<BatteryState, int>> list)
        {
            Priority = list;
        }
    }
}