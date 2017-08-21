using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.AvalonDock.Layout;

namespace MMITool.Platform.View.Layout
{
    class PanesTemplateSelector : DataTemplateSelector
    {
        public DataTemplate UserWorkSpaceViewTemplate { get; set; }

        public DataTemplate ToolViewTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var itemAsLayoutContent = item as LayoutContent;

            //if (item is UserWorkSpaceViewModel)
            //    return UserWorkSpaceViewTemplate;

            //if (item is ToolViewModel)
            //    return ToolViewTemplate;

            return base.SelectTemplate(item, container);
        }
    }
}
