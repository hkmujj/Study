using System.ComponentModel.Composition;
using System.Windows.Controls;
using LightRail.HMI.SZLHLF.ViewModel;

namespace LightRail.HMI.SZLHLF.View.DataMonitor
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
        public DataMonitorView(SZLHLFViewModel viewModel)
            : this()
        {
            DataContext = viewModel;
        }
    }
}