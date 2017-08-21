using MMI.Facility.Interface.Project;
using Tram.CBTC.Infrasturcture.ViewModel.Monitor;

namespace Tram.CBTC.Infrasturcture.Monitor
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
