using System.Windows;
using Microsoft.Practices.ServiceLocation;
using Tram.CBTC.Casco.ViewModel;
using Tram.CBTC.Infrasturcture.Model.Send;
using Tram.CBTC.Infrasturcture.Monitor;
using Tram.CBTC.Infrasturcture.ViewModel.Monitor;

namespace Tram.CBTC.Casco.View.Shell
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