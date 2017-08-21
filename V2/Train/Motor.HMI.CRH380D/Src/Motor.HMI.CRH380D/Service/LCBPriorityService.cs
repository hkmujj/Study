using System.Collections.Generic;
using Motor.HMI.CRH380D.Models.ConfigModel;
using Motor.HMI.CRH380D.Models.Units;

namespace Motor.HMI.CRH380D.Service
{
    /// <summary>
    /// 门优先级服务
    /// </summary>
    public class LCBPriorityService : PriorityServiceBase<LCBState>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dooPriorities"></param>
        public LCBPriorityService(IList<Priority<LCBState, int>> dooPriorities)
        {
            Priority = dooPriorities;
        }
    }
}