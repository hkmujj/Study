using System.ComponentModel.Composition;
using System.Windows.Controls;
using Engine.TCMS.Turkmenistan.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Engine.TCMS.Turkmenistan.View.Shell
{
    /// <summary>
    /// ShellWithoutButton.xaml 的交互逻辑
    /// </summary>
    public partial class ShellWithoutButton : UserControl
    {
        public ShellWithoutButton()
        {
            InitializeComponent();
            Loaded += ShellWithoutButton_Loaded;
        }

        private void ShellWithoutButton_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataContext = ServiceLocator.Current.GetInstance<DomainViewModel>();
        }
    }
}
