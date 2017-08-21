using Engine.TAX2.SS7C.View.DataMonitor;
using Engine.TAX2.SS7C.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Engine.TAX2.SS7C.View.Shell
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TAX2SS7CShell
    {
        public TAX2SS7CShell()
        {
            InitializeComponent();

            DataContext = ServiceLocator.Current.GetInstance<SS7CViewModel>();
        }

        private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var monitor = new DataMonitorWindow() {Owner = this};
            monitor.Show();
        }
    }
}