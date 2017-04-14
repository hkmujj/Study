using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using TestSubsystem.ViewModel;

namespace TestSubsystem.View
{
    /// <summary>
    /// SubView1.xaml 的交互逻辑
    /// </summary>
    //[ViewExport(RegionName = RegionNames.MainContent)]
    //[PartCreationPolicy(CreationPolicy.Shared)]
    public partial class SubView1 : UserControl
    {
        //[ImportingConstructor]
        public SubView1(Sub1ViewModel viewModel, IRegionManager regionManager)
        {
            //RegionManager.SetRegionManager(this, regionManager);
            
            InitializeComponent();
            //RegionManager.SetRegionManager(ContentControl, regionManager);
            DataContext = viewModel;
            //RegionManager.UpdateRegions();
        }
    }
}
