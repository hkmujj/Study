using System.Collections.Generic;
using Motor.HMI.CRH380D.Models.ConfigModel;
using Motor.HMI.CRH380D.Models.Units;

namespace Motor.HMI.CRH380D.Service
{
    /// <summary>
    /// 门优先级服务
    /// </summary>
    public class DCDevice2PriorityService : PriorityServiceBase<DCDevice2State>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dooPriorities"></param>
        public DCDevice2PriorityService(IList<Priority<DCDevice2State, int>> dooPriorities)
        {
            Priority = dooPriorities;
        }
    }
}