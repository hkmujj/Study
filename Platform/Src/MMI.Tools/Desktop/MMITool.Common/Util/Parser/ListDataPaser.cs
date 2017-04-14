using System;
using System.Collections.Generic;
using System.Linq;

namespace MMITool.Common.Util.Parser
{
    /// <summary>
    ///  解析 IEnumerable<IEnumerable></IEnumerable><string/>> 成 List<T />
    /// </summary>
    public class ListDataPaser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityRows"></param>
        /// <param name="propertyNames"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<T> Parse<T>(List<List<string>> entityRows, params string[] propertyNames) where T : new()
        {
            if (entityRows == null || !entityRows.Any())
            {
                return new List<T>();
            }

            var members = new DataMemberAttributeCollection(typeof(T), propertyNames);

            if (members.Count < 1)
            {
                return new List<T>();
            }

            return (from propertyValues in entityRows
                where propertyValues != null && propertyValues.Any()
                select Generate<T>(propertyValues, members)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityRows"></param>
        /// <param name="propertyNames"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Parse<T>(List<string> entityRows, params string[] propertyNames) where T : new()
        {
            if (entityRows == null || !entityRows.Any())
            {
                return default(T);
            }

            var members = new DataMemberAttributeCollection(typeof(T), propertyNames);

            if (members.Count < 1 || !entityRows.Any())
            {
                return default(T);
            }

            return Generate<T>(entityRows, members);
        }

        private static T Generate<T>(List<string> propertyValues, DataMemberAttributeCollection members) where T : new()
        {
            var entity = Activator.CreateInstance<T>();
            var memberCount = members.Count;
            var propertyCount = propertyValues.Count();

            if (memberCount == 0 || propertyCount == 0)
            {
                return entity;
            }


            var converPropertyValues = propertyValues.Take(Math.Min(memberCount, propertyCount)).ToList();

            for (int i = 0; i < converPropertyValues.Count; i++)
            {
                var propertyValue = converPropertyValues[i];
                var currAttribute = members[i];
                var currPropertyInfo = currAttribute.PropertyInfo;

                if (propertyValue == null)
                {
                    currPropertyInfo.SetValue(entity, null, null);
                    continue;
                }

                var result = DynamicInvokeHelper.DynamicInvoke(currAttribute.PropertyType, propertyValue);
                currPropertyInfo.SetValue(entity, result, null);
            }


            return entity;
        }
    }
}
