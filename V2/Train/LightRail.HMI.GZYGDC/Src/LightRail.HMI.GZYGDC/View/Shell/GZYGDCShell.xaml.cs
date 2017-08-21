using LightRail.HMI.GZYGDC.View.DataMonitor;
using LightRail.HMI.GZYGDC.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace LightRail.HMI.GZYGDC.View.Shell
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GZYGDCShell
    {
        public GZYGDCShell()
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