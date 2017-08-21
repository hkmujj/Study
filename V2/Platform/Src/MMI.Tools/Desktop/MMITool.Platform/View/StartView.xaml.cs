using System.ComponentModel.Composition;
using System.Windows;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace MMITool.Platform.View
{
    /// <summary>
    /// StartView.xaml 的交互逻辑
    /// </summary>
    [Export]
    public partial class StartView
    {
        public StartView()
        {
            InitializeComponent();
        }

        private void StartView_OnLoaded(object sender, RoutedEventArgs e)
        {
            var appService = ServiceLocator.Current.GetInstance<IApplicationService>();
            var shell = ShellFactory.CreateShell();
            Application.Current.MainWindow = shell;
            Application.Current.MainWindow.Show();
            Close();
        }
    }
}
