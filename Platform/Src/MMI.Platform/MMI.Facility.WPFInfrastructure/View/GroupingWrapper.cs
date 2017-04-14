using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mmi.Common.Control.WPF
{
    class GroupingWrapper : IGrouping<object, object>
    {
        readonly object m_Key;
        readonly IEnumerable<object> m_Collection;

        GroupingWrapper(object key, IEnumerable<object> collection)
        {
            this.m_Key = key;
            this.m_Collection = collection;
        }

        #region IGrouping<object, object> 成员

        public object Key
        {
            get { return m_Key; }
        }

        public IEnumerator<object> GetEnumerator()
        {
            return m_Collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        //通过IGrouping<TKey, TElement>创建IGrouping<object, object>
        public static GroupingWrapper Create<TKey, TElement>(IGrouping<TKey, TElement> igroup)
        {
            if (igroup == null)
                throw new ArgumentNullException("igroup");

            return new GroupingWrapper(igroup.Key, igroup.Cast<object>());
        }

        //通过object创建IGrouping<object, object>
        public static GroupingWrapper Create(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            return new GroupingWrapper(obj.GetType().GetProperty("Key").GetValue(obj, new object[0]),
                ((IEnumerable)obj).Cast<object>());
        }
    }
}