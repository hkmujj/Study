using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using MMI.Facility.DataType.Running;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data.Config;

namespace MMI.Facility.DataType
{
    public class AdapterChangedDataReadOnlyDictionary<TKey, TValue> : IChangedDataAdapter<TKey, TValue>
    {
        private readonly object m_LockObj = new object();

        protected readonly IDictionary<TKey, TValue> m_ActureDictionary;
        private IEnumerable<string> m_ActivedProjectNameCollection;
        private Dictionary<string, bool> m_RequestedClearChangedProjectDictionary;


        private readonly ConcurrentDictionary<TKey, TValue> m_ChangedValueDictionary;


        private Dictionary<string, bool> RequestedClearChangedProjectDictionary
        {
            get
            {
                return m_RequestedClearChangedProjectDictionary ??
                       (m_RequestedClearChangedProjectDictionary =
                           ActivedProjectNameCollection.ToDictionary(kvp => kvp, kvp => false));
            }
        }

        private IEnumerable<string> ActivedProjectNameCollection
        {
            get
            {
                return m_ActivedProjectNameCollection ??
                       (m_ActivedProjectNameCollection = GetActivedAppNames());
            }
        }

        private IEnumerable<string> GetActivedAppNames()
        {
            var vs = App.Current.MainForm.DataPackage.ServiceManager.GetService<IRunningViewService>();

            return
                vs.ActivedFormCollection.Where(w => w.AppConfig.SubsystemType == SubsystemType.Addin)
                    .Select(s => s.AppName);
        }


        public AdapterChangedDataReadOnlyDictionary(IDictionary<TKey, TValue> dictionary,
            IDictionary<TKey, TValue> changedValueDictionary)
        {
            m_ActureDictionary = dictionary;
            m_ChangedValueDictionary = new ConcurrentDictionary<TKey, TValue>(changedValueDictionary);

        }

        public AdapterChangedDataReadOnlyDictionary(IDictionary<TKey, TValue> dictionary)
            : this(dictionary, new Dictionary<TKey, TValue>())
        {

        }

        public void UpdateActivedProjectNameCollection()
        {
            m_ActivedProjectNameCollection = null;
            Dictionary<string, bool> oldReq = null;
            if (m_RequestedClearChangedProjectDictionary != null)
            {
                oldReq = new Dictionary<string, bool>(m_RequestedClearChangedProjectDictionary);
            }
            m_RequestedClearChangedProjectDictionary = null;
            if (oldReq != null)
            {
                foreach (var kvp in oldReq.Where(kvp => RequestedClearChangedProjectDictionary.ContainsKey(kvp.Key)))
                {
                    RequestedClearChangedProjectDictionary[kvp.Key] = kvp.Value;
                }
            }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var kvp in m_ActureDictionary)
            {
                if (m_ChangedValueDictionary.ContainsKey(kvp.Key))
                {
                    yield return m_ChangedValueDictionary.First(f => f.Key.Equals(kvp.Key));
                }
                yield return kvp;
            }
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return m_ActureDictionary.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotSupportedException();
        }


        public int Count
        {
            get { return m_ActureDictionary.Count; }
        }

        public bool ContainsKey(TKey key)
        {
            return m_ActureDictionary.ContainsKey(key);
        }


        public bool TryGetValue(TKey key, out TValue value)
        {
            return m_ChangedValueDictionary.TryGetValue(key, out value) ||
                   m_ActureDictionary.TryGetValue(key, out value);
        }

        public void AddChanges(IDictionary<TKey, TValue> changes)
        {
            foreach (var change in changes)
            {
                m_ChangedValueDictionary.AddOrUpdate(change.Key, change.Value, (key, value) => value);
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                if (m_ChangedValueDictionary.ContainsKey(key))
                {
                    return m_ChangedValueDictionary[key];
                }
                return m_ActureDictionary[key];
            }
        }

        public ICollection<TKey> Keys
        {
            get { return m_ActureDictionary.Keys; }
        }

        public ICollection<TValue> Values
        {
            get { return m_ActureDictionary.Values; }
        }

        public void RequestClearChanged(string projectName)
        {
            if (RequestedClearChangedProjectDictionary.ContainsKey(projectName))
            {
                lock (m_LockObj)
                {
                    RequestedClearChangedProjectDictionary[projectName] = true;
                }
            }

            if (RequestedClearChangedProjectDictionary.All(a => a.Value))
            {
                lock (m_LockObj)
                {

                    foreach (var name in ActivedProjectNameCollection)
                    {
                        RequestedClearChangedProjectDictionary[name] = false;
                    }
                }
                ClearChanged();

            }
        }

        public void ClearChanged()
        {
            m_ChangedValueDictionary.Clear();
        }

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