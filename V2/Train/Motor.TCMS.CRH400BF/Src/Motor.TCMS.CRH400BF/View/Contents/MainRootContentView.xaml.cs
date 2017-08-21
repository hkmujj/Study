using System.ComponentModel.Composition;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Motor.TCMS.CRH400BF.Constant;

namespace Motor.TCMS.CRH400BF.View.Contents
{
    /// <summary>
    /// MainTowContentView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainContentContent)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class MainRootContentView
    {
        public MainRootContentView()
        {
            InitializeComponent();
        }
    }
}
