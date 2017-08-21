using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CommonUtil.Util;
using Subway.ShenZhenLine11.Constance;

namespace Subway.ShenZhenLine11.Extention
{
    public static class EnumExtention
    {
        /// <summary>
        /// 获取枚举类型的值 并转换为HelUnit
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumerable<HelpUnit> GetUnit<T>(this IEnumerable<T> value) where T : struct
        {
            return value.Cast<System.Enum>()
                  .Where(w => w.GetAttibutes<DescriptionAttribute>().Count() != 0)
                  .Select(s => new HelpUnit()
                  {
                      Name = EnumUtil.GetDescription(s).FirstOrDefault(),
                      Key = s,
                  });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<HelpUnit> GetUnits<T>(this Type type) where T : struct
        {
            return System.Enum.GetValues(type)
                   .Cast<System.Enum>()
                   .Where(w => w.GetAttibutes<DescriptionAttribute>().Count() != 0)
                   .Select(s => new HelpUnit()
                   {
                       Name = EnumUtil.GetDescription(s).FirstOrDefault(),
                       Key = s,
                   });
        }
    }
}