using System;
using System.Linq;
using CommonUtil.Model;

namespace CommonUtil.Util.Extension
{
    /// <summary>
    /// 
    /// </summary>
    public static class FloatExtension
    {

        /// <summary>
        /// 使用字节对比方式对比float 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsDifferentTo(this float src, float target)
        {
            var s = new FloatBytesMap() {Float = src};
            var t = new FloatBytesMap() {Float = target};

            return s.Byte0 != t.Byte0 || s.Byte1 != t.Byte1 || s.Byte2 != t.Byte2 || s.Byte3 != t.Byte3;
        }
    }
}