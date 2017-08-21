using System.ComponentModel.Composition;
using System.Windows;
using CRH2TrainTypeSelector.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace CRH2TrainTypeSelector
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    [Export]
    public partial class Shell : Window
    {
        public Shell()
        {
            InitializeComponent();

            DataContext = ServiceLocator.Current.GetInstance<ShellViewModel>();
        }

    }
}
