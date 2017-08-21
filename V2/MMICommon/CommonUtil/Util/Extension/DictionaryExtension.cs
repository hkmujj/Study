using System.Collections.Concurrent;
using System.Collections.Generic;
using CommonUtil.Model;

namespace CommonUtil.Util.Extension
{
    /// <summary>
    /// 
    /// </summary>
    public static class DictionaryExtension
    {
        /// <summary>
        /// 转换成 只读 dictionary
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static IReadOnlyDictionary<TKey, TValue> AsReadOnlyDictionary<TKey, TValue>(this IDictionary<TKey, TValue> src)
        {
            return new ReadOnlyDictionary<TKey, TValue>(src);
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