using System.Windows;
using CBTC.Infrasturcture.ViewModel.Monitor;

namespace CBTC.Infrasturcture.Monitor
{
    /// <summary>
    /// DomainMonitorView.xaml 的交互逻辑
    /// </summary>
    public partial class DomainMonitorView
    {
        public DomainMonitorView()
        {
            InitializeComponent();

            Loaded += OnLoaded;

        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            ((MonitorViewModel)DataContext).Controller.Initalize();
        }
    }
}
