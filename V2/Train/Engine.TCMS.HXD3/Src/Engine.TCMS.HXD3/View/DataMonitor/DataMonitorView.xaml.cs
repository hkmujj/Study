using System.ComponentModel.Composition;
using Engine.TCMS.HXD3.ViewModel;

namespace Engine.TCMS.HXD3.View.DataMonitor
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
        public DataMonitorView(HXD3TCMSViewModel viewModel)
            : this()
        {
            DataContext = viewModel;
        }
    }
}
