using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CommonUtil.Model
{
    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public class FloatByteMap
    {
        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(0)]
        public float[] Float;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(0)]
        public byte[] Bytes;
    }
}