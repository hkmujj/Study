using Microsoft.Practices.ServiceLocation;
using Motor.HMI.CRH380D.View.DataMonitor;
using Motor.HMI.CRH380D.ViewModel;

namespace Motor.HMI.CRH380D.View.Shell
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CRH380DShell
    {
        public CRH380DShell()
        {
            InitializeComponent();

            DataContext = ServiceLocator.Current.GetInstance<DomainViewModel>();
        }

        private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var monitor = new DataMonitorWindow() {Owner = this};
            monitor.Show();
        }
    }
}