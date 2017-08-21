using System.Windows;
using CBTC.Infrasturcture.Model.Send;
using CBTC.Infrasturcture.Monitor;
using CBTC.Infrasturcture.ViewModel.Monitor;
using Microsoft.Practices.ServiceLocation;
using Subway.CBTC.THALES.ViewModel;

namespace Subway.CBTC.THALES.View.Shell
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class THALESShell : Window
    {
        private readonly DomainViewModel m_DomainViewModel;


        public THALESShell()
        {
            InitializeComponent();


            m_DomainViewModel = ServiceLocator.Current.GetInstance<DomainViewModel>();

            m_DomainViewModel.Domain.SendInterface = new EmptySendInterface();

            DataContext = m_DomainViewModel;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            var monitor = new DomainMonitorWindow()
            {
                Owner = this,
                DataContext = new MonitorViewModel<DomainViewModel>(m_DomainViewModel)
            };
            monitor.Show();
        }
    }
}