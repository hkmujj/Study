using System.Windows;
using CBTC.Infrasturcture.Model.Send;
using CBTC.Infrasturcture.Monitor;
using CBTC.Infrasturcture.ViewModel.Monitor;
using Microsoft.Practices.ServiceLocation;
using Subway.CBTC.Casco.ViewModel;

namespace Subway.CBTC.Casco.View.Shell
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CascoShell : Window
    {
        private readonly DomainViewModel m_DomainViewModel;


        public CascoShell()
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