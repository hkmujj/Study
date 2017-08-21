using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.XiaMenLine1.Subsystem.Constant;
using Subway.XiaMenLine1.Subsystem.ViewModels;

namespace Subway.XiaMenLine1.Subsystem.View
{
    /// <summary>
    /// MessageView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainRootShell)]
    public partial class MessageView : UserControl
    {
        public MessageView()
        {
            InitializeComponent();
            ConfimButton.PreviewMouseDown += ConfimButton_PreviewMouseDown;
            DataContextChanged += MessageView_DataContextChanged;
        }

        private void MessageView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                model = e.NewValue as ShellViewModel;
            }
        }

        private ShellViewModel model;
        private void ConfimButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ServiceLocator.Current.GetInstance<IRegionManager>().RequestNavigate(RegionNames.MainRootShell, ViewNames.MainRooeShell);
        }
    }
}
