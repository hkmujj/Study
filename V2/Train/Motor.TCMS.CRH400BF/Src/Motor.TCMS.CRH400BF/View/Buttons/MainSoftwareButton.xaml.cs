using System.ComponentModel.Composition;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Motor.TCMS.CRH400BF.Constant;

namespace Motor.TCMS.CRH400BF.View.Buttons
{
    /// <summary>
    /// MainSoftwareBotton.xaml 的交互逻辑
    /// </summary>
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [ViewExport(RegionName = RegionNames.MainSoftwareButton)]
    public partial class MainSoftwareBotton
    {
        public MainSoftwareBotton()
        {
            InitializeComponent();
        }

    }

}
