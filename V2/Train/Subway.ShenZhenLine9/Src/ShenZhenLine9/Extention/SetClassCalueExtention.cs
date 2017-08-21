using System.Linq;

namespace Subway.ShenZhenLine9.Extention
{
    /// <summary>
    /// 
    /// </summary>
    public static class SetClassCalueExtention
    {
        /// <summary>
        /// 设置类中的某一类型的值
        /// </summary>
        /// <param name="classValue"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        public static void Cast<T>(this object classValue, T value)
        {
            var type = classValue.GetType();
            if (type.IsClass)
            {
                type.GetProperties().Where(w => w.PropertyType == typeof(T)).ForEach(f => f.SetValue(classValue, value, null));
            }
        }
    }
}