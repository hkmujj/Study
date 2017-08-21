using System.Windows;
using System.Windows.Controls;

namespace MMITool.Addin.MMIDataDebugger.Selector
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