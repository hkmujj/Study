using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ES.Facility.PublicModule.Util
{
    /// <summary>
    /// 深拷贝 
    /// </summary>
    internal static class DeepCopy
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Copy<T>(T obj) where T : class 
        {
            using (var ms = new MemoryStream())
            {
                var bf = new BinaryFormatter { Binder = new Binder() };
                bf.Serialize(ms, obj);
                ms.Position = 0;
                return bf.Deserialize(ms) as T;
            }
        }

        /// <summary>
        /// 解决 反序列化报的无法找到程序集的BUG
        /// </summary>
        private class Binder : SerializationBinder
        {
            public override Type BindToType(string assemblyName, string typeName)
            {
                return Assembly.GetExecutingAssembly().GetType(typeName);
            }
        }
    }
}
