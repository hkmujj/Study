using System.Windows;
using Microsoft.Practices.ServiceLocation;
using Tram.CBTC.Infrasturcture.Model.Send;
using Tram.CBTC.Infrasturcture.Monitor;
using Tram.CBTC.Infrasturcture.ViewModel.Monitor;
using Tram.CBTC.NRIET.ViewModel;

namespace Tram.CBTC.NRIET.View.Shell
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NrietShell
    {
        private readonly DomainViewModel m_DomainViewModel;


        public NrietShell()
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
            monitor.Left = 0;
            monitor.Width -= 70;
            monitor.Show();
        }
    }
}