using System.Collections.Generic;
using LightRail.HMI.GZYGDC.Config;
using LightRail.HMI.GZYGDC.Model.Units;
using LightRail.HMI.GZYGDC.Service;


namespace LightRail.HMI.GZYGDC.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class AirConditionPriorityService : PriorityServiceBase<AirConditionState>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        public AirConditionPriorityService(IList<Priority<AirConditionState, int>> list)
        {
            Priority = list;
        }
    }
}