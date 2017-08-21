using Engine.TPX21F.HXN5B.View.DataMonitor;
using Engine.TPX21F.HXN5B.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Engine.TPX21F.HXN5B.View.Shell
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TAX2HXN5BShell
    {
        public TAX2HXN5BShell()
        {
            InitializeComponent();

            DataContext = ServiceLocator.Current.GetInstance<HXN5BViewModel>();
        }

        private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var monitor = new DataMonitorWindow() {Owner = this};
            monitor.Show();
        }
    }
}