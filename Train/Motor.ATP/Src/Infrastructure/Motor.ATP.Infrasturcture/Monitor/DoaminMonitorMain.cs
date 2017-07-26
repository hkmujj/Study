using MMI.Facility.Interface.Project;
using Motor.ATP.Infrasturcture.ViewModel;

namespace Motor.ATP.Infrasturcture.Monitor
{
    public partial class DoaminMonitorMain : ProjectFormBase
    {
        public DoaminMonitorMain(MonitorViewModel viewModel = null)
        {
            InitializeComponent();
            this.elementHost1.Child = new DomainView() { DataContext = viewModel };
        }
    }
}
