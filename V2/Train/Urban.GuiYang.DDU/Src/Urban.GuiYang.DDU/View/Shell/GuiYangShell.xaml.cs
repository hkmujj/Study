using System.Windows;
using Microsoft.Practices.ServiceLocation;
using Urban.GuiYang.DDU.View.DataMonitor;
using Urban.GuiYang.DDU.ViewModel;

namespace Urban.GuiYang.DDU.View.Shell
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GuiYangShell
    {
        public GuiYangShell()
        {
            InitializeComponent();

            DataContext = ServiceLocator.Current.GetInstance<DomainViewModel>();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var monitor = new DataMonitorWindow() {Owner = this};
            monitor.Show();
        }
    }
}