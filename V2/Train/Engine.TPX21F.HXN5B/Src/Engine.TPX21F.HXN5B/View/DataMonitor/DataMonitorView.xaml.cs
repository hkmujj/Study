using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.ViewModel;

namespace Engine.TPX21F.HXN5B.View.DataMonitor
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
        public DataMonitorView(HXN5BViewModel viewModel)
            : this()
        {
            DataContext = viewModel;
        }
    }
}