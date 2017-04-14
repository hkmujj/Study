using System;
using System.Collections.Generic;

namespace MMI.Facility.Interface
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public interface IReadOnlyDictionary<TKey, TValue> : ICloneable, IEnumerable<KeyValuePair<TKey, TValue>>
    {
        /// <summary>
        /// 
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsReadOnly { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        new IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool Contains(KeyValuePair<TKey, TValue> item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool ContainsKey(TKey key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool TryGetValue(TKey key, out TValue value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        TValue this[TKey key] { get; }

        /// <summary>
        /// 
        /// </summary>
        ICollection<TKey> Keys { get; }

        /// <summary>
        /// 
        /// </summary>
        ICollection<TValue> Values { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public interface IChangedDataAdapter<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectName"></param>
        void RequestClearChanged(string projectName);

        /// <summary>
        /// 
        /// </summary>
        void ClearChanged();
    }
}
