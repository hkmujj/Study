using System.ComponentModel.Composition;
using Motor.HMI.CRH380BG.Constant;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Motor.HMI.CRH380BG.View.Title
{
    /// <summary>
    /// TitleView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentTitle)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class TitleView 
    {
        public TitleView()
        {
            InitializeComponent();
        }
    }
}
