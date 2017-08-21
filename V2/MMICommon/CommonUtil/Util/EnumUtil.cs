using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace CommonUtil.Util
{
    /// <summary>
    /// 
    /// </summary>
    public static class EnumUtil
    {
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private static readonly Dictionary<Type, Dictionary<string, object>> m_EnumDescriptionDictionary =
                new Dictionary<Type, Dictionary<string, object>>();


        [SuppressMessage("ReSharper", "InconsistentNaming")] 
        private static readonly Dictionary<Type, Dictionary<Enum, Attribute[]>> m_EnumAttibuteBuffer = new Dictionary<Type, Dictionary<Enum, Attribute[]>>();

        /// <summary>
        /// value : ReadOnlyCollection<T>
        /// </summary>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private static  readonly Dictionary<Type, object> m_AllEnumsDictionary = new Dictionary<Type, object>();

        /// <summary>
        /// 获取枚举类型的所有值。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ReadOnlyCollection<T> GetAllEnums<T>()
        {
            var type = typeof (T);
            if (!type.IsEnum)
            {
                LogMgr.Warn("GetAllEnums error, {0} is not an enum type", type);
                m_AllEnumsDictionary.Add(type, new ReadOnlyCollection<T>(new List<T>()));
            }
            if (!m_AllEnumsDictionary.ContainsKey(typeof(T)))
            {
                m_AllEnumsDictionary.Add(type, Enum.GetValues(type).Cast<T>().ToList().AsReadOnly());
            }
            return (ReadOnlyCollection<T>)m_AllEnumsDictionary[typeof(T)];
        }

        /// <summary>
        /// 获得 Description   的特性信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<string> GetDescription(Enum obj)
        {
            return GetObjDescription(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GetAttibutes<T>(this Enum obj) where T : Attribute
        {
            var type = obj.GetType();
            if (!m_EnumAttibuteBuffer.ContainsKey(type))
            {
                m_EnumAttibuteBuffer.Add(type, new Dictionary<Enum, Attribute[]>());
            }
            if (!m_EnumAttibuteBuffer[type].ContainsKey(obj))
            {
                var fild = type.GetFields().First(w => w.Name == obj.ToString());
                m_EnumAttibuteBuffer[type].Add(obj, fild.GetCustomAttributes(false).Cast<Attribute>().ToArray());
            }
            return m_EnumAttibuteBuffer[type][obj].OfType<T>();
        }

        private static List<string> GetObjDescription(object obj)
        {
            var filds = obj.GetType().GetFields();

            foreach (var info in filds)
            {
                if (info.Name == obj.ToString())
                {
                    var curAttr = info.GetCustomAttributes(false);
                    return FindDescriptionAttr(curAttr);
                }
            }

            return new List<string>();
        }

        private static List<string> FindDescriptionAttr(IEnumerable<object> attrs)
        {
            return attrs.OfType<DescriptionAttribute>().Select(attribute => attribute.Description).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="description"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static T FindEnumByDescriptio<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum)
            {
                throw new ArgumentException();
            }
            if (!m_EnumDescriptionDictionary.ContainsKey(type))
            {
                m_EnumDescriptionDictionary.Add(type,
                    Enum.GetValues(type).Cast<T>().ToDictionary(kvp => GetObjDescription(kvp)[0], kvp => (object)kvp));
            }
            if (!m_EnumDescriptionDictionary[type].ContainsKey(description))
            {
                LogMgr.Debug("Can not parser {0} to type({1}) by description, reture default({1})", description, type);
                return default(T);
            }
            var rlt = (T)m_EnumDescriptionDictionary[type][description];
            LogMgr.Debug("Success parser {0} to type({1}) by description, reture {2}", description, type, rlt);
            return rlt;
        }
    }
}
