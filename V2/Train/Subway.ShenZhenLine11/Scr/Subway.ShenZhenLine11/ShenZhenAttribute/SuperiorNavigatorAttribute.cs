using System;

namespace Subway.ShenZhenLine11.ShenZhenAttribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SuperiorNavigatorAttribute : Attribute
    {
        public SuperiorNavigatorAttribute()
        {

        }
        /// <summary>
        /// 控件全名称
        /// </summary>
        public string ControlFullName { get; set; }
        public Type Type { get; set; }
    }
}