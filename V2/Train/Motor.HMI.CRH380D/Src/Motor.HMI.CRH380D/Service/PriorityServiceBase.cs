using System.Collections.Generic;
using System.Linq;
using MMI.Facility.Interface.Service;
using Motor.HMI.CRH380D.Models.ConfigModel;

namespace Motor.HMI.CRH380D.Service
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
            Values = new Dictionary<object, IList<Priority<TKey, int>>>();
            //  Values = new List<Priority<TKey, int>>();
        }
        /// <summary>
        /// 优先级列表
        /// </summary>
        protected IList<Priority<TKey, int>> Priority { get; set; }
        /// <summary>
        /// 缓存的值列表
        /// </summary>
        //protected IList<Priority<TKey, int>> Values { get; }
        protected IDictionary<object, IList<Priority<TKey, int>>> Values { get; set; }

        /// <summary>
        /// 获取类型 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="falg">值</param>
        /// <param name="targetCalss">调用的类</param>
        /// <returns></returns>
        public TKey GetPriorityValue(TKey value, bool falg, object targetCalss)
        {
            object key = targetCalss.ToString();
            {
                var s1 = targetCalss.GetType();
                key = (from propertyInfo in s1.GetProperties() where propertyInfo.Name == "Location" || propertyInfo.Name == "Car" select propertyInfo.GetValue(targetCalss, null)).Aggregate(key, (current, tmp00) => current.ToString() + "+" + tmp00);
            }

            var tmp = Priority.FirstOrDefault(f => f.Key.Equals(value));
            if (tmp == null)
            {
                return value;
            }
            if (falg)
            {
                if (Values.ContainsKey(key))
                {
                    var tmpValue = Values[key].FirstOrDefault(f => f.Key.Equals(value));
                    if (tmpValue == null)
                    {
                        Values[key].Add(tmp.Clone());
                    }
                }
                else
                {
                    Values.Add(key, new List<Priority<TKey, int>>() { tmp.Clone() });
                }
            }
            else
            {
                if (Values.ContainsKey(key))
                {
                    var tmpValue = Values[key].FirstOrDefault(f => f.Key.Equals(value));
                    if (tmpValue != null)
                    {
                        Values[key].Remove(tmpValue);
                    }
                }
            }
            if (Values.ContainsKey(key))
            {
                var retue = Values[key].OrderBy(o => o.Prioritys).FirstOrDefault();
                if (retue != null)
                {
                    return retue.Key;
                }
            }

            return default(TKey);

        }

        /// <summary>
        /// 获取最高优先级
        /// </summary>
        /// <returns></returns>
        public TKey GetHightPriority()
        {
            var first = Priority.OrderBy(o => o.Prioritys).FirstOrDefault();
            return first != null ? first.Key : default(TKey);
        }
        /// <summary>
        /// 获取最低优先级
        /// </summary>
        /// <returns></returns>
        public TKey GetLowPriority()
        {
            var first = Priority.OrderByDescending(o => o.Prioritys).FirstOrDefault();
            return first != null ? first.Key : default(TKey);
        }
    }
}