using System.Collections.Generic;
using Motor.HMI.CRH380D.Models.ConfigModel;
using Motor.HMI.CRH380D.Models.Units;

namespace Motor.HMI.CRH380D.Service
{
    /// <summary>
    /// 门优先级服务
    /// </summary>
    public class DCDevice1PriorityService : PriorityServiceBase<DCDevice1State>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dooPriorities"></param>
        public DCDevice1PriorityService(IList<Priority<DCDevice1State, int>> dooPriorities)
        {
            Priority = dooPriorities;
        }
    }
}