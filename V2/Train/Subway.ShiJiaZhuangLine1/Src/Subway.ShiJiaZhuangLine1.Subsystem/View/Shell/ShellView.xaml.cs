using System.ComponentModel.Composition;
using System.Windows;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShiJiaZhuangLine1.Subsystem.Constant;
using Subway.ShiJiaZhuangLine1.Subsystem.ViewModels;

namespace Subway.ShiJiaZhuangLine1.Subsystem.View.Shell
{
    /// <summary>
    /// DoMain.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.RootRegion)]
    public partial class ShellView
    {
        private readonly IRegionManager m_RegionManager;

        [ImportingConstructor]
        public ShellView(ShellViewModel viewModel, IRegionManager regionManager)
        {
            m_RegionManager = regionManager;
            InitializeComponent();
            DataContext = viewModel;
        }

        private void ShellView_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            m_RegionManager.RequestNavigate(RegionNames.ShellContentRegion, ViewNames.ShellContentMainContentView);
            m_RegionManager.RequestNavigate(RegionNames.MainContentContentRegion, ViewNames.MainRunningView);
            m_RegionManager.RequestNavigate(RegionNames.MainRunningChildrenTrainRegion, ViewNames.MainRunningDoorPage);
            m_RegionManager.RequestNavigate(RegionNames.MainRunningChildrenBreakerRegion, ViewNames.HightBreakerView);
        }
    }
}
