using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.ServiceLocation;
using Subway.WuHanLine6.ViewModels;

namespace Subway.WuHanLine6.Views.Shell
{
    /// <summary>
    /// DoMainShell.xaml 的交互逻辑
    /// </summary>
    [Export]
    public partial class DoMainShell : UserControl
    {
        /// <summary>
        ///
        /// </summary>
        public DoMainShell()
        {
            InitializeComponent();

            this.Loaded += DoMainShell_Loaded;
        }

        private void DoMainShell_Loaded(object sender, RoutedEventArgs e)
        {
            var tmp = ServiceLocator.Current.GetInstance<WuHanViewModel>();
            DataContext = tmp;
        }
    }
}