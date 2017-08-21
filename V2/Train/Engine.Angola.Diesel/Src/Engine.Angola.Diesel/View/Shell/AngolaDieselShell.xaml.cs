using System.ComponentModel.Composition;
using System.Windows;
using Engine.Angola.Diesel.View.DataProvider;
using Engine.Angola.Diesel.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Engine.Angola.Diesel.View.Shell
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    [Export]
    public partial class AngolaDieselShell
    {
        public AngolaDieselShell()
        {
            InitializeComponent();

            Loaded += OnLoaded;

        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var wind = new DataProviderView(ServiceLocator.Current.GetInstance<AngolaDieselShellViewModel>())
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Width = 400,
                Height = 300
            };
            wind.Show();
        }
    }
}