using System;

namespace Subway.ShenZhenLine7.CusstomAttribute
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class BrotherAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type">控件类型</param>
        public BrotherAttribute(Type type)
        {
            Type = type;
        }

        /// <summary>
        /// 控件类型
        /// </summary>
        public Type Type { get; private set; }
    }
}