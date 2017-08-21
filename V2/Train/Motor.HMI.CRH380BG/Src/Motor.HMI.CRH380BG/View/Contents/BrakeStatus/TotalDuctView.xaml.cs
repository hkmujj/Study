using MMI.Facility.WPFInfrastructure.Behaviors;
using Motor.HMI.CRH380BG.Constant;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;

namespace Motor.HMI.CRH380BG.View.Contents.BrakeStatus
{
    /// <summary>
    /// BrakeStatusView.xaml 的交互逻辑
    /// </summary>
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [ViewExport(RegionName = RegionNames.ContentContent)]
    public partial class TotalDuctView
    {
        public TotalDuctView()
        {
            InitializeComponent();
        }
    }
}
