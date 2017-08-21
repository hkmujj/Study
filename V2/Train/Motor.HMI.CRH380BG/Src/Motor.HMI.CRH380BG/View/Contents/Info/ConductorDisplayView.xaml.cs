using MMI.Facility.WPFInfrastructure.Behaviors;
using Motor.HMI.CRH380BG.Constant;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Motor.HMI.CRH380BG.View.Contents.Info
{
    /// <summary>
    /// MaintainInfoView.xaml 的交互逻辑
    /// </summary>
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [ViewExport(RegionName = RegionNames.ContentContent)]
    public partial class ConductorDisplayView
    {
        public ConductorDisplayView()
        {
            InitializeComponent();
        }
    }
}
