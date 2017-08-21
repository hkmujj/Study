using System.Collections.Generic;
using Subway.ShenZhenLine7.Config;
using Subway.ShenZhenLine7.Models.Units;

namespace Subway.ShenZhenLine7.Service
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