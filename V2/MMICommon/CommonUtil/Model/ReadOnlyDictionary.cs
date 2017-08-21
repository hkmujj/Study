using System.Collections;
using System.Collections.Generic;
using CommonUtil.Util;

namespace CommonUtil.Model
{
    /// <summary>
    /// ReadOnlyDictionary
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class ReadOnlyDictionary<TKey, TValue> : IReadOnlyDictionary<TKey,TValue>
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly IDictionary<TKey, TValue> m_ActureDictionary;

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
            get
            {
                if (!m_ActureDictionary.ContainsKey(key))
                {
                    LogMgr.Info(string.Format("Can not found key 【{0}】, this will be case exception where called this[TKey key] func.", key));   
                }
                return m_ActureDictionary[key];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<TKey> Keys { get { return m_ActureDictionary.Keys; } }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<TValue> Values { get { return m_ActureDictionary.Values; } }

        /// <summary>
        /// 创建作为当前实例副本的新对象。
        /// </summary>
        /// <returns>
        /// 作为此实例副本的新对象。
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public object Clone()
        {
            return new ReadOnlyDictionary<TKey, TValue>(new Dictionary<TKey, TValue>(this.m_ActureDictionary));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_ActureDictionary.GetEnumerator();
        }
    }
}
