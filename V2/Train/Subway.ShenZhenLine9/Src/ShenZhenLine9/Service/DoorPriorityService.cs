using System.Collections.Generic;
using Subway.ShenZhenLine9.Config;
using Subway.ShenZhenLine9.Models.Units;

namespace Subway.ShenZhenLine9.Service
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