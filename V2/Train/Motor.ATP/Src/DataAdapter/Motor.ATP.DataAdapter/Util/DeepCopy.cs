using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Motor.ATP.DataAdapter.Model;

namespace Motor.ATP.DataAdapter.Util
{
    /// <summary>
    /// 深拷贝 
    /// </summary>
    internal static class DeepCopy
    {
        private static readonly MemoryStream MemoryStreamBuffer = new MemoryStream();

        private static readonly ReaderWriterLockSlim MemoryStreamLock = new ReaderWriterLockSlim();

        private static readonly MemoryStream MemoryStreamBufferOut = new MemoryStream();

        private static readonly ReaderWriterLockSlim MemoryStreamOutLock = new ReaderWriterLockSlim();

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
            var d =  bf.Deserialize(MemoryStreamBuffer) as T;
            MemoryStreamLock.ExitWriteLock();
            return d;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal static T CopyDataOut<T>(T obj) where T : SignalDataOut
        {
            MemoryStreamOutLock.EnterWriteLock();
            MemoryStreamBufferOut.Position = 0;
            var bf = new BinaryFormatter { Binder = new Binder() };
            bf.Serialize(MemoryStreamBufferOut, obj);
            MemoryStreamBufferOut.Position = 0;
            var d = bf.Deserialize(MemoryStreamBufferOut) as T;
            MemoryStreamOutLock.ExitWriteLock();
            return d;
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