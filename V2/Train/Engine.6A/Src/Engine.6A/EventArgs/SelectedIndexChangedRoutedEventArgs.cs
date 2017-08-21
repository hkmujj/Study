using System.Collections.Generic;
using System.Windows;

namespace Engine._6A.EventArgs
{
    public class SelectedIndexChangedRoutedEventArgs : RoutedEventArgs
    {
        public SelectedIndexChangedRoutedEventArgs() { }
        //
        // 摘要: 
        //     使用提供的路由事件标识符初始化 System.Windows.RoutedEventArgs 类的一个新实例。
        //
        // 参数: 
        //   routedEvent:
        //     System.Windows.RoutedEventArgs 类的此实例的路由事件标识符。
        public SelectedIndexChangedRoutedEventArgs(RoutedEvent routedEvent) : base(routedEvent) { }
        //
        // 摘要: 
        //     使用提供的路由事件标识符初始化 System.Windows.RoutedEventArgs 类的一个新实例，同时提供为事件另外声明一个源的机会。
        //
        // 参数: 
        //   routedEvent:
        //     System.Windows.RoutedEventArgs 类的此实例的路由事件标识符。
        //
        //   source:
        //     将在处理事件时报告的备用源。这将预先填充 System.Windows.RoutedEventArgs.Source 属性。
        public SelectedIndexChangedRoutedEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source) { }

        public int NewIndex { get; set; }

        public int OldIndex { get; set; }

        public IEnumerable<object> StateCollection { get; set; }

    }
}