using MMI.Facility.WPFInfrastructure.Behaviors;
using Motor.HMI.CRH380BG.Constant;
using System.ComponentModel.Composition;

namespace Motor.HMI.CRH380BG.View.Contents.Maintain
{
    /// <summary>
    /// EnergyMeteringView.xaml 的交互逻辑
    /// </summary>
    //[ViewExport(RegionName = RegionNames.MainContentContent)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [ViewExport(RegionName = RegionNames.ContentContent)]
    public partial class MaintainCloseView
    {
        public MaintainCloseView()
        {
            InitializeComponent();
        }
    }
}
