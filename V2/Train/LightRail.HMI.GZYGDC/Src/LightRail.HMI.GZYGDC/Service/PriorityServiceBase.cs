using System.Collections.Generic;
using System.Linq;
using MMI.Facility.Interface.Service;
using LightRail.HMI.GZYGDC.Config;

namespace LightRail.HMI.GZYGDC.Service
{
    /// <summary>
    /// 优先级服务基类
    /// </summary>
    public abstract class PriorityServiceBase<TKey> : IService where TKey : struct
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        protected PriorityServiceBase()
        {
            Values = new Dictionary<object, List<Priority<TKey, int>>>();
        }
        /// <summary>
        /// 优先级列表
        /// </summary>
        protected IList<Priority<TKey, int>> Priority { get; set; }
        /// <summary>
        /// 缓存的值列表
        /// </summary>
        protected Dictionary<object, List<Priority<TKey, int>>> Values { get; }

        /// <summary>
        /// 获取类型 
        /// </summary>
        /// <param name="target">目标类</param>
        /// <param name="value"></param>
        /// <param name="falg"></param>
        /// <returns></returns>
        public TKey GetPriorityValue(TKey value, bool falg, object target)
        {
            List<Priority<TKey, int>> values;
            if (Values.ContainsKey(target))
            {
                values = Values[target];
            }
            else
            {
                values = new List<Priority<TKey, int>>();
                Values.Add(target, values);
            }

            if (falg)
            {
                var tmp = Priority.FirstOrDefault(f => f.Key.Equals(value));
                if (tmp != null && !values.Contains(tmp))
                {
                    values.Add(tmp);
                }
            }
            else
            {
                values.Remove(Priority.FirstOrDefault(f => f.Key.Equals(value)));
            }
            var tmpValue = values.OrderBy(o => o.Prioritys).FirstOrDefault();

            return tmpValue?.Key ?? GetLowPriority();

        }

        /// <summary>
        /// 获取最高优先级
        /// </summary>
        /// <returns></returns>
        public TKey GetHightPriority()
        {
            var first = Priority.OrderBy(o => o.Prioritys).FirstOrDefault();
            return first?.Key ?? default(TKey);
        }
        /// <summary>
        /// 获取最低优先级
        /// </summary>
        /// <returns></returns>
        public TKey GetLowPriority()
        {
            var first = Priority.OrderByDescending(o => o.Prioritys).FirstOrDefault();
            return first?.Key ?? default(TKey);
        }
    }
}