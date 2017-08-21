using System;

namespace Subway.XiaMenLine1.Interface
{
    public static class TypeConverter
    {
        public static T ConverterToType<T>(this object value)
        {
            T result;
            try
            {
                result = (T)((object)value);
                return result;
            }
            catch (Exception e)
            {
                //AppLog.Info(e.ToString());
            }
            result = default(T);
            return result;
        }
    }
}