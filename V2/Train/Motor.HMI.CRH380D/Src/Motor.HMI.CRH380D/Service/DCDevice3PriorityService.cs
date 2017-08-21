using System.Collections.Generic;
using Motor.HMI.CRH380D.Models.ConfigModel;
using Motor.HMI.CRH380D.Models.Units;

namespace Motor.HMI.CRH380D.Service
{
    /// <summary>
    /// 门优先级服务
    /// </summary>
    public class DCDevice3PriorityService : PriorityServiceBase<DCDevice3State>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dooPriorities"></param>
        public DCDevice3PriorityService(IList<Priority<DCDevice3State, int>> dooPriorities)
        {
            Priority = dooPriorities;
        }
    }
}