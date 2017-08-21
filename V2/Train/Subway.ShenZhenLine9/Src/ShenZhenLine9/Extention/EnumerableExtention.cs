using System;
using System.Collections.Generic;

namespace Subway.ShenZhenLine9.Extention
{
    /// <summary>
    /// 
    /// </summary>
    public static class EnumerableExtention
    {
        /// <summary>
        /// foreach语法糖
        /// </summary>
        /// <param name="values">需要操作的集合</param>
        /// <param name="action">需要做的操作</param>
        /// <typeparam name="T">需要操作的类型</typeparam>
        public static void ForEach<T>(this IEnumerable<T> values, Action<T> action)
        {
            if (action == null)
            {
                return;
            }
            foreach (var value in values)
            {
                action.Invoke(value);
            }
        }
    }
}