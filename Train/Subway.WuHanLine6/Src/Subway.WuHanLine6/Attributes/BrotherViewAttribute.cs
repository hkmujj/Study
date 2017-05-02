using System;

namespace Subway.WuHanLine6.Attributes
{
    /// <summary>
    /// 兄弟控件
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class BrotherViewAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="brotherView"></param>
        public BrotherViewAttribute(Type brotherView)
        {
            BrotherView = brotherView;
        }

        /// <summary>
        /// 兄弟控件
        /// </summary>
        public Type BrotherView { get; private set; }
    }
}