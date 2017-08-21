using System.Collections.Generic;
using LightRail.HMI.GZYGDC.Config;
using LightRail.HMI.GZYGDC.Model.Units;

namespace LightRail.HMI.GZYGDC.Service
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