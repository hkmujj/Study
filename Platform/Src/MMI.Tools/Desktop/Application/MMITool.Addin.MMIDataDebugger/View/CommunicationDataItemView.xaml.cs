using System.Windows.Controls;
using DevExpress.Xpf.Grid;

namespace MMITool.Addin.MMIDataDebugger.View
{
    /// <summary>
    /// CommunicationDataView.xaml 的交互逻辑
    /// </summary>
    public partial class CommunicationDataItemView : UserControl
    {
        public CommunicationDataItemView()
        {
            InitializeComponent();
        }

        private void TableView_OnCellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            var tv = (TableView) sender;
            tv.CloseEditor();
        }
    }
}
