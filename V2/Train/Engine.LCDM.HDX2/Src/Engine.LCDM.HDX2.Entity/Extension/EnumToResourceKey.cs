using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CommonUtil.Util;
using Engine.LCDM.HDX2.Entity.Attributes;
using Engine.LCDM.HDX2.Entity.Model.Domain;

namespace Engine.LCDM.HDX2.Entity.Extension
{
    public static class EnumToResourceKey
    {
        public static string GetResourceKey(this Enum obj)
        {
            return GetResourceKeyAttr(obj).FirstOrDefault();
        }

        /// <summary>
        /// 获得 Description   的特性信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<string> GetResourceKeyAttr(Enum obj)
        {
            var filds = obj.GetType().GetFields();

            foreach (var info in filds)
            {
                if (info.Name == obj.ToString())
                {
                    var curAttr = info.GetCustomAttributes(false);
                    return FindResourceKeyAttr(curAttr);
                }
            }

            return new List<string>();
        }

        private static List<string> FindResourceKeyAttr(IEnumerable<object> attrs)
        {
            return attrs.OfType<ResourceKeyAttribute>().Select(attribute => attribute.ResourceKey).ToList();
        }
    }
}