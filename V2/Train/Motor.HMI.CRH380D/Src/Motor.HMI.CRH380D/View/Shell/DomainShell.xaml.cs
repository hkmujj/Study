using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.ServiceLocation;
using Motor.HMI.CRH380D.ViewModel;

namespace Motor.HMI.CRH380D.View.Shell
{
    /// <summary>
    /// ShellWithButton.xaml 的交互逻辑
    /// </summary>
    [Export]
    public partial class DomainShell : UserControl
    {
        public DomainShell()
        {
            InitializeComponent();
            this.Loaded += DoMainShell_Loaded;
        }
        private void DoMainShell_Loaded(object sender, RoutedEventArgs e)
        {
            var tmp = ServiceLocator.Current.GetInstance<DomainViewModel>();
            DataContext = tmp;
        }
    }
}
