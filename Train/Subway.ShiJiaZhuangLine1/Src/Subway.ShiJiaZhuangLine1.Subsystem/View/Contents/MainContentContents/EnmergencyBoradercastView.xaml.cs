using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShiJiaZhuangLine1.Interface.Attibutes;
using Subway.ShiJiaZhuangLine1.Subsystem.Constant;
using Subway.ShiJiaZhuangLine1.Subsystem.Controls;
using Subway.ShiJiaZhuangLine1.Subsystem.ViewModels;

namespace Subway.ShiJiaZhuangLine1.Subsystem.View.Contents.MainContentContents
{
    /// <summary>
    /// EnmergencyBoradercastView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainContentContentRegion)]
    [TitleName("紧急广播")]
    public partial class EnmergencyBoradercastView :INavigationAware
    {
        public EnmergencyBoradercastView()
        {
            InitializeComponent();
        }
      

        private void EnmergencyBoradercastView_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.DataContext != null)
            {
                if ((bool)e.NewValue && !(bool)e.OldValue)
                {
                   
                }
                else if (!(bool)e.NewValue && (bool)e.OldValue)
                {

                   
                }
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            ((ShellViewModel)DataContext).Model.EnmergencyBorader.GetCurrent.Execute(null);
            ((ShellViewModel)DataContext).Model.EnmergencyBorader.ChangeCurrentPage.Execute(null);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }
    }
}
