using System;

namespace Subway.WuHanLine6.Attributes
{
    /// <summary>
    /// 父控件
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ParentViewAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parentView"></param>
        public ParentViewAttribute(Type parentView)
        {
            ParentView = parentView;
        }

        /// <summary>
        /// 当前控件的父控件
        /// </summary>
        public Type ParentView { get; private set; }
    }
}