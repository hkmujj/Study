using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommonUtil.Util;

namespace ES.Facility.PublicModule.IO
{
    /// <summary>
    /// 
    /// </summary>
    public class IniFieldAttributeCollection
    {
        private readonly List<IniFieldAttribute> m_MemberAttributes;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyNames"></param>
        public IniFieldAttributeCollection(Type type, params string[] propertyNames)
        {
            m_MemberAttributes = new List<IniFieldAttribute>();
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
                AddAllIniFieldAttributes(type);
            }
            else
            {
                AddIniFieldAttributes(type, propertyNames);
            }

        }

        private void AddIniFieldAttributes(Type type, IEnumerable<string> propertyNames)
        {
            IList<PropertyInfo> validPropertyInfos = new List<PropertyInfo>();

            foreach (var propertyName in propertyNames)
            {
                if (propertyName == null || string.IsNullOrEmpty(propertyName) || string.IsNullOrWhiteSpace(propertyName))
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
                    AddAttributes(property.GetCustomAttributes(true).OfType<IniFieldAttribute>().FirstOrDefault(), property);
                }
            }
        }

        private void AddAllIniFieldAttributes(Type type)
        {
            var properties = type.GetProperties();
            foreach (var propertyInfo in properties)
            {
                var attr = AttributeUtility.GetCustomAttribute<IniFieldAttribute>(propertyInfo).FirstOrDefault();

                AddAttributes(attr, propertyInfo);
            }
        }

        private void AddAttributes(IniFieldAttribute attr, PropertyInfo propertyInfo)
        {
            if (attr == null)
            {
                return;
            }

            attr.PropertyInfo = propertyInfo;

            this.m_MemberAttributes.Add(attr);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idx"></param>
        public IniFieldAttribute this[int idx]
        {
            get { return m_MemberAttributes[idx]; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        public IniFieldAttribute this[string keyName]
        {
            get { return m_MemberAttributes.Find(f => f.KeyName == keyName); }
        }

        public IEnumerator<IniFieldAttribute> GetEnumerator()
        {
            return m_MemberAttributes.GetEnumerator();
        }
    }
}
