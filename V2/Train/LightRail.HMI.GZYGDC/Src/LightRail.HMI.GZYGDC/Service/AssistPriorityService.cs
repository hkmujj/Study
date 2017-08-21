using System.Collections.Generic;
using LightRail.HMI.GZYGDC.Config;
using LightRail.HMI.GZYGDC.Model.Units;

namespace LightRail.HMI.GZYGDC.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class AssistPriorityService : PriorityServiceBase<AssistState>
    {
        /// <summary>
        ///  构造函数
        /// </summary>
        /// <param name="lis">辅助的优先级列表</param>
        public AssistPriorityService(IList<Priority<AssistState, int>> lis)
        {
            Priority = lis;
        }
    }
}