using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Subway.CBTC.BeiJiaoKong.Extentions
{
    /// <summary>
    /// 创 建 者：谭栋炜
    /// 创建时间：2017/1/16
    /// 修 改 者：谭栋炜
    /// 修改时间：2017/1/16
    /// </summary>
    public static class ClassExtention
    {
        /// <summary>
        /// 设置类中的统一属性的值
        /// </summary>
        /// <typeparam name="T1">类</typeparam>
        /// <typeparam name="T2">值</typeparam>
        /// <param name="targetClass">类</param>
        /// <param name="value">值</param>
        public static void SetAllPropertyValue<T1, T2>(this T1 targetClass, T2 value = default(T2)) where T1 : class
        {
            var type = typeof(T1);
            foreach (var info in type.GetProperties().Where(info => info.PropertyType == typeof(T2)))
            {
                info.SetValue(targetClass, value, BindingFlags.Public, null, null, CultureInfo.CurrentCulture);
            }
        }
        /// <summary>
        /// 设置类中的统一字段的值
        /// </summary>
        /// <typeparam name="T1">类</typeparam>
        /// <typeparam name="T2">值</typeparam>
        /// <param name="targetClass">类</param>
        /// <param name="value">值</param>
        public static void SetAllFiledValue<T1, T2>(this T1 targetClass, T2 value = default(T2)) where T1 : class
        {
            var type = typeof(T1);
            foreach (var info in type.GetFields().Where(info => info.FieldType == typeof(T2)))
            {
                info.SetValue(targetClass, value, BindingFlags.Public, null, CultureInfo.CurrentCulture);
            }
        }
    }
}