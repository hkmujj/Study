using System.Windows;
using Motor.TCMS.CRH400BF.View.DataMonitor;

namespace Motor.TCMS.CRH400BF.View.Shell
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainCRH400BFShell
    {
        public MainCRH400BFShell()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var monitor = new DataMonitorWindow
            {
                Owner = this,
                DataContext = DataContext
            };
            monitor.Show();
        }
    }
}