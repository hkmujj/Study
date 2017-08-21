using System.Collections.Generic;
using Subway.ShenZhenLine9.Config;
using Subway.ShenZhenLine9.Models.Units;

namespace Subway.ShenZhenLine9.Service
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