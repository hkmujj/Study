using System.Collections.Generic;
using Motor.HMI.CRH380D.Models.ConfigModel;
using Motor.HMI.CRH380D.Models.Units;

namespace Motor.HMI.CRH380D.Service
{
    /// <summary>
    /// 门优先级服务
    /// </summary>
    public class ACMPriorityService : PriorityServiceBase<ACMState>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dooPriorities"></param>
        public ACMPriorityService(IList<Priority<ACMState, int>> dooPriorities)
        {
            Priority = dooPriorities;
        }
    }
}