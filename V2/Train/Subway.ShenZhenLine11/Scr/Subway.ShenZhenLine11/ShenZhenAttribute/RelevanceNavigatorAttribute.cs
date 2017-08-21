using System;

namespace Subway.ShenZhenLine11.ShenZhenAttribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RelevanceNavigatorAttribute : Attribute
    {
        public RelevanceNavigatorAttribute(string controlName)
        {
            ControlName = controlName;
        }
        /// <summary>
        /// 控件全路径
        /// </summary>
        public string ControlName { get; private set; }
    }
}