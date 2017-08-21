using System;
using System.Windows;

namespace MMI.Facility.WPFInfrastructure.Interactivity
{
    /// <summary>
    /// 扩展 CommandParameter, 包含事件的参数
    /// </summary>
    public class CommandParameter 
    {
        /// <summary>
        /// 事件触发源
        /// </summary>
        public DependencyObject Sender { set; get; }

        /// <summary>
        /// 事件参数
        /// </summary>
        public EventArgs EventArgs { set; get; }

        /// <summary>
        /// Parameter 参数
        /// </summary>
        public object Parameter { set; get; }
    }
}