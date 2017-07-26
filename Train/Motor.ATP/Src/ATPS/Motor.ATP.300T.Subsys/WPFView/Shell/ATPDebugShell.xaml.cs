using System.Windows;
using Motor.ATP.Infrasturcture.Monitor;
using Motor.ATP._300T.Subsys.Model;

namespace Motor.ATP._300T.Subsys.WPFView.Shell
{
    /// <summary>
    /// ATPDebugShell.xaml 的交互逻辑
    /// </summary>
    public partial class ATPDebugShell 
    {
        public ATPDebugShell()
        {
            InitializeComponent();

            DataContext = new ATP300TDomainModel(null);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var monitor = new Window()
            {
                Width = 400,
                Height = 300,
                Owner = this,
                Content = new DomainView() {DataContext = DataContext}
            };
            monitor.Show();
        }
    }
}
