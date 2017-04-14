using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.DataType.Log;
using MMI.Facility.Interface;
using MMI.Facility.Interface.IndexDescription;

namespace MMI.Facility.DataType.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class DictionaryProxy<TKey, TValue> : NotificationObject, IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>
        where TKey : IEquatable<TKey>
    {
        private readonly ConcurrentDictionary<TKey, IndexValueDescriptionModel<TKey, TValue>> m_ProxyDictionary;

        private readonly ConcurrentDictionary<TKey, TValue> m_ProxyBuff;

        /// <summary>
        ///来自界面的 数据变化， 
        /// </summary>
        public event Action<DictionaryProxy<TKey, TValue>, IndexValueDescriptionModel<TKey, TValue>> ValueChanged;

        public IList<IndexValueDescriptionModel<TKey, TValue>> ProxyValues { private set; get; }

        public DictionaryProxy(IList<IndexValueDescriptionModel<TKey, TValue>> values)
        {

            ProxyValues = values;

            foreach (var model in ProxyValues)
            {
                model.PropertyChanged += IndexValueModelOnPropertyChanged;
            }

            m_ProxyDictionary =
                new ConcurrentDictionary<TKey, IndexValueDescriptionModel<TKey, TValue>>(
                    values.ToDictionary(kvp => kvp.Index, kvp => kvp));

            m_ProxyBuff = new ConcurrentDictionary<TKey, TValue>(values.ToDictionary(kvp => kvp.Index, kvp => kvp.Value));

        }

        /// <summary>
        /// 来自界面点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="propertyChangedEventArgs"></param>
        private void IndexValueModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == IndexValueDescriptionModel<TKey, TValue>.ValuePropertyName)
            {
                var sed = (IndexValueDescriptionModel<TKey, TValue>)sender;
                this[sed.Index] = sed.Value;

                ThreadPool.QueueUserWorkItem(s => OnValueChanged(this, (IndexValueDescriptionModel<TKey, TValue>) s),
                    sed);
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return m_ProxyBuff.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotSupportedException();
        }

        public void Clear()
        {
            throw new NotSupportedException();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            throw new NotSupportedException();
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotSupportedException();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotSupportedException();
        }

        public int Count
        {
            get { return m_ProxyDictionary.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool ContainsKey(TKey key)
        {
            return m_ProxyDictionary.ContainsKey(key);
        }

        public void Add(TKey key, TValue value)
        {
            throw new NotSupportedException();
        }

        public bool Remove(TKey key)
        {
            throw new NotSupportedException();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return m_ProxyBuff.TryGetValue(key, out value);
        }

        public TValue this[TKey key]
        {
            get
            {
                if (!m_ProxyDictionary.ContainsKey(key))
                {
                    SysLog.Debug("Can not found data where index={0}", key);
                }
                return m_ProxyDictionary[key].Value;
            }
            set
            {
                if (!m_ProxyDictionary.ContainsKey(key))
                {
                    SysLog.Debug("Can not found data where index={0}", key);
                }
                m_ProxyDictionary[key].PropertyChanged -= IndexValueModelOnPropertyChanged;
                m_ProxyDictionary[key].Value = value;
                m_ProxyDictionary[key].PropertyChanged += IndexValueModelOnPropertyChanged;
                m_ProxyBuff[key] = value;
            }
        }

        public ICollection<TKey> Keys
        {
            get { return m_ProxyDictionary.Keys; }
        }

        public ICollection<TValue> Values
        {
            get { return m_ProxyBuff.Values; }
        }

        public object Clone()
        {
            throw new NotSupportedException();
        }

        protected virtual void OnValueChanged(DictionaryProxy<TKey, TValue> arg1, IndexValueDescriptionModel<TKey, TValue> arg2)
        {
            var handler = ValueChanged;
            if (handler != null)
            {
                handler(arg1, arg2);
            }
        }
    }
}