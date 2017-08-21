using System.ComponentModel.Composition;
using System.Windows;
using Engine.Angola.TCMS.View.Monitor;
using Engine.Angola.TCMS.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Engine.Angola.TCMS.View.Shell
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    [Export]
    public partial class AngolaTCMSShell : Window
    {
        public AngolaTCMSShell()
        {
            InitializeComponent();
            DataContext = ServiceLocator.Current.GetInstance<AngolaTCMSShellViewModel>();

        }

        private void AngolaTCMSShell_OnLoaded(object sender, RoutedEventArgs e)
        {

            var monitor = new DataMonitorWindow
            {
                DataContext = ServiceLocator.Current.GetInstance<AngolaTCMSShellViewModel>(),
                Owner = this,
            };
            monitor.Show();
        }
    }
}
