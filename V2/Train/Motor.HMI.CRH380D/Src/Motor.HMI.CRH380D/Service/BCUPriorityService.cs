using System.Collections.Generic;
using Motor.HMI.CRH380D.Models.ConfigModel;
using Motor.HMI.CRH380D.Models.Units;

namespace Motor.HMI.CRH380D.Service
{
    /// <summary>
    /// 门优先级服务
    /// </summary>
    public class BCUPriorityService : PriorityServiceBase<BCUState>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dooPriorities"></param>
        public BCUPriorityService(IList<Priority<BCUState, int>> dooPriorities)
        {
            Priority = dooPriorities;
        }
    }
}