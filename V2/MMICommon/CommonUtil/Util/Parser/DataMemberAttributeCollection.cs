using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommonUtil.Util.Parser
{
    /// <summary>
    /// 
    /// </summary>
    public class DataMemberAttributeCollection
    {
        private List<DataMemberAttribute> m_MemberAttributes;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyNames"></param>
        public DataMemberAttributeCollection(Type type, params string[] propertyNames)
        {
            m_MemberAttributes = new List<DataMemberAttribute>();
            GetConfiguration(type, propertyNames);
        }

        /// <summary>
        /// 
        /// </summary>
        public int Count { get { return m_MemberAttributes.Count; }}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyNames"></param>
        public void GetConfiguration(Type type, params string[] propertyNames)
        {
            if (type == null || !type.GetProperties().Any())
            {
                return;
            }

            if (propertyNames == null || !propertyNames.Any())
            {
                AddAllDataMemberAttributes(type);
            }
            else
            {
                AddDataMemberAttributes(type, propertyNames);
            }

            this.m_MemberAttributes = this.m_MemberAttributes.OrderBy(p => p.Order).ToList();
        }

        private void AddDataMemberAttributes(Type type, IEnumerable<string> propertyNames)
        {
            IList<PropertyInfo> validPropertyInfos = new List<PropertyInfo>();

            foreach (var propertyName in propertyNames)
            {
                if (propertyName.IsNullOrWhiteSpace())
                {
                    continue;
                }

                var tempPropertyInfo = type.GetProperty(propertyName.Trim());

                if (tempPropertyInfo == null)
                {
                    throw new ArgumentException(string.Format(@"Contains Invalid Property Name Arg : {0}.", propertyName.Trim()));
                }

                validPropertyInfos.Add(tempPropertyInfo);
            }

            if (validPropertyInfos.Any())
            {
                foreach (var property in validPropertyInfos)
                {
                    AddAttributes(new DataMemberAttribute(), property);
                }
            }
        }

        private void AddAllDataMemberAttributes(Type type)
        {
            var properties = type.GetProperties();
            foreach (var propertyInfo in properties)
            {
                var attr = AttributeUtility.GetCustomAttribute<DataMemberAttribute>(propertyInfo).FirstOrDefault();

                if (attr == null)
                {
                    continue;
                }

                if (!attr.IsRequire)
                {
                    continue;
                }

                if (this.m_MemberAttributes.Count(p => p.Order == attr.Order) > 0)
                {
                    throw new ArgumentException(string.Format(@"Contains Same Order {0}.Please Look Up DataMemberAttribute
                            Of The Type {1}", attr.Order, type.Name));
                }

                AddAttributes(attr, propertyInfo);
            }
        }

        private void AddAttributes(DataMemberAttribute attr, PropertyInfo propertyInfo)
        {
            if (attr.Name.IsNullOrWhiteSpace())
            {
                attr.Name = propertyInfo.Name;
            }

            attr.PropertyName = propertyInfo.Name;
            attr.PropertyType = propertyInfo.PropertyType;
            attr.PropertyInfo = propertyInfo;

            this.m_MemberAttributes.Add(attr);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idx"></param>
        public DataMemberAttribute this[int idx]
        {
            get { return m_MemberAttributes[idx]; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        public DataMemberAttribute this[string propertyName]
        {
            get { return m_MemberAttributes.Find(f => f.PropertyName == propertyName); }
        }
    }
}
