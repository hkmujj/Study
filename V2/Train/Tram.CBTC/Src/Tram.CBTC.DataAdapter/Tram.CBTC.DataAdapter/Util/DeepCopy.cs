using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Tram.CBTC.DataAdapter.Model;

namespace Tram.CBTC.DataAdapter.Util
{
    /// <summary>
    /// 深拷贝 
    /// </summary>
    internal static class DeepCopy
    {
        private static readonly MemoryStream MemoryStreamBuffer = new MemoryStream();

        private static readonly ReaderWriterLockSlim MemoryStreamLock = new ReaderWriterLockSlim();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal static T CopyDataIn<T>(T obj) where T : SignalDataIn
        {
            MemoryStreamLock.EnterWriteLock();
            MemoryStreamBuffer.Position = 0;
            var bf = new BinaryFormatter {Binder = new Binder()};
            bf.Serialize(MemoryStreamBuffer, obj);
            MemoryStreamBuffer.Position = 0;
            MemoryStreamLock.ExitWriteLock();
            return bf.Deserialize(MemoryStreamBuffer) as T;
        }

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