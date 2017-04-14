using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MMITool.Common.Util
{
    /// <summary>
    /// 
    /// </summary>
    public class AttributeUtility
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> GetCustomAttribute<T>(MemberInfo memberInfo)
        {

            var curAttr = memberInfo.GetCustomAttributes(false);
            return curAttr.OfType<T>().ToList();
        }
    }
}