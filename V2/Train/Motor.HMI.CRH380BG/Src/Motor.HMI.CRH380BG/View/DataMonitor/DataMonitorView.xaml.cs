using System.ComponentModel.Composition;
using Motor.HMI.CRH380BG.ViewModel;

namespace Motor.HMI.CRH380BG.View.DataMonitor
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
        public DataMonitorView(ViewModelManager viewModel)
            : this()
        {
            DataContext = viewModel;
        }
    }
}