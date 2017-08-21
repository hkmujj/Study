using System.Collections.Generic;
using LightRail.HMI.SZLHLF.Config;
using LightRail.HMI.SZLHLF.Model.Units;


namespace LightRail.HMI.SZLHLF.Service
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