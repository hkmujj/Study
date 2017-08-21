using System.ComponentModel.Composition;
using Engine.HMI.SS3B.View.Constance;
using Engine.HMI.SS3B.View.ViewModel.KunMing;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine.HMI.SS3B.View.View.LiuZhou
{
    /// <summary>
    /// RunningMain.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = LiuZhouRegionNames.MainContent, IsDefaultView = true)]
    public partial class Main
    {
        [ImportingConstructor]
        public Main(SS3BViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
