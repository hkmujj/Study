using System;
using System.Collections.Generic;

namespace Subway.ShenZhenLine7.Extention
{
    /// <summary>
    /// 
    /// </summary>
    public static class DictionaryExtention
    {
        /// <summary>
        /// 循环
        /// </summary>
        /// <param name="value"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        public static void ForEach<T, K>(this IDictionary<T, K> value, Action<T, K> action)
        {
            foreach (var k in value)
            {
                action?.Invoke(k.Key, k.Value);
            }
        }
    }
}