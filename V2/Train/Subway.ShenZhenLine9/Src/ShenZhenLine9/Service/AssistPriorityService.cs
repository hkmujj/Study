using System.Collections.Generic;
using Subway.ShenZhenLine9.Config;
using Subway.ShenZhenLine9.Models.Units;

namespace Subway.ShenZhenLine9.Service
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