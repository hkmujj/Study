using System.Collections;
using System.Collections.Generic;
using CommonUtil.Util;
using MMI.Facility.Interface;

namespace MMI.Facility.DataType
{
    public class ReadOnlyDictionary<TKey, TValue> : IReadOnlyDictionary<TKey,TValue>
    {
        protected readonly IDictionary<TKey, TValue> m_ActureDictionary;

        public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionary)
        {
            m_ActureDictionary = dictionary;
        }

        public bool IsReadOnly { get { return true; }}

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return m_ActureDictionary.GetEnumerator();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return m_ActureDictionary.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            m_ActureDictionary.CopyTo(array, arrayIndex);
        }


        public int Count { get { return m_ActureDictionary.Count; } }

        public bool ContainsKey(TKey key)
        {
            return m_ActureDictionary.ContainsKey(key);
        }


        public bool TryGetValue(TKey key, out TValue value)
        {
            return m_ActureDictionary.TryGetValue(key, out value);
        }

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

        public ICollection<TKey> Keys { get { return m_ActureDictionary.Keys; } }

        public ICollection<TValue> Values { get { return m_ActureDictionary.Values; } }

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
