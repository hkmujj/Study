using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using LightRail.HMI.SZLHLF.Resources;
using LightRail.HMI.SZLHLF.Extension;
using LightRail.HMI.SZLHLF.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace LightRail.HMI.SZLHLF.View.Shell
{
    /// <summary>
    /// ShellWithButton.xaml 的交互逻辑
    /// </summary>
    [Export]
    public partial class ShellWithButton : UserControl
    {
        public ShellWithButton()
        {
            InitializeComponent();

            Loaded += ShellWithButton_Loaded;
            this.Resources.MergedDictionaries.Add(SZLHLFResourceManager.Instance);
            SZLHLFResourceManager.ResourceChanged += this.ResourceChanged;
        }

        private void ShellWithButton_Loaded(object sender, RoutedEventArgs e)
        {
            var tmp = ServiceLocator.Current.GetInstance<SZLHLFViewModel>();
            DataContext = tmp;
        }
    }
}
