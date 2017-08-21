using System.ComponentModel.Composition;
using Urban.GuiYang.DDU.ViewModel;

namespace Urban.GuiYang.DDU.View.DataMonitor
{
    /// <summary>
    /// DataMonitorView.xaml 的交互逻辑
    /// </summary>
    [Export]
    public partial class DataMonitorView 
    {
        public DataMonitorView()
        {
            InitializeComponent();
        }

        [ImportingConstructor]
        public DataMonitorView(DomainViewModel viewModel)
            : this()
        {
            DataContext = viewModel;
        }
    }
}