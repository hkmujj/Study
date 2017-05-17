using System.Windows.Controls;
using System.Windows.Input;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShiJiaZhuangLine1.Interface;
using Subway.ShiJiaZhuangLine1.Subsystem.Constant;
using Subway.ShiJiaZhuangLine1.Subsystem.ViewModels;

namespace Subway.ShiJiaZhuangLine1.Subsystem.View.Contents.MainContentContents
{
    /// <summary>
    /// StationView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainContentHeaderRegion)]
    public partial class StationView : UserControl
    {
        public StationView()
        {
            InitializeComponent();
        }

        private void UIElement_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var shell = DataContext as ShellViewModel;
            if (shell.Model.EventPageModel.TitleEventLevel == EventLevel.Normal)
            {
                return;
            }
            shell.Controller.ShellContentNavigateCommand.Execute(ViewNames.EventView);
        }
    }
}
