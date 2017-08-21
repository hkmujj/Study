using System.Collections.Generic;
using Subway.ShenZhenLine7.Config;
using Subway.ShenZhenLine7.Models.Units;

namespace Subway.ShenZhenLine7.Service
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