using System.Collections.Concurrent;
using System.Collections.Generic;
using MMITool.Common.Model;

namespace MMITool.Common.Util.Extension
{
    /// <summary>
    /// 
    /// </summary>
    public static class DictionaryExtention
    {
        /// <summary>
        /// 转换成 只读 dictionary
        /// </summary>
        /// <param name="sourceDic"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static IReadOnlyDictionary<TKey, TValue> AsReadOnlyDictionary<TKey, TValue>(this IDictionary<TKey, TValue> sourceDic)
        {
            return new ReadOnlyDictionary<TKey, TValue>(sourceDic);
        }

        /// <summary>
        /// 转换成 线程同步的
        /// </summary>
        /// <param name="sourceDic"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static ConcurrentDictionary<TKey, TValue> AsConcurrentDictionary<TKey, TValue>(this IDictionary<TKey, TValue> sourceDic)
        {
            return new ConcurrentDictionary<TKey, TValue>(sourceDic);
        }
    }
}
