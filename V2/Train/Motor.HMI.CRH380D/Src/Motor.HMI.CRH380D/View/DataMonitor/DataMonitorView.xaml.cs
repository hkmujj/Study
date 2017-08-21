using System.ComponentModel.Composition;
using System.Windows.Controls;
using Motor.HMI.CRH380D.ViewModel;

namespace Motor.HMI.CRH380D.View.DataMonitor
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