using System;

namespace MessageManager.Extention
{
    public static class MessageExtention
    {
        /// <summary>
        /// 将克隆的类型转换为IMessage的派生类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T Cast<T>(this object value) where T : IMessage
        {
            try
            {
                return (T)value;
            }
            catch (Exception e)
            {
                Console.Error.WriteAsync(e.ToString());
            }
            return default(T);
        }
    }
}
