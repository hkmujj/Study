using System;
using System.Runtime.InteropServices;

namespace CommonUtil.Model
{
    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = sizeof(float))]
    public struct FloatBytesMap
    {
        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(0)]
        public float Float;

        ///// <summary>
        ///// 
        ///// </summary>
        //[FieldOffset(0)]
        //public byte[] Bytes;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(0)]
        public byte Byte0;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(1)]
        public byte Byte1;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(2)]
        public byte Byte2;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(3)]
        public byte Byte3;

#if X64
        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(4)]
        public byte Byte4;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(5)]
        public byte Byte5;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(6)]
        public byte Byte6;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(7)]
        public byte Byte7;

#endif
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public byte this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return Byte0;
                    case 1: return Byte1;
                    case 2: return Byte2;
                    case 3: return Byte3;
#if X64
                    case 4: return Byte4;
                    case 5: return Byte5;
                    case 6: return Byte6;
                    case 7: return Byte7;
#endif
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }
    }
}