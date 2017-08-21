using System.ComponentModel.Composition;
using System.Windows.Controls;
using LightRail.HMI.GZYGDC.ViewModel;

namespace LightRail.HMI.GZYGDC.View.DataMonitor
{
    /// <summary>
    /// DataMonitorView.xaml 的交互逻辑
    /// </summary>
    public partial class DataMonitorView : UserControl
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