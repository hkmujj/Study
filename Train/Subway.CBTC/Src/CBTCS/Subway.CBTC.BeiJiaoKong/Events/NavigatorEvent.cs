using System;
using Microsoft.Practices.Prism.Events;

namespace Subway.CBTC.BeiJiaoKong.Events
{
    /// <summary>
    /// 导航事件
    /// </summary>
    public class NavigatorEvent : CompositePresentationEvent<NavigatorEventArgs>
    {
       
    }
    /// <summary>
    /// 导航参数
    /// </summary>
    public class NavigatorEventArgs
    {
        private Type m_ViewType;

        /// <summary>
        /// 视图名称
        /// </summary>
        public string ViewName { get; set; }

        /// <summary>
        /// 视图类型
        /// </summary>
        public Type ViewType
        {
            get { return m_ViewType; }
            set
            {
                m_ViewType = value;
                if (value != null)
                {
                    ViewName = value.FullName;
                }
            }
        }
    }
}