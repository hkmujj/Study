using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MMITool.Common.Util
{
    /// <summary>
    /// 
    /// </summary>
    public class EnumUtil
    {
        /// <summary>
        /// 获得 Description   的特性信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<string> GetDescription(Enum obj)
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
    }
}
