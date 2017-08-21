using System.Windows;
using Engine.TCMS.HXD3.View.DataMonitor;
using Engine.TCMS.HXD3.View.Shell;
using Microsoft.Practices.ServiceLocation;

namespace Engine.TCMS.HXD3.WPFApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HXD3TCMS
    {
        public HXD3TCMS()
        {
            InitializeComponent();
            Content = ServiceLocator.Current.GetInstance<TCMSShell>();

        }

        private void HXD3TCMS_OnLoaded(object sender, RoutedEventArgs e)
        {
            var monitor = new HXD3TCMSDataMonitor {Owner = this};
            monitor.Show();
        }
    }
}
