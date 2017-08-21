using System;
using CommonUtil.Util;

namespace Subway.WuHanLine6.Extention
{
    /// <summary>
    ///
    /// </summary>
    public static class ObjectToType
    {
        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="value">Object 类型</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Cast<T>(this object value)
        {
            T tmpValue = default(T);

            try
            {
                tmpValue = (T)value;
            }
            catch (Exception e)
            {
                AppLog.Error("类型转换失败");
                AppLog.Error(e.ToString());
            }
            return tmpValue;
        }
    }
}