using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.ViewModel;

namespace Engine.TAX2.SS7C.View.DataMonitor
{
    /// <summary>
    /// DataMonitorView.xaml 的交互逻辑
    /// </summary>
    public partial class DataMonitorView 
    {
        public DataMonitorView()
        {
            InitializeComponent();
        }

        [ImportingConstructor]
        public DataMonitorView(SS7CViewModel viewModel)
            : this()
        {
            DataContext = viewModel;
        }
    }
}