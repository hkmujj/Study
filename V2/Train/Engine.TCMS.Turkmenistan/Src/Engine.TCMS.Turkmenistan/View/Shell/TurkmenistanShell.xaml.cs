using Engine.TCMS.Turkmenistan.View.DataMonitor;
using Engine.TCMS.Turkmenistan.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Engine.TCMS.Turkmenistan.View.Shell
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TurkmenistanShell
    {
        public TurkmenistanShell()
        {
            InitializeComponent();

            DataContext = ServiceLocator.Current.GetInstance<DomainViewModel>();
        }

        private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var monitor = new DataMonitorWindow() { Owner = this };
            monitor.Show();
        }
    }
}