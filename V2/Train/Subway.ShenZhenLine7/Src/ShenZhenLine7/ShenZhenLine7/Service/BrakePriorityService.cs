using System.Collections.Generic;
using Subway.ShenZhenLine7.Config;
using Subway.ShenZhenLine7.Models.Units;

namespace Subway.ShenZhenLine7.Service
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