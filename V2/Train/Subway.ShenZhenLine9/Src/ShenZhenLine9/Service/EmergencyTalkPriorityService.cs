using System.Collections.Generic;
using Subway.ShenZhenLine9.Config;
using Subway.ShenZhenLine9.Models.Units;

namespace Subway.ShenZhenLine9.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class EmergencyTalkPriorityService : PriorityServiceBase<EmergencyTalkState>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        public EmergencyTalkPriorityService(IList<Priority<EmergencyTalkState, int>> list)
        {
            Priority = list;
        }
    }
}