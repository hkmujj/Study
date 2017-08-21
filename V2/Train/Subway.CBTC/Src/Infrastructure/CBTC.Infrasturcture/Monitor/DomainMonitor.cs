using CBTC.Infrasturcture.ViewModel.Monitor;
using MMI.Facility.Interface.Project;

namespace CBTC.Infrasturcture.Monitor
{
    public partial class DomainMonitor : ProjectFormBase
    {
        public DomainMonitor(MonitorViewModel viewModel = null)
        {
            InitializeComponent();

            elementHost1.Child = new DomainMonitorView() {DataContext = viewModel};
        }
    }
}
