using System.Collections.Generic;
using LightRail.HMI.SZLHLF.Config;
using LightRail.HMI.SZLHLF.Model.Units;

namespace LightRail.HMI.SZLHLF.Service
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