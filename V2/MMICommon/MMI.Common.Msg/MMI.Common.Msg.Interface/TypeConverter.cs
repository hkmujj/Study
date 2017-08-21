using System;

namespace MMI.Common.Msg.Interface
{
    public static class TypeConverter
    {
        public static T Cast<T>(this object value)
        {
            T result;
            try
            {
                result = (T)((object)value);
                return result;
            }
            catch (Exception e)
            {
            }
            result = default(T);
            return result;
        }
    }
}