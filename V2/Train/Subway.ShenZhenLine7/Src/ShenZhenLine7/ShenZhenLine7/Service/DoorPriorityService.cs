using System.Collections.Generic;
using Subway.ShenZhenLine7.Config;
using Subway.ShenZhenLine7.Models.Units;

namespace Subway.ShenZhenLine7.Service
{
    /// <summary>
    /// 门优先级服务
    /// </summary>
    public class DoorPriorityService : PriorityServiceBase<DoorState>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dooPriorities"></param>
        public DoorPriorityService(IList<Priority<DoorState, int>> dooPriorities)
        {
            Priority = dooPriorities;
        }
    }
}