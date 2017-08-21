using System;
using System.Runtime.InteropServices;

namespace CommonUtil.Util
{
    /// <summary>
    /// 
    /// </summary>
    public static class BytesStructConverter
    {
        /// <summary>
        /// 将Byte转换为结构体类型
        /// </summary>
        /// <param name="structObj"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static byte[] StructToBytes(object structObj, int size)
        {
            var bytes = new byte[size];
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            //将结构体拷到分配好的内存空间
            Marshal.StructureToPtr(structObj, structPtr, false);
            //从内存空间拷贝到byte 数组
            Marshal.Copy(structPtr, bytes, 0, size);
            //释放内存空间
            Marshal.FreeHGlobal(structPtr);
            return bytes;

        }

        //
        /// <summary>
        /// 将Byte转换为结构体类型
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object ByteToStruct(byte[] bytes, Type type)
        {
            int size = Marshal.SizeOf(type);
            if (size > bytes.Length)
            {
                return null;
            }
            //分配结构体内存空间
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            //将byte数组拷贝到分配好的内存空间
            Marshal.Copy(bytes, 0, structPtr, size);
            //将内存空间转换为目标结构体
            object obj = Marshal.PtrToStructure(structPtr, type);
            //释放内存空间
            Marshal.FreeHGlobal(structPtr);
            return obj;
        }

        /// <summary>
        /// 将Byte转换为结构体类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T ByteToStruct<T>(byte[] data)
        {
            return (T) ByteToStruct(data, typeof (T));
        }
    }
}