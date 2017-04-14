using System.Collections;
using System.Collections.Generic;

namespace MMITool.Common.Model
{
    /// <summary>
    /// 只读字典
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class ReadOnlyDictionary<TKey, TValue> : IReadOnlyDictionary<TKey,TValue>
    {
        private readonly IDictionary<TKey, TValue> m_ActureDictionary;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictionary"></param>
        public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionary)
        {
            m_ActureDictionary = dictionary;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsReadOnly { get { return true; }}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return m_ActureDictionary.GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return m_ActureDictionary.Contains(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            m_ActureDictionary.CopyTo(array, arrayIndex);
        }


        /// <summary>
        /// 
        /// </summary>
        public int Count { get { return m_ActureDictionary.Count; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            return m_ActureDictionary.ContainsKey(key);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return m_ActureDictionary.TryGetValue(key, out value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public TValue this[TKey key]
        {
            get { return m_ActureDictionary[key]; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<TKey> Keys { get { return m_ActureDictionary.Keys; } }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<TValue> Values { get { return m_ActureDictionary.Values; } }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_ActureDictionary.GetEnumerator();
        }
    }
}
