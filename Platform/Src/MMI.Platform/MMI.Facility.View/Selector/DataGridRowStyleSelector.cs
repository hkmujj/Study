using System.Windows;
using System.Windows.Controls;

namespace MMI.Facility.View.Selector
{
    public class DataGridRowStyleSelector : StyleSelector
    {
        public Style SelectedStyle { set; get; }

        public Style UnSelectedStyle { set; get; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            var row = (DataGridRow) container;
            return row.IsSelected ? SelectedStyle : UnSelectedStyle;
        }
    }
}