using System.Windows;
using Microsoft.Practices.ServiceLocation;
using Subway.TCMS.LanZhou.View.DataMonitor;
using Subway.TCMS.LanZhou.ViewModel;

namespace Subway.TCMS.LanZhou.View.Shell
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TAX2TCMSLanZhouShell
    {
        public TAX2TCMSLanZhouShell()
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