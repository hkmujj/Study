using System.Collections.Generic;
using Subway.ShenZhenLine9.Config;
using Subway.ShenZhenLine9.Models.Units;

namespace Subway.ShenZhenLine9.Service
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